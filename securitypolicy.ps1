# =========================================
# Move to ASP.NET Core src
# =========================================

Set-Location "aspnet-core/src"

# =========================================
# CORE
# =========================================

New-Item -ItemType Directory -Force -Path "kamrj.Core/Security/Authorization"
New-Item -ItemType Directory -Force -Path "kamrj.Core/Security/Entities"
New-Item -ItemType Directory -Force -Path "kamrj.Core/Security/Managers"
New-Item -ItemType Directory -Force -Path "kamrj.Core/Security/Validators"
New-Item -ItemType Directory -Force -Path "kamrj.Core/Security/Expiration"
New-Item -ItemType Directory -Force -Path "kamrj.Core/Security/Lockout"

New-Item -ItemType File -Force -Path "kamrj.Core/Security/Authorization/SecurityPermissionNames.cs"

New-Item -ItemType File -Force -Path "kamrj.Core/Security/Entities/SecurityPolicySetting.cs"
New-Item -ItemType File -Force -Path "kamrj.Core/Security/Entities/UserPasswordHistory.cs"

New-Item -ItemType File -Force -Path "kamrj.Core/Security/Managers/PasswordPolicyManager.cs"
New-Item -ItemType File -Force -Path "kamrj.Core/Security/Managers/SecurityPolicyManager.cs"

New-Item -ItemType File -Force -Path "kamrj.Core/Security/Validators/CustomPasswordValidator.cs"

New-Item -ItemType File -Force -Path "kamrj.Core/Security/Expiration/PasswordExpirationChecker.cs"

New-Item -ItemType File -Force -Path "kamrj.Core/Security/Lockout/LoginFailureHandler.cs"

# =========================================
# APPLICATION
# =========================================

New-Item -ItemType Directory -Force -Path "kamrj.Application/Security/Dto"
New-Item -ItemType Directory -Force -Path "kamrj.Application/Security/Services"

New-Item -ItemType File -Force -Path "kamrj.Application/Security/Dto/PasswordPolicyDto.cs"
New-Item -ItemType File -Force -Path "kamrj.Application/Security/Dto/LoginFailurePolicyDto.cs"

New-Item -ItemType File -Force -Path "kamrj.Application/Security/Services/SecurityPolicyAppService.cs"

# =========================================
# WEB HOST
# =========================================

New-Item -ItemType Directory -Force -Path "kamrj.Web.Host/Security"

New-Item -ItemType File -Force -Path "kamrj.Web.Host/Security/SecurityConfiguration.cs"

# =========================================
# Move back to root
# =========================================

Set-Location "../.."

# =========================================
# ANGULAR
# =========================================

New-Item -ItemType Directory -Force -Path "angular/src/app/admin/security-policy/models"
New-Item -ItemType Directory -Force -Path "angular/src/app/admin/security-policy/services"

New-Item -ItemType File -Force -Path "angular/src/app/admin/security-policy/security-policy.component.ts"
New-Item -ItemType File -Force -Path "angular/src/app/admin/security-policy/security-policy.component.html"
New-Item -ItemType File -Force -Path "angular/src/app/admin/security-policy/security-policy.component.scss"

New-Item -ItemType File -Force -Path "angular/src/app/admin/security-policy/services/security-policy.service.ts"

New-Item -ItemType File -Force -Path "angular/src/app/admin/security-policy/models/password-policy.dto.ts"
New-Item -ItemType File -Force -Path "angular/src/app/admin/security-policy/models/login-failure-policy.dto.ts"

Write-Host ""
Write-Host "====================================="
Write-Host "Security module created successfully!"
Write-Host "====================================="