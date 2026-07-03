<#
.SYNOPSIS
    Runs kamrj.Web.Host (API, Debug config) and the Angular dev server together.
    Each opens in its own PowerShell window; close either window to stop it.
#>

$ErrorActionPreference = "Stop"

$root = $PSScriptRoot
$hostDir     = Join-Path $root "aspnet-core\src\kamrj.Web.Host"
$hostProject = Join-Path $hostDir "kamrj.Web.Host.csproj"
$angularDir  = Join-Path $root "angular"

if (-not (Test-Path $hostProject)) {
    Write-Error "Web.Host project not found at: $hostProject"
    exit 1
}
if (-not (Test-Path $angularDir)) {
    Write-Error "Angular folder not found at: $angularDir"
    exit 1
}

# dotnet run's working directory follows the caller's CWD, not --project,
# and appsettings.json / log4net.config are loaded relative to CWD.
Write-Host "Starting kamrj.Web.Host (Debug) ..." -ForegroundColor Cyan
Start-Process powershell -ArgumentList @(
    "-NoExit",
    "-Command",
    "Set-Location `"$hostDir`"; dotnet run --project `"$hostProject`" --configuration Debug"
)

Write-Host "Starting Angular dev server ..." -ForegroundColor Cyan
Start-Process powershell -ArgumentList @(
    "-NoExit",
    "-Command",
    "Set-Location `"$angularDir`"; npm start"
)

Write-Host "Both processes launched in separate windows." -ForegroundColor Green
