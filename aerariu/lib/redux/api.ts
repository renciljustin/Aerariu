import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { HYDRATE } from 'next-redux-wrapper';
import { getApiEndpoint } from '../utils/auth';

export const apiSlice = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: getApiEndpoint(),
  }),
  extractRehydrationInfo(action, { reducerPath }) {
    if (action.type === HYDRATE) {
      return action.payload[reducerPath];
    }
  },
  endpoints: (build) => ({}),
});
