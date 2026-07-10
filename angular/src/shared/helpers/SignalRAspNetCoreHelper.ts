import { AppConsts } from '@shared/AppConsts';

export class SignalRAspNetCoreHelper {
    static initSignalR(callback?: () => void): void {
        const encryptedAuthToken = SignalRAspNetCoreHelper.getCookieValue(AppConsts.authorization.encryptedAuthTokenName);

        abp.signalr = {
            autoConnect: true,
            connect: undefined as any,
            hubs: undefined as any,
            qs: AppConsts.authorization.encryptedAuthTokenName + '=' + encodeURIComponent(encryptedAuthToken),
            remoteServiceBaseUrl: AppConsts.remoteServiceBaseUrl,
            startConnection: undefined as any,
            url: '/signalr',
            withUrlOptions: {}
        };

        const script = document.createElement('script');
        if (callback) {
            script.onload = () => {
                callback();
            };
        }
        script.src = AppConsts.appBaseUrl + '/assets/abp/abp.signalr-client.js';
        document.head.appendChild(script);
    }

    private static getCookieValue(name: string): string {
        const nameEQ = name + '=';
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
            let cookie = cookies[i].trim();
            if (cookie.indexOf(nameEQ) === 0) {
                return cookie.substring(nameEQ.length);
            }
        }
        return '';
    }
}
