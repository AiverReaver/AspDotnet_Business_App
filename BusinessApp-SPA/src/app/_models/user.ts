import { Business } from './Business';

export interface User {
    id: number;
    username: string;
    gender: string;
    created: string;
    lastActive: string;
    photoUrl: string;
    age?: number;
    contactNumber?: string;
    businesses?: Business[];
}
