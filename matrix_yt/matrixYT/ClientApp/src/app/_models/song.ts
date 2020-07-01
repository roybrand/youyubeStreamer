import { User } from './user';

export class Song {
    id: string;
    songName: string;
    songUrl: string;
    category: Category;
    user:User;
}

export class Category {
    id: string;
    categoryName: string;    
}