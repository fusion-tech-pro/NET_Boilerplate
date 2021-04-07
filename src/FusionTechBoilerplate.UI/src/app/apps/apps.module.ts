import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppsComponent } from './apps.component';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TodoAppComponent } from './todo-app/todo-app.component';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';


const routes = [
  {
    path     : '',
    component: AppsComponent
  }
];

@NgModule({
  declarations: [
    AppsComponent,
    TodoAppComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),

    MatButtonModule,
    MatIconModule,
    FlexLayoutModule,

    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    MatCardModule
  ]
})
export class AppsModule { }
