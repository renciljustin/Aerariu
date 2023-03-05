import { isDevelopment } from './common';

export const getApiEndpoint = () =>
  isDevelopment() ? 'https://localhost:7232/api' : 'https://aerariu.com/api';

export const getAuthEndpoint = () => [getApiEndpoint(), 'Auth'].join('/');

export const getAccessToken = () =>
  typeof localStorage !== 'undefined'
    ? localStorage.getItem('access-token')
    : null;

export const setAccessToken = (value: string) =>
  localStorage.setItem('access-token', value);

export const removeAccessToken = () => localStorage.removeItem('access-token');

export const getRefreshToken = () =>
  typeof localStorage !== 'undefined'
    ? localStorage.getItem('refresh-token')
    : null;

export const setRefreshToken = (value: string) =>
  localStorage.setItem('refresh-token', value);

export const removeRefreshToken = () =>
  localStorage.removeItem('refresh-token');
