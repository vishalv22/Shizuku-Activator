# Shizuku Activator

Simple Windows batch file to activate **Shizuku** on an Android device using `adb`.

## Requirements

- A Windows PC
- [Android Platform Tools](https://developer.android.com/tools/releases/platform-tools) installed
- The **Shizuku** app installed
- **Developer options** enabled
- **USB debugging** enabled
- A USB connection between phone and PC

## Setup

### 1. Install Platform Tools

Download and extract Android Platform Tools on your PC.

Example:

```text
C:\Users\YourName\Desktop\platform-tools
```

### 2. Update the ADB Path in `ShizukuGo.bat`

Open [ShizukuGo.bat](/c:/Users/sonam/Desktop/Tools/Shizuku-Activator/ShizukuGo.bat) and change this line:

```bat
cd "C:\Users\YourName\Desktop\platform-tools"
```

Replace it with the location of your own `platform-tools` folder.

### 3. Prepare Your Android Device

On your phone:

1. Enable **Developer options**
2. Enable **USB debugging**
3. Install and open the **Shizuku** app at least once
4. Connect the phone to the PC
5. Allow the **USB debugging** authorization prompt if Android asks for it

## Usage

1. Double-click [ShizukuGo.bat](/c:/Users/sonam/Desktop/Tools/Shizuku-Activator/ShizukuGo.bat)
2. Press `1` to check if your device is detected
3. If the device appears, press `2` to activate Shizuku

You will see:

```text
[1] Check connected Devices
[2] Activate Shizuku
[0] Exit
```

If the device shows as `unauthorized`, check your phone and accept the debugging prompt.

## Troubleshooting

- Device not showing: reconnect the cable, enable **USB debugging**, and accept the phone authorization prompt
- `adb` not working: verify the `platform-tools` path in [ShizukuGo.bat](/c:/Users/sonam/Desktop/Tools/Shizuku-Activator/ShizukuGo.bat) and make sure `adb.exe` exists there
- Shizuku not starting: make sure the app is installed, opened once, and the device is detected before activating
