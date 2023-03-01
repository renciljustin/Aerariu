import { IUserRegisterDto } from '@/lib/dtos/UserDtos';
import { logToConsole } from '@/lib/tools/logger';
import { getAuthEndpoint } from '@/lib/utils/auth';
import { Nullable, StateWithStatus } from '@/lib/utils/common-types';
import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';
import { RootState } from '../store';

type Auth = {
  accessToken: Nullable<string>;
  isValidated: boolean;
  redirectToPath: Nullable<string>;
  userInfo: Nullable<IUserRegisterDto>;
};

export const registerUser = createAsyncThunk(
  'auth/register',
  async (userInfo: IUserRegisterDto, { rejectWithValue }) => {
    try {
      await axios.post(`${getAuthEndpoint()}/Register`, userInfo, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
    } catch (error: any) {
      if (error.response && error.response.data) {
        return rejectWithValue(error.response.data.message);
      } else {
        return rejectWithValue(error.message);
      }
    }
  }
);

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
    success: false,
  },
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder
      //register reducers
      .addCase(registerUser.pending, (state) => {
        state.status.loading = true;
        state.status.error = null;
      })
      .addCase(registerUser.fulfilled, (state, { payload }) => {
        state.status.loading = false;
        state.status.success = true;
        //TODO: Save userinfo - authentication token on payload or call login reducer instead.
      })
      .addCase(registerUser.rejected, (state, { payload }: any) => {
        state.status.loading = false;
        state.status.success = false;
        state.status.error = payload;
      });
  },
});

export const getAuthState = (state: RootState) => state.auth;

export default authSlice.reducer;
