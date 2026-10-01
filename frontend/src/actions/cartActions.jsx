import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../utilies/axios";

export const saveAddressInfo = createAsyncThunk(
  "shoppingCart/saveAddressInfo",
  async (params, { rejectWithValue }) => {
    try {
      const requestConfig = {
        "Content-Type": "application/json",
      };

      const { data } = await axios.post(
        "/api/v1/Order/address",
        params,
        requestConfig,
      );
      return data;
    } catch (err) {
      return rejectWithValue(err.response.data.message);
    }
  },
);
