import { NgModule } from '@angular/core';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';


@NgModule({
  imports: [AuthModule.forRoot({
    //config: {
    //  authority: 'https://auth.bula21.ch/auth/realms/Test/protocol/openid-connect/auth',
    //  redirectUrl: window.location.origin,
    //  postLogoutRedirectUri: window.location.origin,
    //  unauthorizedRoute: '/unauthorized',
    //  clientId: 'test-app-velo',
    //  scope: 'openid profile email offline_access', // 'openid profile ' + your scopes
    //  responseType: 'code',
    //  silentRenew: true,
    //  authWellknownEndpointUrl: "https://auth.bula21.ch/auth/realms/Test/.well-known/openid-configuration",
    //  silentRenewUrl: window.location.origin + '/silent-renew.html',
    //  useRefreshToken: true,
    //  logLevel: LogLevel.Debug,
    //  renewTimeBeforeTokenExpiresInSeconds: 10
    //}
    config: {
      authority: 'https://auth.bula21.ch/auth/realms/Test/protocol/openid-connect/auth',
      redirectUrl: window.location.origin,
      postLogoutRedirectUri: window.location.origin,
      unauthorizedRoute: '/unauthorized',
      clientId: 'test-app-velo',
      scope: 'openid profile email',
      responseType: 'id_token token',
      silentRenewUrl: `${window.location.origin}/silent-renew.html`,
      startCheckSession: true,
      silentRenew: true,
      authWellknownEndpointUrl: "https://auth.bula21.ch/auth/realms/Test/.well-known/openid-configuration",
      logLevel: LogLevel.Warn,
    },
  })],
  exports: [AuthModule],
})
export class AuthConfigModule { }
