//common
export type Nullable<T> = T | null | undefined;

export type ResponseData<T> = {
  message: string;
  resultData: T;
  statusCode: number;
};

//redux state
export type StateStatus = {
  loading: boolean;
  success: boolean;
  error: Nullable<string>;
};

export type StateWithStatus<T> = {
  data: Nullable<T>;
  status: StateStatus;
};

//exceptions

export type CustomErrorResponse = {
  data: string | ResponseData<string>;
  status: string;
  statusText: Nullable<string>;
};

export type CustomError = {
  code: string;
  message: string;
  response: CustomErrorResponse;
};
