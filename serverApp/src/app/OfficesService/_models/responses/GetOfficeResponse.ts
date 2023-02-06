import { OfficeStatusEnum } from "../OfficeStatusEnum";

export class GetOfficeResponse {
    constructor (
      public id: string,
      public address: string,
      public officeStatus?: OfficeStatusEnum

    ) {}
  }