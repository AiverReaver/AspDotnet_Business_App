import { Business } from './Business';

export interface User {
    id: number;
    userName: string;
    gender: string;
    created: string;
    city: string;
    age?: number;
    contactNumber?: string;
    businesses?: Business[];
    roles?: string[];
}
