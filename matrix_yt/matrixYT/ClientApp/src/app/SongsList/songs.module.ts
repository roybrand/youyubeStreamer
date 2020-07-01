import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { SongsRoutingModule } from './songs-routing.module';
import { LayoutSongComponent } from './layout-song.component';
import { SongListComponent } from './songList.component';
import { AddEditSongComponent } from './add-edit-song.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        SongsRoutingModule
    ],
    declarations: [
        LayoutSongComponent,
        SongListComponent,
        AddEditSongComponent
    ]
})
export class SongsModule { }