import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { SecurityRoutingModule } from './security-routing.module';

import { SecurityPolicyComponent } from './pages/security-policy/security-policy.component';

@NgModule({
    declarations: [
        SecurityPolicyComponent
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        SecurityRoutingModule
    ]
})
export class SecurityModule {
}