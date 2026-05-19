export class MenuItem {
  id: number | undefined;
  parentId: number | undefined;
  label: string;
  route: string;
  icon: string;
  permissionName: string;
  isActive?: boolean;
  isCollapsed?: boolean;
  children: MenuItem[];

  constructor(
    label: string,
    route: string,
    icon: string,
    permissionName: string = null as any,
    children: MenuItem[] = null as any
  ) {
    this.label = label;
    this.route = route;
    this.icon = icon;
    this.permissionName = permissionName;
    this.children = children;
  }
}
