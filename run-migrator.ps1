<#
.SYNOPSIS
    Builds and runs kamrj.Migrator to apply database migrations / seed data.
#>

$ErrorActionPreference = "Stop"

$root = $PSScriptRoot
$migratorDir = Join-Path $root "aspnet-core\src\kamrj.Migrator"
$migratorProject = Join-Path $migratorDir "kamrj.Migrator.csproj"

if (-not (Test-Path $migratorProject)) {
    Write-Error "Migrator project not found at: $migratorProject"
    exit 1
}

# dotnet run's working directory follows the caller's CWD, not --project,
# and log4net.config / appsettings.json are loaded relative to CWD.
Push-Location $migratorDir
try {
    Write-Host "Running kamrj.Migrator ..." -ForegroundColor Cyan
    dotnet run --project $migratorProject

    if ($LASTEXITCODE -ne 0) {
        Write-Error "Migrator exited with code $LASTEXITCODE"
        exit $LASTEXITCODE
    }

    Write-Host "Migration completed successfully." -ForegroundColor Green
}
finally {
    Pop-Location
}
