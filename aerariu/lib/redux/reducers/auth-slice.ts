import { IUserLoginDto, IUserRegisterDto } from '@/lib/dtos/UserDtos';
import { handleError } from '@/lib/tools/exception';
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
    } catch (error) {
      const result = handleError(error as Error);
      return rejectWithValue(result);
    }
  }
);

export const loginUser = createAsyncThunk(
  'auth/login',
  async ({ username, password }: IUserLoginDto, { rejectWithValue }) => {
    try {
      const data = await axios.post(
        `${getAuthEndpoint()}/Login`,
        { username, password },
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
    } catch (error) {
      const result = handleError(error as Error);
      return rejectWithValue(result);
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
      //register reducer
      .addCase(registerUser.pending, (state) => {
        state.status.loading = true;
        state.status.error = null;
      })
      .addCase(registerUser.fulfilled, (state, { payload }) => {
        state.status.loading = false;
        state.status.success = true;
        //TODO: Save userinfo - authentication token on payload or call login reducer instead.
      })
      .addCase(registerUser.rejected, (state, { payload }) => {
        state.status.loading = false;
        state.status.success = false;
        state.status.error = payload as string;
      })
      //login reducer
      .addCase(loginUser.pending, (state) => {
        state.status.loading = true;
        state.status.error = null;
      })
      .addCase(loginUser.fulfilled, (state, { payload }) => {
        state.status.loading = false;
        state.status.success = true;
        //TODO: Save userinfo to payload
      })
      .addCase(loginUser.rejected, (state, { payload }) => {
        state.status.loading = false;
        state.status.success = false;
        state.status.error = payload as string;
      });
  },
});

export const getAuthState = (state: RootState) => state.auth;

export default authSlice.reducer;
