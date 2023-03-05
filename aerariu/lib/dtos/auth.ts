export type AuthTokenDto = {
  accessToken: string;
  refreshToken: string;
};

export type IUserRegisterDto = {
  name: string;
  email: string;
  username: string;
  password: string;
};

export type IUserLoginDto = {
  username: string;
  password: string;
};
