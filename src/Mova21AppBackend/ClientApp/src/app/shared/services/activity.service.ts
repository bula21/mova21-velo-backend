import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ServiceBase } from "./servicebase.service";
import { Activity } from "../models/activity";
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: "root"
})
export class ActivityService extends ServiceBase {
  private url = "api/activity";  // URL to web api

  constructor(private http: HttpClient, public oidcSecurityService: OidcSecurityService) {
    super(oidcSecurityService);
  }

  createActivity(activity: Activity): Observable<Activity> {
    return this.http.post<Activity>(`${this.url}`, activity, this.httpOptions());
  }
}
