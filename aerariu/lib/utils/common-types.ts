//commons

export type Nullable<T> = T | null | undefined;

export type ResponseData<T> = {
  message: string;
  resultData: T;
  statusCode: number;
};

export type MiddlewareErrorData = {
  readonly [key: string]: string[];
};

export type MiddlewareErrorResponse = {
  errors: MiddlewareErrorData;
  status: number;
  statusText: string;
};

//exceptions

export type CustomErrorResponse = {
  data: string | MiddlewareErrorResponse | ResponseData<string>;
  status: string;
  statusText: Nullable<string>;
};

export type CustomError = {
  code: string;
  message: string;
  response: CustomErrorResponse;
};

//redux state

export type StateStatus = {
  loading: boolean;
  success: boolean;
  error: Nullable<string>;
};

export type StateWithStatus<T> = {
  data: T;
  status: StateStatus;
};
