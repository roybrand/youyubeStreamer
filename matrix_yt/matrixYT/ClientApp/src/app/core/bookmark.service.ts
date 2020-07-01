import {throwError,  Observable } from 'rxjs';
import {catchError} from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { IBookmark } from '../shared/interfaces';

@Injectable({
   providedIn:'root'    
})
export class BookmarkService {
    constructor(private httpClient: HttpClient) { }
     //private baseUrl = window.location.origin + '/api/bookmark/';
     private baseUrl =  '/api/repos';

    createBookmark(bookmark: IBookmark): Observable<boolean> {
      return this.httpClient.post<boolean>(this.baseUrl,bookmark)
      
       .pipe(catchError(this.handleError));
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





 //const body = JSON.stringify(bookmark);
       //const headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });
      //  return this.httpClient.put<IBookmark>(this.baseUrl + '/bookmark', body, {
      //         headers: headerOptions