import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { BikeComponent } from "./bike/bike.component";
import { WeatherComponent } from "./weather/weather.component"
import { ActivitiesComponent } from './activities/activities.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { LocalStorageService } from "./infrastructure/localStorageService";
import { AuthConfigModule } from './auth/auth-config.module';

import { CalendarModule } from "primeng/calendar";
import { SelectButtonModule } from "primeng/selectbutton";
import { ToastModule } from "primeng/toast";
import { MessagesModule } from "primeng/messages";
import { MessageModule } from "primeng/message";
import { TooltipModule } from "primeng/tooltip";

import { AutoLoginAllRoutesGuard } from 'angular-auth-oidc-client';
import { AbstractSecurityStorage } from 'angular-auth-oidc-client';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BikeComponent,
    WeatherComponent,
    UnauthorizedComponent,
    ActivitiesComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    CalendarModule,
    SelectButtonModule,
    ToastModule,
    MessagesModule,
    MessageModule,
    TooltipModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "bike", component: BikeComponent, canActivate: [AutoLoginAllRoutesGuard] },
      { path: "weather", component: WeatherComponent, canActivate: [AutoLoginAllRoutesGuard] },
      { path: "activity", component: ActivitiesComponent, canActivate: [AutoLoginAllRoutesGuard] },
      { path: "unauthorized", component: UnauthorizedComponent }
    ]),
    AuthConfigModule
  ],
  providers: [{ provide: AbstractSecurityStorage, useClass: LocalStorageService }],
  bootstrap: [AppComponent]
})
export class AppModule { }
