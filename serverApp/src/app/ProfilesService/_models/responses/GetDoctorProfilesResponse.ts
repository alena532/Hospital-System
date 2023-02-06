import { DoctorStatusEnum } from "../DoctorStatusEnum";

export class GetDoctorProfilesResponse {

    constructor (
      public id: string,
      public firstName: string,
      public lastName: string,
      public middleName: string | null,
      public dateOfBirth: Date,
      public careerStartYear: number,
      public status: DoctorStatusEnum,
      public specializationId: string,
      public specializationName: string,
      public address: string,
      public officeId: string,
      public accountId: string,
    ) {}
  }