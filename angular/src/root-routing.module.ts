import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayersComponent } from '@app/layers/layers.component';
import { PublicLayoutComponent } from '@app/layout/public-layout/public-layout.component';
import { HomeComponent } from '@app/public/home/home.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';

const routes: Routes = [
    {
        path: '',
        component: PublicLayoutComponent,
        children: [
            {
                path: '',
                component: HomeComponent
            }
        ]
    },
    {
        path: 'account',
        loadChildren: () => import('account/account.module').then(m => m.AccountModule), // Lazy load account module
        data: { preload: true }
    },
    {
        path: 'app',
        loadChildren: () => import('app/app.module').then(m => m.AppModule), // Lazy load account module
        data: { preload: true }
    },
    {
        path: 'layers',
        component: LayersComponent,
        canActivate: [AppRouteGuard],
        data: { permission: 'Pages.Users' }
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule],
    providers: []
})
export class RootRoutingModule { }
