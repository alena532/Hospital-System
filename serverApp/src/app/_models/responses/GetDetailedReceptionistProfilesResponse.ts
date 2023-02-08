import { Guid } from "guid-typescript";

export class GetDetailedReceptionistProfilesResponse {
    constructor (
      public id: Guid,
      public email: string,
      public firstName: string,
      public lastName: string,
      public middleName: string | null,
      public address: string | null,
      public officeId: Guid,
      public accountId: Guid,
      public Role: string,
    ) {}
  }