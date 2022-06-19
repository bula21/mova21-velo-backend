import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ServiceBase } from "./servicebase.service";
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AvailablePermissions } from '../models/availablepermissions';

@Injectable({
  providedIn: "root"
})
export class PermissionService extends ServiceBase {
  private url = "api/permission";  // URL to web api

  constructor(private http: HttpClient, public oidcSecurityService: OidcSecurityService) {
    super(oidcSecurityService);
  }

  /** GET invoices from the server */
  getAvailablePermission(): Observable<AvailablePermissions> {
    return this.http.get<AvailablePermissions>(`${this.url}`, this.httpOptions());
  }
}
