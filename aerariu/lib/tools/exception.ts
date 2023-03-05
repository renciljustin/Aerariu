import { AxiosError } from 'axios';
import {
  CustomError,
  MiddlewareErrorResponse,
  ResponseData,
} from '../utils/common-types';

export const handleError = (error: Error) => {
  if (error instanceof AxiosError && error.response && error.response.data) {
    const data = error.response.data;

    if (typeof data === 'string') return data;

    if (isMiddlewareErrorResponse(data)) return data.errors;

    if (isResponseData(data)) return data.message;
  }

  return error.message;
};

function isMiddlewareErrorResponse(data: any): data is MiddlewareErrorResponse {
  return typeof data === 'object' && 'errors' in data;
}

function isResponseData(data: any): data is ResponseData<string> {
  return typeof data === 'object' && 'message' in data;
}
