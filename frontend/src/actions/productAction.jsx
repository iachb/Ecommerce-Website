import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from '../utilities/axios';
import { delayedTimeout } from "../utilities/delayedTimeout";

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