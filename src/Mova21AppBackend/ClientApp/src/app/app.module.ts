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
import { WeatherComponent } from "./weather/weather.component";

import { CalendarModule } from "primeng/calendar";
import { SelectButtonModule } from "primeng/selectbutton";
import { ToastModule } from "primeng/toast";
import { MessagesModule } from "primeng/messages";
import { MessageModule } from "primeng/message";
import { TooltipModule } from "primeng/tooltip";
import { AuthConfigModule } from './auth/auth-config.module';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AutoLoginAllRoutesGuard } from 'angular-auth-oidc-client';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BikeComponent,
    WeatherComponent,
    UnauthorizedComponent,
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
      { path: "unauthorized", component: UnauthorizedComponent }
    ]),
    AuthConfigModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
