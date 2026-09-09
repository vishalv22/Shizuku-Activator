@echo off
title Shizuku Activation

set "ADB=%~dp0adb.exe"
if not exist "%ADB%" (
	echo adb.exe was not found in this folder:
	echo %~dp0
	echo.
	echo Put ShizukuGo.bat inside the platform-tools folder.
	pause
	exit /b 1
)
cd /d "%~dp0"

:menu
cls
echo =================================================================
echo                  Shizuku Activation Tool By Vishal.
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
"%ADB%" devices
echo.
echo Press ENTER to return to menu...
set /p dummy=
goto menu

:shizuku
echo.
"%ADB%" shell sh /sdcard/Android/data/moe.shizuku.privileged.api/start.sh
echo.
echo Press ENTER to return to menu...
set /p dummy=
goto menu

:exit
exit
