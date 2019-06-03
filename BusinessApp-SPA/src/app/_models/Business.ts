import { Photo } from './Photo';
import { Video } from './Video';

export interface Business {
    id: number;
    name: string;
    description?: string;
    address?: string;
    officeNumber: string;
    userId: number;
    photoUrl: string;
    validTill: number;
    photos?: Photo[];
    video?: Video;
}
