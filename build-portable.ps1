$ErrorActionPreference = "Stop"

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue) -or -not ((dotnet --list-sdks) -match "^8\.")) {
    if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
        Write-Error "The .NET 8 SDK is not installed and winget is unavailable. Install the .NET 8 SDK from https://dotnet.microsoft.com/download/dotnet/8.0, then run this script again."
    }

    Write-Host "Installing the .NET 8 SDK..."
    winget install --id Microsoft.DotNet.SDK.8 --exact --accept-source-agreements --accept-package-agreements
}

if (-not ((dotnet --list-sdks) -match "^8\.")) {
    Write-Error "The .NET 8 SDK is still unavailable. Restart PowerShell and run this script again."
}

dotnet publish windows\ShizukuActivator\ShizukuActivator.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:DebugSymbols=false -p:DebugType=None -o windows\dist

Write-Host ""
Write-Host "Portable executable created at windows\dist\ShizukuActivator.exe"
