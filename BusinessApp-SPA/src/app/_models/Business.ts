import { Photo } from './Photo';
import { Video } from './Video';

export interface Business {
    id: number;
    name: string;
    description?: string;
    address?: string;
    officeNumber: string;
    photoUrl: string;
    photos?: Photo[];
    video?: Video;
}
