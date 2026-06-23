import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from '../utilities/axios';
import { delayedTimeout } from "../utilities/delayedTimeout";

export const getProducts = createAsyncThunk(
  "products/getProducts",
  async (ThunkApi, { rejectWithValue }) => {
    try {
      await delayedTimeout(1000); // Simulate a delay of 1 second
      return await axios.get(`/api/v1/Product/list`);
    } catch (err) {
      return rejectWithValue(`Errors: ${err.message}`);
    }
  },
);