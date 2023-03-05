export type AuthenticatedUserDto = {
  user: PartialUserInfoDto;
  accessToken: string;
  refreshToken: string;
};

export type PartialUserInfoDto = {
  id: string;
  username: string;
  email: string;
};

export type UserLoginDto = {
  username: string;
  password: string;
};

export type UserRegisterDto = {
  name: string;
  email: string;
  username: string;
  password: string;
};
