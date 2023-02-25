import { IUserRegisterDto } from '@/lib/dtos/UserDtos';
import { Nullable, StateWithStatus } from '@/lib/utils/common-types';
import { createSlice } from '@reduxjs/toolkit';
import { RootState } from '../store';

type Auth = {
  accessToken: Nullable<string>;
  isValidated: boolean;
  redirectToPath: Nullable<string>;
  userInfo: Nullable<IUserRegisterDto>;
};

const initialState: StateWithStatus<Auth> = {
  data: {
    accessToken: null,
    isValidated: false,
    redirectToPath: '/',
    userInfo: null,
  },
  status: {
    error: null,
    loading: false,
    success: null,
  },
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {},
  extraReducers(builder) {},
});

export const getAuthState = (state: RootState) => state.auth;

export default authSlice.reducer;
