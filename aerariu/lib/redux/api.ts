import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { HYDRATE } from 'next-redux-wrapper';
import { getAccessToken, getApiEndpoint } from '../utils/auth';

export const apiSlice = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: getApiEndpoint(),
    prepareHeaders: (headers, { getState }) => {
      const token = getAccessToken();

      if (token) {
        headers.set('Authorization', `Bearer ${token}`);
        return headers;
      }
    },
  }),
  extractRehydrationInfo(action, { reducerPath }) {
    if (action.type === HYDRATE) {
      return action.payload[reducerPath];
    }
  },
  endpoints: (build) => ({}),
});
