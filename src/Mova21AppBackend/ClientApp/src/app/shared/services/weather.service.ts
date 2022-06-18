import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ServiceBase } from "./servicebase.service";
import { WeatherEntry } from "../models/weatherEntry";
import { WeatherEntries } from "../models/weatherEntries";
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: "root"
})
export class WeatherService extends ServiceBase {
  private url = "api/weather";  // URL to web api

  constructor(private http: HttpClient, public oidcSecurityService: OidcSecurityService) {
    super(oidcSecurityService);
  }

  /** GET invoices from the server */
  getEntriesByDateRange(startDate: Date, endDate: Date): Observable<WeatherEntries> {
    return this.http.get<WeatherEntries>(`${this.url}/${startDate.toISOString()}/${endDate.toISOString()}`, this.httpOptions());
  }

  updateEntry(entry: WeatherEntry): Observable<void> {
    return this.http.put<void>(`${this.url}`, entry, this.httpOptions());
  }

  addEntry(entry: WeatherEntry): Observable<WeatherEntry> {
    return this.http.post<WeatherEntry>(`${this.url}`, entry, this.httpOptions());
  }
}
