import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";

export const login = createAsyncThunk(
  "user/login",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };
      const { data } = await axios.post(
        "api/v1/User/login",
        params,
        requestConfig,
      );

      localStorage.setItem("token", data.token);

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);

export const register = createAsyncThunk(
  "user/register",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "multipart/form-data", // Required for file uploads
        },
      };
      const { data } = await axios.post(
        "api/v1/User/register",
        params,
        requestConfig,
      );

      localStorage.setItem("token", data.token);

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);

export const update = createAsyncThunk(
  "user/update",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "multipart/form-data", // Required for file uploads
        },
      };
      const { data } = await axios.put(
        "api/v1/User/update",
        params,
        requestConfig,
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);

export const getUser = createAsyncThunk(
  "user/getUser",
  async (_, { rejectWithValue }) => {
    try {
      const { data } = await axios.get("api/v1/User/get-user-by-token");
      localStorage.setItem("token", data.token);
      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);

export const updatePassword = createAsyncThunk(
  "user/updatePassword",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };
      const { data } = await axios.put(
        "api/v1/User/update-password",
        params,
        requestConfig,
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);

export const forgotPassword = createAsyncThunk(
  "user/forgotPassword",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };
      const { data } = await axios.post(
        "api/v1/User/forgot-password",
        params,
        requestConfig,
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);

export const resetPassword = createAsyncThunk(
  "user/resetPassword",
  async ({ email, password, confirmPassword, token }, { rejectWithValue }) => {
    try {
      const requestConfig = {
        headers: {
          "Content-Type": "application/json",
        },
      };

      const request = { email, password, confirmPassword, token };
      const { data } = await axios.post(
        "api/v1/User/reset-password-by-token",
        request,
        requestConfig,
      );

      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);
