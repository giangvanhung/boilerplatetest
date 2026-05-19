# =========================================
# Move to Angular app
# =========================================

Set-Location "angular/src/app"

# =========================================
# SECURITY ROOT
# =========================================

New-Item -ItemType Directory -Force -Path "security"

# =========================================
# MODELS
# =========================================

New-Item -ItemType Directory -Force -Path "security/models"

New-Item -ItemType File -Force -Path "security/models/password-policy.dto.ts"
New-Item -ItemType File -Force -Path "security/models/login-failure-policy.dto.ts"

# =========================================
# SERVICES
# =========================================

New-Item -ItemType Directory -Force -Path "security/services"

New-Item -ItemType File -Force -Path "security/services/security-policy.service.ts"

# =========================================
# COMPONENTS
# =========================================

New-Item -ItemType Directory -Force -Path "security/components/password-policy-form"

New-Item -ItemType File -Force -Path "security/components/password-policy-form/password-policy-form.component.ts"
New-Item -ItemType File -Force -Path "security/components/password-policy-form/password-policy-form.component.html"
New-Item -ItemType File -Force -Path "security/components/password-policy-form/password-policy-form.component.scss"

# =========================================
# LOCKOUT WARNING COMPONENT
# =========================================

New-Item -ItemType Directory -Force -Path "security/components/login-lockout-warning"

New-Item -ItemType File -Force -Path "security/components/login-lockout-warning/login-lockout-warning.component.ts"
New-Item -ItemType File -Force -Path "security/components/login-lockout-warning/login-lockout-warning.component.html"
New-Item -ItemType File -Force -Path "security/components/login-lockout-warning/login-lockout-warning.component.scss"

# =========================================
# PAGES
# =========================================

New-Item -ItemType Directory -Force -Path "security/pages/security-policy"

New-Item -ItemType File -Force -Path "security/pages/security-policy/security-policy.component.ts"
New-Item -ItemType File -Force -Path "security/pages/security-policy/security-policy.component.html"
New-Item -ItemType File -Force -Path "security/pages/security-policy/security-policy.component.scss"

# =========================================
# PASSWORD EXPIRATION PAGE
# =========================================

New-Item -ItemType Directory -Force -Path "security/pages/password-expiration"

New-Item -ItemType File -Force -Path "security/pages/password-expiration/password-expiration.component.ts"
New-Item -ItemType File -Force -Path "security/pages/password-expiration/password-expiration.component.html"
New-Item -ItemType File -Force -Path "security/pages/password-expiration/password-expiration.component.scss"

# =========================================
# LOGIN FAILURE POLICY PAGE
# =========================================

New-Item -ItemType Directory -Force -Path "security/pages/login-failure-policy"

New-Item -ItemType File -Force -Path "security/pages/login-failure-policy/login-failure-policy.component.ts"
New-Item -ItemType File -Force -Path "security/pages/login-failure-policy/login-failure-policy.component.html"
New-Item -ItemType File -Force -Path "security/pages/login-failure-policy/login-failure-policy.component.scss"

# =========================================
# ROOT MODULE FILES
# =========================================

New-Item -ItemType File -Force -Path "security/security.module.ts"
New-Item -ItemType File -Force -Path "security/security-routing.module.ts"

# =========================================
# DONE
# =========================================

Write-Host ""
Write-Host "========================================="
Write-Host "Angular Security module created!"
Write-Host "========================================="