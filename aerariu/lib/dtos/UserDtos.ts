export interface IUserRegisterDto {
  name: string;
  email: string;
  username: string;
  password: string;
}

export interface IUserLoginDto {
  username: string;
  password: string;
}
