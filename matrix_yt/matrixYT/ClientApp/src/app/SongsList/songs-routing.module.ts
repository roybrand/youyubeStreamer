import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutSongComponent } from './layout-song.component';
import { SongListComponent } from './songList.component';
import { AddEditSongComponent } from './add-edit-song.component';

const routes: Routes = [
    {
        path: '', component: LayoutSongComponent,
        children: [
            { path: '', component: SongListComponent },
            { path: 'add', component: AddEditSongComponent },
            { path: 'edit/:id', component: AddEditSongComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SongsRoutingModule { }