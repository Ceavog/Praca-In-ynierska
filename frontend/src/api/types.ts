export interface IUser {
  id: number;
  login: string;
  role: string;
}

export class OrderRequest {
  productIds: number[];
}

export interface GenericResponse {
  status: string;
  message: string;
}

export interface ILoginResponse {
  status: string;
  accessToken: string;
}

export interface IUserResponse {
  status: number;
  data: IUser;
}

export interface ILoginVars {
  login: string;
  password: string;
}

