import { Photo } from './IPhoto';

export interface Member {
  Id: number;
  username: string;
  gender: string;
  dateOfBirth: string;
  photoUrl: string;
  knownAs: string;
  created: Date;
  lastActive: Date;
  introduction: string;
  lookingFor: string;
  interests: string;
  city: string;
  country: string;
  photos: Photo[];
}
