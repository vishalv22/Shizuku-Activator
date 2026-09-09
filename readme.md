# Shizuku Activator

A simple Windows tool to activate **Shizuku** on Android using `adb`.

## Download for Windows

Download `ShizukuActivator.exe` from the latest GitHub release:

[Download Latest Release](https://github.com/vishalv22/Shizuku-Activator/releases/latest)

Download Android Platform Tools:

[Android Platform Tools](https://developer.android.com/tools/releases/platform-tools)

## Android Setup:

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



## Batch File Method

You can also activate Shizuku with [ShizukuGo.bat](ShizukuGo.bat). Place `ShizukuGo.bat` directly inside your `platform-tools` folder. It automatically finds `adb.exe`.

Then:

1. Run `ShizukuGo.bat`
2. Press `1` to check device detection
3. Press `2` to activate Shizuku

## Build From Source

You can create a portable `.exe` on your pc or laptop:

### 1. Install Git

Install [Git for Windows](https://git-scm.com/download/win), so you can download the source code.

You will need [Android Platform Tools](https://developer.android.com/tools/releases/platform-tools) later to use the app with your phone.

### 2. Download the project

Open PowerShell and run:

```powershell
git clone https://github.com/vishalv22/Shizuku-Activator.git
cd Shizuku-Activator
```

### 3. Install the build requirements and create the portable executable

Run the included PowerShell script. It installs the .NET 8 SDK using `winget` if it is missing, then creates the portable executable:

```powershell
.\build-portable.ps1
```

If PowerShell blocks the script, run:

```powershell
powershell -ExecutionPolicy Bypass -File .\build-portable.ps1
```

Your executable will be here:

```text
windows\dist\ShizukuActivator.exe
```

You can run the executable directly from `windows\dist`, or move it to your existing `platform-tools` folder and run it there.

## Troubleshooting

- If the device shows `unauthorized`, accept the USB debugging prompt on your phone
- If no device appears, reconnect the cable and make sure **USB debugging** is enabled
- If `adb` is not found, install Platform Tools or select the folder manually in the app
