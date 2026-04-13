using System.Diagnostics;
using System.Drawing;

namespace ShizukuActivator;

public partial class Form1 : Form
{
    private const string ProjectUrl = "https://github.com/vishalv22/Shizuku-Activator";
    private readonly AdbService adbService = new();
    private IReadOnlyList<DeviceInfo> devices = Array.Empty<DeviceInfo>();
    private bool isBusy;

    public Form1()
    {
        InitializeComponent();
        ApplyTheme();
    }

    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);
        await RefreshStatusAsync();
    }

    private async void DetectButton_Click(object sender, EventArgs e)
    {
        await RefreshStatusAsync();
    }

    private async void BrowseButton_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select the folder that contains adb.exe or the platform-tools folder.",
            UseDescriptionForTitle = true,
            ShowNewFolderButton = false,
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var selection = adbService.TrySetManualPath(dialog.SelectedPath);
        if (!selection.Success)
        {
            ApplySummary(
                "ADB path invalid",
                selection.Message,
                StatusTone.Danger);
            return;
        }

        ApplySummary(
            "ADB detected",
            selection.Message,
            StatusTone.Success);

        await RefreshStatusAsync();
    }

    private async void ActivateButton_Click(object sender, EventArgs e)
    {
        var selectedDevice = GetSelectedDevice();
        if (selectedDevice is null)
        {
            ApplySummary(
                "Select a device",
                "Choose one authorized device from the list before activating Shizuku.",
                StatusTone.Warning);
            return;
        }

        await RunBusyActionAsync(
            "Activating Shizuku...",
            async () =>
            {
                var result = await adbService.ActivateShizukuAsync(selectedDevice.Id);
                if (!result.Success)
                {
                    ApplySummary("Activation failed", result.Message, StatusTone.Danger);
                    return;
                }

                ApplySummary("Shizuku activated", result.Message, StatusTone.Success);
                await RefreshStatusAsync();
            });
    }

    private void DeviceListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateSelectionState();
    }

    private void CopyButton_Click(object sender, EventArgs e)
    {
        var summary = new[]
        {
            $"ADB: {adbPathValueLabel.Text}",
            $"Source: {adbSourceValueLabel.Text}",
            $"Summary: {summaryValueLabel.Text}",
            string.Empty,
            "Devices:",
        }.ToList();

        summary.AddRange(devices.Select(device => $"{device.Id} | {device.Name} | {device.Model} | {device.StateLabel}"));
        summary.Add(string.Empty);
        summary.Add(detailsTextBox.Text);

        Clipboard.SetText(string.Join(Environment.NewLine, summary));
        statusStripLabel.Text = "Status copied.";
    }

    private void ReadmeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = ProjectUrl,
                UseShellExecute = true,
            });
        }
        catch (Exception ex)
        {
            ApplySummary("Could not open README", ex.Message, StatusTone.Danger);
        }
    }

    private async Task RefreshStatusAsync()
    {
        await RunBusyActionAsync(
            "Checking ADB and device status...",
            async () =>
            {
                var result = await adbService.GetStatusAsync();
                adbPathValueLabel.Text = result.AdbPathDisplay;
                adbSourceValueLabel.Text = result.AdbSourceDisplay;
                devices = result.Devices;
                BindDevices(devices);
                ApplySummary(result.SummaryTitle, result.Message, result.Tone);
            });
    }

    private async Task RunBusyActionAsync(string statusText, Func<Task> action)
    {
        SetBusyState(true, statusText);

        try
        {
            await action();
        }
        catch (Exception ex)
        {
            ApplySummary("Unexpected error", ex.Message, StatusTone.Danger);
        }
        finally
        {
            SetBusyState(false, "Ready");
        }
    }

    private void BindDevices(IReadOnlyList<DeviceInfo> deviceItems)
    {
        var previousSelection = GetSelectedDevice()?.Id;
        deviceListView.BeginUpdate();
        deviceListView.Items.Clear();

        foreach (var device in deviceItems)
        {
            var item = new ListViewItem(device.Id);
            item.SubItems.Add(device.Name);
            item.SubItems.Add(device.Model);
            item.SubItems.Add(device.StateLabel);
            item.Tag = device;
            item.ForeColor = GetToneColor(device.Tone);
            deviceListView.Items.Add(item);
        }

        if (deviceListView.Items.Count > 0)
        {
            var matchingItem = deviceListView.Items
                .Cast<ListViewItem>()
                .FirstOrDefault(item => ((DeviceInfo)item.Tag!).Id == previousSelection)
                ?? deviceListView.Items[0];

            matchingItem.Selected = true;
        }

        deviceListView.EndUpdate();
        UpdateSelectionState();
    }

    private void UpdateSelectionState()
    {
        var selectedDevice = GetSelectedDevice();
        activateButton.Enabled = !isBusy && selectedDevice is { IsReady: true };

        if (selectedDevice is null)
        {
            deviceValueLabel.Text = "No device selected";
            nameValueLabel.Text = "-";
            modelValueLabel.Text = "-";
            stateValueLabel.Text = "-";
            SetLabelTone(stateValueLabel, StatusTone.Info);
            return;
        }

        deviceValueLabel.Text = selectedDevice.Id;
        nameValueLabel.Text = selectedDevice.Name;
        modelValueLabel.Text = selectedDevice.Model;
        stateValueLabel.Text = selectedDevice.StateLabel;
        SetLabelTone(stateValueLabel, selectedDevice.Tone);
    }

    private DeviceInfo? GetSelectedDevice()
    {
        if (deviceListView.SelectedItems.Count == 0)
        {
            return null;
        }

        return deviceListView.SelectedItems[0].Tag as DeviceInfo;
    }

    private void ApplySummary(string title, string details, StatusTone tone)
    {
        summaryValueLabel.Text = title;
        detailsTextBox.Text = details;
        SetLabelTone(summaryValueLabel, tone);
        detailsTextBox.ForeColor = GetToneColor(tone);
    }

    private void SetBusyState(bool busy, string statusText)
    {
        isBusy = busy;
        detectButton.Enabled = !busy;
        browseButton.Enabled = !busy;
        copyButton.Enabled = !busy;
        activateButton.Enabled = !busy && GetSelectedDevice() is { IsReady: true };
        statusStripLabel.Text = statusText;
        UseWaitCursor = busy;
    }

    private void ApplyTheme()
    {
        BackColor = Color.White;
        ForeColor = ColorPalette.Text;
        instructionLabel.ForeColor = ColorPalette.Muted;
        readmeLinkLabel.LinkColor = ColorPalette.Info;
        readmeLinkLabel.ActiveLinkColor = ColorPalette.Info;
        readmeLinkLabel.VisitedLinkColor = ColorPalette.Info;

        foreach (var label in new[] { adbLabel, adbPathValueLabel, adbSourceLabel, adbSourceValueLabel, summaryLabel, deviceLabel, deviceValueLabel, modelLabel, modelValueLabel, stateLabel, stateValueLabel })
        {
            label.Font = label == adbPathValueLabel || label == adbSourceValueLabel || label == deviceValueLabel || label == modelValueLabel || label == stateValueLabel
                ? new Font("Consolas", 9F, FontStyle.Regular)
                : new Font("Segoe UI", 9F, FontStyle.Regular);
        }

        foreach (var label in new[] { nameLabel, nameValueLabel })
        {
            label.Font = label == nameValueLabel
                ? new Font("Consolas", 9F, FontStyle.Regular)
                : new Font("Segoe UI", 9F, FontStyle.Regular);
        }

        detailsTextBox.Font = new Font("Consolas", 9F, FontStyle.Regular);
        deviceListView.Font = new Font("Consolas", 9F, FontStyle.Regular);
        statusStrip.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        SetLabelTone(summaryValueLabel, StatusTone.Info);
        SetLabelTone(stateValueLabel, StatusTone.Info);
    }

    private static void SetLabelTone(Control control, StatusTone tone)
    {
        control.ForeColor = GetToneColor(tone);
    }

    private static Color GetToneColor(StatusTone tone)
    {
        return tone switch
        {
            StatusTone.Success => ColorPalette.Success,
            StatusTone.Warning => ColorPalette.Warning,
            StatusTone.Danger => ColorPalette.Danger,
            _ => ColorPalette.Info,
        };
    }
}

internal sealed class AdbService
{
    private const string ShizukuCommand = "shell sh /sdcard/Android/data/moe.shizuku.privileged.api/start.sh";
    private static readonly string ManualPathFile = Path.Combine(AppContext.BaseDirectory, "adb-path.txt");

    public async Task<StatusSnapshot> GetStatusAsync()
    {
        var adb = await ResolveAdbAsync();
        if (!adb.Success)
        {
            return new StatusSnapshot(
                "ADB not found",
                adb.Message,
                "Not found",
                "-",
                Array.Empty<DeviceInfo>(),
                StatusTone.Danger);
        }

        var devicesResult = await RunProcessAsync(adb.Path!, "devices");
        if (!devicesResult.Success)
        {
            return new StatusSnapshot(
                "ADB error",
                devicesResult.Message,
                adb.Path!,
                adb.Source!,
                Array.Empty<DeviceInfo>(),
                StatusTone.Danger);
        }

        var devices = await ParseDevicesAsync(adb.Path!, devicesResult.Output);
        if (devices.Count == 0)
        {
            return new StatusSnapshot(
                "No device found",
                "ADB is ready. Connect your phone, enable USB debugging, and press Detect Device.",
                adb.Path!,
                adb.Source!,
                devices,
                StatusTone.Warning);
        }

        var readyCount = devices.Count(device => device.IsReady);
        var tone = readyCount > 0 ? StatusTone.Success : StatusTone.Warning;
        var title = readyCount > 0 ? "ADB ready" : "Device needs attention";
        var details = readyCount > 0
            ? $"{devices.Count} device(s) detected. Select the correct phone by name or ID, then activate Shizuku."
            : "Device detected, but authorization or connection still needs attention.";

        return new StatusSnapshot(
            title,
            details,
            adb.Path!,
            adb.Source!,
            devices,
            tone);
    }

    public async Task<ActionResult> ActivateShizukuAsync(string deviceId)
    {
        var adb = await ResolveAdbAsync();
        if (!adb.Success)
        {
            return new ActionResult(false, adb.Message);
        }

        var command = $"-s {Quote(deviceId)} {ShizukuCommand}";
        var result = await RunProcessAsync(adb.Path!, command);
        if (!result.Success)
        {
            return new ActionResult(false, result.Message);
        }

        var message = string.IsNullOrWhiteSpace(result.Output)
            ? $"Shizuku activation command sent to {deviceId}."
            : result.Output.Trim();

        return new ActionResult(true, message);
    }

    public ManualPathResult TrySetManualPath(string selectedFolder)
    {
        var adbPath = FindAdbFromFolder(selectedFolder);
        if (adbPath is null)
        {
            return new ManualPathResult(false, "Could not find adb.exe in that folder or in its platform-tools subfolder.");
        }

        try
        {
            File.WriteAllText(ManualPathFile, Path.GetDirectoryName(adbPath)!);
        }
        catch (Exception ex)
        {
            return new ManualPathResult(false, $"ADB found, but the path could not be saved: {ex.Message}");
        }

        return new ManualPathResult(true, $"ADB files detected successfully at:{Environment.NewLine}{adbPath}");
    }

    private async Task<AdbResolution> ResolveAdbAsync()
    {
        var manualPath = TryResolveSavedManualPath();
        if (manualPath.Success)
        {
            return manualPath;
        }

        var globalPath = await TryFindGlobalAdbAsync();
        if (globalPath.Success)
        {
            return globalPath;
        }

        foreach (var candidate in GetPortableCandidates())
        {
            if (File.Exists(candidate))
            {
                return new AdbResolution(true, candidate, "Portable");
            }
        }

        return new AdbResolution(
            false,
            null,
            null,
            "ADB was not found. Use Set ADB Folder, install platform-tools globally, or place this app in the platform-tools folder.");
    }

    private static IEnumerable<string> GetPortableCandidates()
    {
        var baseDirectory = AppContext.BaseDirectory;
        yield return Path.Combine(baseDirectory, "adb.exe");
        yield return Path.Combine(baseDirectory, "platform-tools", "adb.exe");
    }

    private static AdbResolution TryResolveSavedManualPath()
    {
        try
        {
            if (!File.Exists(ManualPathFile))
            {
                return new AdbResolution(false, null, null, "No manual path saved.");
            }

            var savedDirectory = File.ReadAllText(ManualPathFile).Trim();
            var adbPath = FindAdbFromFolder(savedDirectory);
            if (adbPath is not null)
            {
                return new AdbResolution(true, adbPath, "Manual path");
            }
        }
        catch
        {
        }

        return new AdbResolution(false, null, null, "Saved path invalid.");
    }

    private static string? FindAdbFromFolder(string folder)
    {
        if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
        {
            return null;
        }

        var direct = Path.Combine(folder, "adb.exe");
        if (File.Exists(direct))
        {
            return direct;
        }

        var nested = Path.Combine(folder, "platform-tools", "adb.exe");
        return File.Exists(nested) ? nested : null;
    }

    private static async Task<AdbResolution> TryFindGlobalAdbAsync()
    {
        var result = await RunProcessAsync("where.exe", "adb");
        if (!result.Success)
        {
            return new AdbResolution(false, null, null, result.Message);
        }

        var path = result.Output
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault();

        return string.IsNullOrWhiteSpace(path)
            ? new AdbResolution(false, null, null, "ADB not found on PATH.")
            : new AdbResolution(true, path.Trim(), "Global PATH");
    }

    private static async Task<List<DeviceInfo>> ParseDevicesAsync(string adbPath, string output)
    {
        var devices = new List<DeviceInfo>();
        var lines = output
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                continue;
            }

            var id = parts[0];
            var rawState = parts[1];
            var state = NormalizeState(rawState);
            var isAuthorized = rawState.Equals("device", StringComparison.OrdinalIgnoreCase);
            var name = isAuthorized ? await GetDeviceNameAsync(adbPath, id) : "Unknown";
            var model = isAuthorized ? await GetDeviceModelAsync(adbPath, id) : "Unknown";

            devices.Add(new DeviceInfo(
                id,
                string.IsNullOrWhiteSpace(name) ? "Unknown" : name,
                string.IsNullOrWhiteSpace(model) ? "Unknown" : model,
                state.Label,
                state.Tone,
                state.Ready));
        }

        return devices;
    }

    private static (string Label, StatusTone Tone, bool Ready) NormalizeState(string rawState)
    {
        return rawState.ToLowerInvariant() switch
        {
            "device" => ("Authorized", StatusTone.Success, true),
            "unauthorized" => ("Unauthorized", StatusTone.Warning, false),
            "offline" => ("Offline", StatusTone.Danger, false),
            _ => (rawState, StatusTone.Info, false),
        };
    }

    private static async Task<string?> GetDeviceModelAsync(string adbPath, string deviceId)
    {
        var result = await RunProcessAsync(adbPath, $"-s {Quote(deviceId)} shell getprop ro.product.model");
        return result.Success ? result.Output.Trim() : null;
    }

    private static async Task<string?> GetDeviceNameAsync(string adbPath, string deviceId)
    {
        var candidates = new[]
        {
            "shell settings get global device_name",
            "shell settings get secure bluetooth_name",
            "shell getprop ro.config.marketing_name",
            "shell getprop ro.product.marketname",
            "shell getprop ro.product.odm.marketname",
        };

        foreach (var candidate in candidates)
        {
            var result = await RunProcessAsync(adbPath, $"-s {Quote(deviceId)} {candidate}");
            var value = CleanShellValue(result.Success ? result.Output : string.Empty);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
        }

        var manufacturerResult = await RunProcessAsync(adbPath, $"-s {Quote(deviceId)} shell getprop ro.product.manufacturer");
        var modelResult = await RunProcessAsync(adbPath, $"-s {Quote(deviceId)} shell getprop ro.product.model");
        var manufacturer = CleanShellValue(manufacturerResult.Success ? manufacturerResult.Output : string.Empty);
        var model = CleanShellValue(modelResult.Success ? modelResult.Output : string.Empty);

        var combined = string.Join(" ", new[] { manufacturer, model }.Where(value => !string.IsNullOrWhiteSpace(value))).Trim();
        return string.IsNullOrWhiteSpace(combined) ? null : combined;
    }

    private static string CleanShellValue(string value)
    {
        var cleaned = value.Trim();
        return cleaned.Equals("null", StringComparison.OrdinalIgnoreCase) ? string.Empty : cleaned;
    }

    private static async Task<CommandResult> RunProcessAsync(string fileName, string arguments)
    {
        try
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            var stdoutTask = process.StandardOutput.ReadToEndAsync();
            var stderrTask = process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            var stdout = await stdoutTask;
            var stderr = await stderrTask;

            if (process.ExitCode != 0)
            {
                var error = string.IsNullOrWhiteSpace(stderr) ? stdout.Trim() : stderr.Trim();
                return new CommandResult(false, stdout, error);
            }

            return new CommandResult(true, stdout, string.Empty);
        }
        catch (Exception ex)
        {
            return new CommandResult(false, string.Empty, ex.Message);
        }
    }

    private static string Quote(string value)
    {
        return $"\"{value.Replace("\"", "\\\"", StringComparison.Ordinal)}\"";
    }
}

internal static class ColorPalette
{
    public static readonly Color Text = Color.FromArgb(31, 35, 40);
    public static readonly Color Muted = Color.FromArgb(101, 109, 118);
    public static readonly Color Success = Color.FromArgb(31, 136, 61);
    public static readonly Color Warning = Color.FromArgb(154, 103, 0);
    public static readonly Color Danger = Color.FromArgb(207, 34, 46);
    public static readonly Color Info = Color.FromArgb(9, 105, 218);
}

internal enum StatusTone
{
    Info,
    Success,
    Warning,
    Danger,
}

internal sealed record DeviceInfo(string Id, string Name, string Model, string StateLabel, StatusTone Tone, bool IsReady);

internal sealed record StatusSnapshot(
    string SummaryTitle,
    string Message,
    string AdbPathDisplay,
    string AdbSourceDisplay,
    IReadOnlyList<DeviceInfo> Devices,
    StatusTone Tone);

internal sealed record ActionResult(bool Success, string Message);
internal sealed record ManualPathResult(bool Success, string Message);
internal sealed record AdbResolution(bool Success, string? Path, string? Source, string Message = "");
internal sealed record CommandResult(bool Success, string Output, string Message);
