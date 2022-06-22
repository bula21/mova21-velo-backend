import { NgModule } from '@angular/core';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';


@NgModule({
  imports: [AuthModule.forRoot({
    config: {
      authority: 'https://auth.bula21.ch/auth/realms/master/protocol/openid-connect/auth',
      redirectUrl: window.location.origin,
      postLogoutRedirectUri: window.location.origin,
      unauthorizedRoute: '/unauthorized',
      clientId: 'velo-backend',
      scope: 'openid profile email',
      responseType: 'id_token token',
      silentRenewUrl: `${window.location.origin}/silent-renew.html`,
      silentRenew: true,
      authWellknownEndpointUrl: "https://auth.bula21.ch/auth/realms/master/.well-known/openid-configuration",
      logLevel: LogLevel.Warn,
    }
  })],
  exports: [AuthModule],
})
export class AuthConfigModule { }
