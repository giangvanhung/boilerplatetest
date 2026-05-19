import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-public-layout',
  templateUrl: './public-layout.component.html',
  styleUrls: ['./public-layout.component.css']
})
export class PublicLayoutComponent extends AppComponentBase implements OnInit {

  isLoggedIn = false;

  constructor(
    injector: Injector
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.isLoggedIn = this.appSession.user != null;
  }

  logout(): void {
    abp.auth.clearToken();
    window.location.href = '/';
}
}