import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { Song } from '@app/_models/song';

@Injectable({ providedIn: 'root' })
export class SongService {
    private songSubject: BehaviorSubject<Song>;
    public song: Observable<Song>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.songSubject = new BehaviorSubject<Song>(JSON.parse(localStorage.getItem('song')));
        this.song = this.songSubject.asObservable();
    }

    public get songValue(): Song {
        return this.songSubject.value;
    }

    register(song: Song) {
        
        return this.http.post(`${environment.apiUrl}/songs/register`, song);
    }
    

    getAll() {
        return this.http.get<Song[]>(`${environment.apiUrl}/songs`);
    }

    getById(id: string) {
        return this.http.get<Song>(`${environment.apiUrl}/songs/${id}`);
    }

    update(id, params) {
        return this.http.put(`${environment.apiUrl}/songs/${id}`, params)
            .pipe(map(x => {
                // update stored user if the logged in user updated their own record
                if (id == this.songValue.id) {
                    // update local storage
                    const song = { ...this.songValue, ...params };
                    localStorage.setItem('song', JSON.stringify(song));

                    // publish updated user to subscribers
                    this.songSubject.next(song);
                }
                return x;
            }));
    }

    delete(id: string) {
        return this.http.delete(`${environment.apiUrl}/songs/${id}`)
            .pipe(map(x => {
               
                return x;
            }));
    }
}