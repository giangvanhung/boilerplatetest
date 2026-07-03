<#
.SYNOPSIS
    Starts the Angular dev server (ng serve) on http://localhost:4200.
#>

$ErrorActionPreference = "Stop"

$angularDir = Join-Path $PSScriptRoot "angular"

if (-not (Test-Path $angularDir)) {
    Write-Error "Angular folder not found at: $angularDir"
    exit 1
}

Write-Host "Starting Angular dev server ..." -ForegroundColor Cyan
Set-Location $angularDir
npm start
