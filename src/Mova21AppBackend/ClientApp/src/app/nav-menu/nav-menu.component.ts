import { Component, OnInit } from "@angular/core";
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

  constructor(public oidcSecurityService: OidcSecurityService) { }

  ngOnInit() {
    this.oidcSecurityService.checkAuth().subscribe(loginResponse => {
      console.warn("checkAuth(): isAuthenticated: " + loginResponse.isAuthenticated);
      console.warn("checkAuth(): userData: " + loginResponse.userData);
      console.warn("checkAuth(): accessToken: " + loginResponse.accessToken);
      console.warn("checkAuth(): idToken: " + loginResponse.idToken);
      console.warn("checkAuth(): errorMessage: " + loginResponse.errorMessage);

      this.oidcSecurityService.getPayloadFromIdToken(true).subscribe(x => {
        let json = JSON.parse(atob(x));
        let roles: string[] = json.resource_access["velo-backend"].roles;
        this.availablePermissions.canEditActivities = roles.includes('activity');
        this.availablePermissions.canEditBike = roles.includes('velo');
        this.availablePermissions.canEditWeather = roles.includes('wetter');

      });
    });
    this.oidcSecurityService.isAuthenticated$.subscribe(authenticationResult => {
      console.warn("isAuthenticated$: isAuthenticated: " + authenticationResult.isAuthenticated);
    });
    this.oidcSecurityService.isAuthenticated().subscribe(value => {
      console.warn("isAuthenticated(): value: " + value);
      this.isAuthenticated = value;
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
