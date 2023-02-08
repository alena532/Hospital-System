import { DoctorStatusEnum } from "../DoctorStatusEnum";

export class CreateDoctorProfileAndPhotoRequest {

    constructor (
      public firstName: string,
      public lastName: string,
      public middleName: string | null,
      public dateOfBirth: Date,
      public email: string,
      public officeId: string,
      public address: string,
      public careerStartYear: number,
      public phoneNumber: string,
      public status: DoctorStatusEnum,
      public specializationId: string,
      public specializationName: string,
      public photo: any
    ) {}
  }