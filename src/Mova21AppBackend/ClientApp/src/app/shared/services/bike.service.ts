import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BikeAvailability  } from "../models/bikeavailability";
import { Observable } from "rxjs";
import { ServiceBase } from "./servicebase.service";
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: "root"
})
export class BikeService extends ServiceBase {
  private url = "api/bike";  // URL to web api

  constructor(private http: HttpClient, public oidcSecurityService: OidcSecurityService) {
    super(oidcSecurityService);
  }

  /** GET invoices from the server */
  getAvailability(): Observable<BikeAvailability> {
    return this.http.get<BikeAvailability>(`${this.url}`, this.httpOptions());
  }

  update(updated: BikeAvailability): Observable<BikeAvailability> {
    return this.http.put<BikeAvailability>(`${this.url}`, updated, this.httpOptions());
  }
}
