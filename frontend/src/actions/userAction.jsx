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
      const { data } = await axios.post("api/v1/User/login", params, requestConfig);

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
      const { data } = await axios.post("api/v1/User/register", params, requestConfig);

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
      const { data } = await axios.put("api/v1/User/update", params, requestConfig);

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
      const { data } = await axios.get("api/v1/User");
      localStorage.setItem("token", data.token);
      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);