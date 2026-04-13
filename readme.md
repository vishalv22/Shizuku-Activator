# Shizuku Activator

A simple Windows tool to activate **Shizuku** on Android using `adb`.

## Download for Windows

Download `ShizukuActivator.exe` from the latest GitHub release:

[Download Latest Release](https://github.com/vishalv22/Shizuku-Activator/releases/latest)

## Android Setup:

On your Android phone:

1. Install the **Shizuku** app
2. Enable **Developer options**
3. Enable **USB debugging**
4. Connect the phone to your Windows PC
5. Allow the USB debugging prompt if Android asks for permission

## Use The Windows App

1. Open `ShizukuActivator.exe`
2. Click **Detect**
3. Select your phone from the device list
4. Click **Activate Shizuku**

If the app cannot find `adb`, click **Set ADB Folder** and select your `platform-tools` folder.

The selected ADB folder is saved for the next time you open the app.

## Download Platform Tools:

If `adb` is not installed on your PC, download Android Platform Tools:

[Android Platform Tools](https://developer.android.com/tools/releases/platform-tools)

After downloading, extract the ZIP file. You can then select that extracted `platform-tools` folder inside the app.

## Batch File Method

You can also activate Shizuku with [ShizukuGo.bat](ShizukuGo.bat).

Before using the batch file, open it and update the `platform-tools` path so it matches your PC.

Then:

1. Run `ShizukuGo.bat`
2. Press `1` to check device detection
3. Press `2` to activate Shizuku

## Build From Source

The Windows app source code is available in [windows/ShizukuActivator](windows/ShizukuActivator).

Requirements:

- Windows PC
- [.NET SDK 8.0 or newer](https://dotnet.microsoft.com/download)
- Android Platform Tools, only if `adb` is not already installed globally

Steps:

1. Clone the repo:

```powershell
git clone https://github.com/vishalv22/Shizuku-Activator.git
```

2. Open a terminal in the project folder:

```powershell
cd Shizuku-Activator
```

3. Check that the app builds correctly:

```powershell
dotnet build windows\ShizukuActivator\ShizukuActivator.csproj
```

4. Create your own release folder:

```powershell
dotnet publish windows\ShizukuActivator\ShizukuActivator.csproj -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true -p:DebugSymbols=false -p:DebugType=None -o windows\dist
```

5. Open the generated app:

```text
windows\dist\ShizukuActivator.exe
```

## Troubleshooting

- If the device shows `unauthorized`, accept the USB debugging prompt on your phone
- If no device appears, reconnect the cable and make sure **USB debugging** is enabled
- If `adb` is not found, install Platform Tools or select the folder manually in the app
