import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import { MdbModalService } from 'mdb-angular-ui-kit/modal';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { MdbModalConfig } from 'mdb-angular-ui-kit/modal';
import { MdbModalModule } from 'mdb-angular-ui-kit/modal';
import {MatButtonModule} from '@angular/material/button';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { DataService } from './services/data-service.service';
import { DatePipe } from '@angular/common';
import { MachineInfoModalComponent } from './machine-info-modal/machine-info-modal.component';
import { CreateMachineModalComponent } from './create-machine-modal/create-machine-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    NavBarComponent,
    MachineInfoModalComponent,
    CreateMachineModalComponent
  ],
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule, 
    HttpClientModule,
    MdbModalModule,
    MatTableModule, 
    MatIconModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'home-page' },
      { path: 'home-page', component: HomePageComponent },
    ])
  ],
  providers: [
    DataService,
    MdbModalService,
    DatePipe
  ],
  bootstrap: [
    AppComponent,
    NavBarComponent
  ]
})
export class AppModule { }
