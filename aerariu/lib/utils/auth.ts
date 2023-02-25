import { isDevelopment } from './common';

export const getApiEndpoint = () =>
  isDevelopment() ? 'http://localhost:5038/api' : 'https://aerariu.com/api';
