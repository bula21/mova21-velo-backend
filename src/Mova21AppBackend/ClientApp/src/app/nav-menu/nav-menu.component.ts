import { Component, OnInit } from "@angular/core";
import { PermissionService } from "../shared/services/permission.service";
import { AvailablePermissions } from "../shared/models/availablepermissions";

import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"]
})
export class NavMenuComponent implements OnInit {
  availablePermissions: AvailablePermissions = { canEditActivities: false, canEditBike: false, canEditWeather: false };
  isAuthenticated = false;
  isExpanded = false;

  constructor(public oidcSecurityService: OidcSecurityService, private permissionService: PermissionService) { }

  ngOnInit() {
    this.permissionService.getAvailablePermission().subscribe(availablePermissions => {
      this.availablePermissions = availablePermissions;
    });
    this.oidcSecurityService.checkAuth().subscribe(loginResponse => {
      //this.isAuthenticated = loginResponse.isAuthenticated;
      console.warn("checkAuth(): isAuthenticated: " + loginResponse.isAuthenticated);
      console.warn("checkAuth(): userData: " + loginResponse.userData);
      console.warn("checkAuth(): accessToken: " + loginResponse.accessToken);
      console.warn("checkAuth(): idToken: " + loginResponse.idToken);
      console.warn("checkAuth(): errorMessage: " + loginResponse.errorMessage);
    });
    this.oidcSecurityService.isAuthenticated$.subscribe(authenticationResult => {
      console.warn("isAuthenticated$: isAuthenticated: " + authenticationResult.isAuthenticated);
    });
    this.oidcSecurityService.isAuthenticated().subscribe(value => {
      console.warn("isAuthenticated(): value: " + value);
      this.isAuthenticated = value;
      this.permissionService.getAvailablePermission().subscribe(availablePermissions => {
        this.availablePermissions = availablePermissions;
      });
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.availablePermissions = { canEditActivities: false, canEditBike: false, canEditWeather: false };
    this.isAuthenticated = false;
    this.oidcSecurityService.logoffLocal();
  }
}
