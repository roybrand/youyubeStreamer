export interface Repo {
    items?: Item[];
}

export interface Owner {
    avatar_url:string; 
}

export interface Item {
    id:string
    name:string;
    owner?:Owner;
    
}

export interface IBookmark {
    id:string
    name:string;
    avatar:string;
    
}
  