import { Guid } from "guid-typescript";

export class User {
    constructor (
      public email: string,
      public id: string,
      public firstName: string,
      public lastName: string,
      public middleName: string | null,
      public phoneNumber: string | null,
      public dateOfBirth: Date,
      public accountId: Guid,
    ) {}
  }