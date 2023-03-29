import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button'
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    NavBarComponent
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    MatTableModule, 
    MatIconModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'home-page' },
      { path: 'home-page', component: HomePageComponent },
    ])
  ],
  providers: [],
  bootstrap: [
    AppComponent,
    NavBarComponent
  ]
})
export class AppModule { }
