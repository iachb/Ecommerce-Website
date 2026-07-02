import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilities/axios";
import { delayedTimeout } from "../utilities/delayedTimeout";
import { httpParams } from "../utilities/httpParams";

export const getProducts = createAsyncThunk(
  "products/getProducts",
  async (ThunkApi, { rejectWithValue }) => {
    try {
      return await axios.get(`/api/v1/Product/list`);
    } catch (err) {
      return rejectWithValue(`Errors: ${err.message}`);
    }
  },
);

export const getProductById = createAsyncThunk(
  "products/getProductById",
  async (id, { rejectWithValue }) => {
    try {
      return await axios.get(`/api/v1/Product/${id}`);
    } catch (err) {
      return rejectWithValue(`Errors: ${err.message}`);
    }
  },
);

export const getProductPagination = createAsyncThunk(
  "products/getProductPagination",
  async (params, { rejectWithValue }) => {
    try {
      params = httpParams(params);
      const paramUrl = new URLSearchParams(params).toString();
      var results = await axios.get(`/api/v1/Product/pagination?${paramUrl}`);
      return (await results).data;
    } catch (err) {
      return rejectWithValue(`Errors: ${err.message}`);
    }
  },
);
