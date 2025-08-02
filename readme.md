## Shizuku Activator

codes:

```bash 
@echo off
title Shizuku Activation
cd "replace-with-adb-folder-path"

:menu
cls
echo =================================================================
echo                  Shizuku Activation Tool By Vishal
echo =================================================================
echo [1] Check connected Devices
echo [2] Activate Shizuku
echo [0] Exit
echo.

choice /c 120 /n /m "Select an option: "
:: /c = valid keys (1, 2, 0)
:: /n = don't display the valid keys automatically
:: /m = custom message

if errorlevel 3 goto exit
if errorlevel 2 goto shizuku
if errorlevel 1 goto devices
goto menu

:devices
echo.
adb devices
echo.
echo Press ENTER to return to menu...
set /p dummy=
goto menu

:shizuku
echo.
adb shell sh /sdcard/Android/data/moe.shizuku.privileged.api/start.sh
echo.
echo Press ENTER to return to menu...
set /p dummy=
goto menu

:exit
exit


```