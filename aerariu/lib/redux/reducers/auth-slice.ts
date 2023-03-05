import {
  AuthenticatedUserDto,
  PartialUserInfoDto,
  UserLoginDto,
  UserRegisterDto,
} from '@/lib/dtos/auth';
import { handleError } from '@/lib/tools/exception';
import {
  getAuthEndpoint,
  removeAccessToken,
  removeRefreshToken,
  setAccessToken,
  setRefreshToken,
} from '@/lib/utils/auth';
import {
  Nullable,
  ResponseData,
  StateWithStatus,
} from '@/lib/utils/common-types';
import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';
import { RootState } from '../store';

type Auth = {
  accessToken: Nullable<string>;
  isValidated: boolean;
  redirectToPath: Nullable<string>;
  userInfo: Nullable<PartialUserInfoDto>;
};

export const registerUser = createAsyncThunk(
  'auth/register',
  async (userInfo: UserRegisterDto, { rejectWithValue }) => {
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

export const loginUser = createAsyncThunk<AuthenticatedUserDto, UserLoginDto>(
  'auth/login',
  async ({ username, password }: UserLoginDto, { rejectWithValue }) => {
    try {
      const response = await axios.post<ResponseData<AuthenticatedUserDto>>(
        `${getAuthEndpoint()}/Login`,
        { username, password },
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      setAccessToken(response.data.resultData.accessToken);
      setRefreshToken(response.data.resultData.refreshToken);
      return response.data.resultData;
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
  reducers: {
    logout: (state) => {
      if (Boolean(localStorage)) {
        removeAccessToken();
        removeRefreshToken();
      }

      state.status.error = null;
      state.status.loading = false;
      state.status.success = false;

      if (state.data) {
        state.data.accessToken = null;
        state.data.isValidated = true;
        state.data.userInfo = null;
        state.data.redirectToPath = null;
      }
    },
  },
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

        state.data.userInfo = payload.user;
        state.data.accessToken = payload.accessToken;
        state.data.isValidated = true;
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
