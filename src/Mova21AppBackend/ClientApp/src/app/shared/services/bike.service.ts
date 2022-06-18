import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BikeAvailabilities,  } from "../models/bikeavailabilities";
import { Observable } from "rxjs";
import { ServiceBase } from "./servicebase.service";
import { ChangeBikeAvailabilityCountModel } from "../models/changeBikeAvailabilityCountModel";
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
  getAvailabilities(): Observable<BikeAvailabilities> {
    return this.http.get<BikeAvailabilities>(`${this.url}`, this.httpOptions());
  }

  changeCount(delta: ChangeBikeAvailabilityCountModel): Observable<BikeAvailabilities> {
    return this.http.put<BikeAvailabilities>(`${this.url}`, delta, this.httpOptions());
  }
}
