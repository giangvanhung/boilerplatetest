import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  UserServiceProxy,
  CreateUserDto,
  RoleDto
} from '@shared/service-proxies/service-proxies';
import { AbpValidationError } from '@shared/components/validation/abp-validation.api';

@Component({
  templateUrl: './create-user-dialog.component.html'
})
export class CreateUserDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  user = new CreateUserDto();
  roles: RoleDto[] | undefined = [] ;
  checkedRolesMap: { [key: string]: boolean } = {};
  defaultRoleCheckedStatus = false;
  passwordValidationErrors: AbpValidationError[] = [
    {
      name: 'pattern',
      localizationKey:
        'PasswordsMustBeAtLeast8CharactersContainLowercaseUppercaseNumber',
      propertyKey: 'Password'
    },
  ];
  confirmPasswordValidationErrors: AbpValidationError[] = [
    {
      name: 'validateEqual',
      localizationKey: 'PasswordsDoNotMatch',
      propertyKey: 'ConfirmPassword'
    },
  ];

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _userService: UserServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.user.isActive = true;

    this._userService.getRoles().subscribe((result) => {
      this.roles = result.items;
      this.setInitialRolesStatus();
    });
  }

  setInitialRolesStatus(): void {
    _map(this.roles, (item) => {
      this.checkedRolesMap[item.normalizedName as any] = this.isRoleChecked(
        item.normalizedName as any
      );
    });
  }

  isRoleChecked(normalizedName: string): boolean {
    // just return default role checked status
    // it's better to use a setting
    return this.defaultRoleCheckedStatus;
  }

  onRoleChange(role: RoleDto, $event) {
    this.checkedRolesMap[role.normalizedName as any] = $event.target.checked;
  }

  getCheckedRoles(): string[] {
    const roles: string[] = [];
    _forEach(this.checkedRolesMap, function (value, key) {
      if (value) {
        roles.push(key);
      }
    });
    return roles;
  }

  save(): void {
    this.saving = true;

    this.user.roleNames = this.getCheckedRoles();

    this._userService.create(this.user).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
