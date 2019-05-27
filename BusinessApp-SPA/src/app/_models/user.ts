import { Business } from './Business';
import { Photo } from './Photo';

export interface User {
    id: number;
    username: string;
    gender: string;
    created: string;
    lastActive: string;
    country: string;
    city: string;
    age?: number;
    contactNumber?: string;
    businesses?: Business[];
}
