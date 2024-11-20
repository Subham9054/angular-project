import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginModule } from './login/login.module';
import { ApplicationModule } from './application/application.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptorService } from './interceptors/auth-interceptor.service';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { LoaderComponent } from './loader/loader.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ForgotpasswordComponent,
    PagenotfoundComponent,
    LoaderComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    LoginModule,
    ApplicationModule,
    BrowserAnimationsModule,
    
    
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true }, // Fix provider syntax here
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
