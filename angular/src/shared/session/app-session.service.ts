import { Injectable } from '@angular/core';
import {
    ApplicationInfoDto,
    GetCurrentLoginInformationsOutput,
    SessionServiceProxy,
    TenantLoginInfoDto,
    UserLoginInfoDto
} from '@shared/service-proxies/service-proxies';

declare const abp: any;

@Injectable()
export class AppSessionService {

    private _user: UserLoginInfoDto | undefined;
    private _tenant!: TenantLoginInfoDto;
    private _application!: ApplicationInfoDto;

    constructor(private _sessionService: SessionServiceProxy) {
    }

    get application(): ApplicationInfoDto {
        return this._application;
    }

    get user(): UserLoginInfoDto | undefined {
        return this._user;
    }

    get userId(): number | null {
        return this.user ? this.user.id : null;
    }

    get tenant(): TenantLoginInfoDto {
        return this._tenant;
    }

    get tenantId(): number | null {
        return this.tenant ? this.tenant.id : null;
    }

    getShownLoginName(): string {
        const userName = this._user?.userName ?? '';
        if (!abp.multiTenancy.isEnabled) {
            return userName;
        }

        return (this._tenant ? this._tenant.tenancyName : '.') + '\\' + userName;
    }

    init(): Promise<boolean> {
        return new Promise<boolean>((resolve, reject) => {
            this._sessionService.getCurrentLoginInformations().toPromise().then((result: GetCurrentLoginInformationsOutput) => {
                this._application = result.application;
                this._user = result.user;
                this._tenant = result.tenant;

                resolve(true);
            }, (err) => {
                reject(err);
            });
        });
    }

    changeTenantIfNeeded(tenantId?: number): boolean {
        if (this.isCurrentTenant(tenantId)) {
            return false;
        }

        abp.multiTenancy.setTenantIdCookie(tenantId);
        location.reload();
        return true;
    }

    private isCurrentTenant(tenantId?: number) {
        if (!tenantId && this.tenant) {
            return false;
        } else if (tenantId && (!this.tenant || this.tenant.id !== tenantId)) {
            return false;
        }

        return true;
    }
}
