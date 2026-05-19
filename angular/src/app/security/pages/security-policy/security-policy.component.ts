import { Component, OnInit } from '@angular/core';

import {
    FormBuilder,
    FormGroup,
    Validators
} from '@angular/forms';

import { finalize } from 'rxjs/operators';

import { SecurityPolicyService }
    from '../../services/security-policy.service';

@Component({
    selector: 'app-security-policy',
    templateUrl: './security-policy.component.html',
    styleUrls: ['./security-policy.component.scss']
})
export class SecurityPolicyComponent implements OnInit {

    form: FormGroup | undefined;

    saving = false;

    constructor(
        private fb: FormBuilder,
        private service: SecurityPolicyService 
    ) {
    }

    ngOnInit(): void {

        this.buildForm();

        this.load();
    }

    buildForm(): void {

        this.form = this.fb.group({

            forceChangePasswordFirstLogin: [true],

            requiredLength: [
                8,
                [Validators.required]
            ],

            requireUppercase: [true],

            requireLowercase: [true],

            requireDigit: [true],

            requireNonAlphanumeric: [true],

            passwordExpirationDays: [
                90,
                [Validators.required]
            ],

            maxFailedAccessAttempts: [
                5,
                [Validators.required]
            ],

            lockoutMinutes: [
                15,
                [Validators.required]
            ]
        });
    }

    load(): void {

        this.service.get()
            .subscribe((res: any) => {

                this.form!.patchValue(res.result);
            });
    }

    save(): void {

        if (this.form!.invalid) {

            this.form!.markAllAsTouched();

            return;
        }

        this.saving = true;

        this.service.update(this.form!.value)
            .pipe(
                finalize(() => {

                    this.saving = false;
                })
            )
            .subscribe(() => {

                abp.notify.success(
                    'Security policy saved successfully'
                );
            });
    }
}