<#
.SYNOPSIS
    Starts kamrj.Web.Host (ASP.NET Core API) in Debug configuration.
#>

$ErrorActionPreference = "Stop"

$hostDir     = Join-Path $PSScriptRoot "aspnet-core\src\kamrj.Web.Host"
$hostProject = Join-Path $hostDir "kamrj.Web.Host.csproj"

if (-not (Test-Path $hostProject)) {
    Write-Error "Web.Host project not found at: $hostProject"
    exit 1
}

Write-Host "Starting kamrj.Web.Host (Debug) ..." -ForegroundColor Cyan
Set-Location $hostDir
dotnet run --project $hostProject --configuration Debug

if ($LASTEXITCODE -ne 0) {
    Write-Error "Web.Host exited with code $LASTEXITCODE"
    exit $LASTEXITCODE
}
