import { Photo } from './Photo';

export interface Business {
    id: number;
    name: string;
    description?: string;
    address?: string;
    officeNumber: string;
    photoUrl: string;
    photos?: Photo[];
}
