import { DoctorStatusEnum } from "../DoctorStatusEnum";

export class GetDoctorAndPhotoProfilesResponse {
  constructor (
    public id: string,
    public fullName: string,
    public specializationName: string,
    public experience: number,
    public address: string,
    public photo: any
  ) {}
}
