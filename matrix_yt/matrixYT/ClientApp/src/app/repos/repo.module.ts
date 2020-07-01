import { NgModule } from '@angular/core';
import { RepositoryListComponent } from './repo.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser'

@NgModule({
  declarations: [
    RepositoryListComponent
  ],
  imports: [
    RouterModule.forChild([
      { path: 'repos', component: RepositoryListComponent },
      
    ]),
    FormsModule,
    BrowserModule
  ]
})
export class RepoModule { }