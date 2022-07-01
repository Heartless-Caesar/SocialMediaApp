import { Photo } from './IPhoto';

export interface Member {
  Id: number;
  Username: string;
  Gender: string;
  DateOfBirth: string;
  KnownAs: string;
  Created: Date;
  LastActive: Date;
  Introduction: string;
  LookingFor: string;
  Interests: string;
  City: string;
  Country: string;
  Photos: Photo[];
}
