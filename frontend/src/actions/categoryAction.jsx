import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const getCategories = createAsyncThunk(
  "category/getCategories",
  async (ThunkApi, { rejectWithValue }) => {
    try {
      return await axios.get(`/api/v1/Category/list`)
    } catch (err) {
      return rejectWithValue(err.message);
    }
  },
);