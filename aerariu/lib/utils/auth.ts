import { isDevelopment } from './common';

export const getApiEndpoint = () =>
  isDevelopment() ? 'http://localhost:5038/api' : 'https://aerariu.com/api';

export const getAuthEndpoint = () => [getApiEndpoint(), 'Auth'].join('/');

export const getAccessToken = () =>
  typeof localStorage !== 'undefined'
    ? localStorage.getItem('access-token')
    : null;

export const removeAccessToken = () => localStorage.removeItem('access-token');

export const setAccessToken = (value: string) =>
  localStorage.setItem('access-token', value);
