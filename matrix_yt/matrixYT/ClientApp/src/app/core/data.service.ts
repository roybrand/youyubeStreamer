import {Injectable} from '@angular/core';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable,  } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Repo} from '../shared/interfaces';


@Injectable({
    providedIn:'root'    
})

export class DataService{

    baseUrl =  '/api/';
    baseRepoUrl = this.baseUrl + 'repos';

    constructor(private http: HttpClient){

    }

    getRepo(id: string) : Observable<Repo> {
        return this.http.get<Repo>(this.baseRepoUrl + '/' + id)
            .pipe(
                catchError(this.handleError)
            );
    }

    private handleError(error: HttpErrorResponse) {
        console.error('server error:', error); 
        if (error.error instanceof Error) {
          let errMessage = error.error.message;
          return Observable.throw(errMessage);
        }
        return Observable.throw(error || 'ASP.NET Core server error');
    }
}