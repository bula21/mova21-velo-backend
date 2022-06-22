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

import { ButtonModule } from 'primeng/button';
import { CalendarModule } from "primeng/calendar";
import { CheckboxModule } from 'primeng/checkbox';
import { EditorModule } from 'primeng/editor';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from "primeng/message";
import { MessagesModule } from "primeng/messages";
import { ToastModule } from "primeng/toast";
import { TooltipModule } from "primeng/tooltip";
import { SelectButtonModule } from "primeng/selectbutton";

import { AutoLoginAllRoutesGuard } from 'angular-auth-oidc-client';
import { AbstractSecurityStorage } from 'angular-auth-oidc-client';

import { LMarkdownEditorModule } from 'ngx-markdown-editor';

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
  providers: [{ provide: AbstractSecurityStorage, useClass: LocalStorageService }],
  bootstrap: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ButtonModule,
    CalendarModule,
    CheckboxModule,
    EditorModule,
    InputTextModule,
    InputTextareaModule,
    LMarkdownEditorModule,
    MessageModule,
    MessagesModule,
    ToastModule,
    TooltipModule,
    SelectButtonModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "bike", component: BikeComponent, canActivate: [AutoLoginAllRoutesGuard] },
      { path: "weather", component: WeatherComponent, canActivate: [AutoLoginAllRoutesGuard] },
      { path: "activity", component: ActivitiesComponent, canActivate: [AutoLoginAllRoutesGuard] },
      { path: "unauthorized", component: UnauthorizedComponent }
    ]),
    AuthConfigModule
  ]
})
export class AppModule { }
