import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AuthGuard } from 'src/core/guards/auth.guards';
import { AuthInterceptor } from 'src/core/auth.interseptor';

const appRoutes: Routes = [
  {
    path        : 'auth', 
    loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule),
  },
  {
    path        : 'apps', 
    loadChildren: () => import('./apps/apps.module').then(m => m.AppsModule),
    canActivate: [ AuthGuard ]
  },
  {   
    path: "**",
    redirectTo:"/apps",
    pathMatch: "full"
  }
]

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)

  ],
  providers: [
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS, 
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
