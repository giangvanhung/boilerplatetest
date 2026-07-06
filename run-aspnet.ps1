<#
.SYNOPSIS
    Starts kamrj.Web.Host (ASP.NET Core API) in Debug configuration.
#>

$ErrorActionPreference = "Stop"

$originalLocation = Get-Location
$hostDir     = Join-Path $PSScriptRoot "aspnet-core\src\kamrj.Web.Host"
$hostProject = Join-Path $hostDir "kamrj.Web.Host.csproj"

if (-not (Test-Path $hostProject)) {
    Write-Error "Web.Host project not found at: $hostProject"
    exit 1
}

Write-Host "Starting kamrj.Web.Host (Debug) ..." -ForegroundColor Cyan
Set-Location $hostDir

try {
    & dotnet run --configuration Debug
} catch {
    Write-Host "Script interrupted" -ForegroundColor Yellow
} finally {
    Write-Host "Returning to original location..." -ForegroundColor Gray
    Set-Location $originalLocation
}

Write-Host "Web.Host stopped" -ForegroundColor Cyan
