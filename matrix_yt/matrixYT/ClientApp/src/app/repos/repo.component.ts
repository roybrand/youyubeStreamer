import { Component, OnInit } from '@angular/core';
import{Repo, Item,IBookmark} from '../shared/interfaces'
import { DataService } from '../core/data.service';
import { BookmarkService } from '../core/bookmark.service';
import { stringify } from 'querystring';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  templateUrl: './repo.component.html'
})
export class RepositoryListComponent  {
  pageTitle: string = 'Repos List';
  imageWidth: number = 50;
  imageMargin: number = 2;
  showImage: boolean = false;
  errorMessage: string;
  items: Item[] = [];
  bookmark:IBookmark;
 
  

  _repoName: string;
  get repoName(): string {
    return this._repoName;
  }
  set repoName(value: string) {
    this._repoName = value;
  }
  
  constructor(private router: Router,private dataService: DataService,private bookmarkService:BookmarkService) { }



  saveRepoSession(bookmark_id:string,bookmark_name:string,bookmark_avatar:string): void {    

    this.bookmark = {id:bookmark_id,name:bookmark_name,avatar:bookmark_avatar}
    
    this.bookmarkService.createBookmark(this.bookmark).subscribe((status:boolean)=>{

      if (status) {
        this.router.navigate(['/repos']);
      }
      else {
        this.errorMessage = 'Unable to save reppo in session';
      }
    },
    (err) => console.log(err));
    
  }
  

  getRepo(): void {

    this.dataService.getRepo(this._repoName).subscribe({
      next: repo => {
        this.items = repo.items;        
      },
      error: err => this.errorMessage = err
    });

    
  }

  public trackItem (index: number, item: Item) {
    return `${index}-${item.id}`; 
    
  }

}
