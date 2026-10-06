import { saveAddressInfo } from "../actions/cartAction";
import { createSlice } from "@reduxjs/toolkit";
import {
  login,
  register,
  update,
  updatePassword,
  getUser,
} from "../actions/userAction";

const initialState = {
  loading: false,
  errors: [],
  isAuthenticated: false,
  user: null,
  isUpdated: false,
  addressInfo: null,
};

export const securitySlice = createSlice({
  name: "security",
  initialState,
  reducers: {
    logout: (state, action) => {
      localStorage.removeItem("token");
      state.loading = false;
      state.errors = [];
      state.isAuthenticated = false;
      state.user = null;
      state.addressInfo = null;
    },
    resetUpdateStatus: (state, action) => {
      state.isUpdated = false;
    },
  },
  extraReducers: {
    // Login
    [login.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [login.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.user = payload;
      state.errors = [];
      state.isAuthenticated = true;
      state.addressInfo = payload.addressInfo;
    },
    [login.rejected]: (state, action) => {
      state.loading = false;
      state.errors = [action.payload];
      state.isAuthenticated = false;
      state.user = null;
    },

    // Register
    [register.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [register.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.user = payload;
      state.errors = [];
      state.isAuthenticated = true;
    },
    [register.rejected]: (state, action) => {
      state.loading = false;
      state.errors = [action.payload];
      state.isAuthenticated = false;
      state.user = null;
    },

    // Update
    [update.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [update.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.user = payload;
      state.errors = [];
      state.isUpdated = true;
      state.isAuthenticated = true;
    },
    [update.rejected]: (state, action) => {
      state.loading = false;
      state.errors = [action.payload];
      state.isAuthenticated = false;
      state.user = null;
    },

    // Update password
    [updatePassword.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [updatePassword.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.isUpdated = true;
    },
    [updatePassword.rejected]: (state, action) => {
      state.loading = false;
      state.errors = [action.payload];
      state.isAuthenticated = false;
      state.user = null;
    },

    // Load user
    [getUser.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [getUser.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.user = payload;
      state.errors = [];
      state.addressInfo = payload.addressInfo;
    },
    [getUser.rejected]: (state, action) => {
      state.loading = false;
      state.errors = [action.payload];
      state.isAuthenticated = false;
      state.user = null;
    },

    // Address
    [saveAddressInfo.pending]: (state) => {
      state.loading = true;
      state.errors = [];
    },
    [saveAddressInfo.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.errors = [];
      state.isUpdated = true;
      state.addressInfo = payload;
    },
    [saveAddressInfo.rejected]: (state, action) => {
      state.loading = false;
      state.errors = [action.payload];
      state.isAuthenticated = false;
      state.user = null;
    },
  },
});

export const {logout, resetUpdateStatus} = securitySlice.actions;
export const securityReducer = securitySlice.reducer;