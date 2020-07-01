import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { SongService } from '@app/_services';

@Component({ templateUrl: 'songList.component.html' })
export class SongListComponent implements OnInit {
    songs = null;

    constructor(private songService: SongService) {}

    ngOnInit() {
        this.songService.getAll()
            .pipe(first())
            .subscribe(songs => this.songs = songs);
    }

    deleteSong(id: string) {
        const user = this.songs.find(x => x.id === id);
        user.isDeleting = true;
        this.songService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.songs = this.songs.filter(x => x.id !== id) 
            });
    }
}