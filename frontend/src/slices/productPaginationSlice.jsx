import { createSlice } from "@reduxjs/toolkit";
import { getProductPagination } from "../actions/productAction";

export const initialState = {
  products: [],
  count: 0,
  pageIndex: 1,
  pageSize: 10,
  pageCount: 0,
  loading: false,
  resultByPage: 0,
  error: null,
  search: null,
  maxPrice: null,
  minPrice: null,
  category: null,
  rating: null,
};

export const productPaginationSlice = createSlice({
  name: "productPagination",
  initialState,
  reducers: {
    searchPagination: (state, action) => {
      state.search = action.payload.search;
      state.pageIndex = 1;
    },
    setPageIndex: (state, action) => {
      state.pageIndex = action.payload.pageIndex;
    },
    updatePrice: (state, action) => {
      state.minPrice = action.payload.price[0];
      state.maxPrice = action.payload.price[1];
    },
    resetPagination: (state, action) => {
      state.search = null;
      state.minPrice = null;
      state.maxPrice = null;
      state.category = null;
      state.rating = null;
      state.pageIndex = 1;
    },
    updateCategory: (state, action) => {
      state.category = action.payload.category;
    },
    updateRating: (state, action) => {
      state.rating = action.payload.rating;
    },
  },
  extraReducers: {
    [getProductPagination.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getProductPagination.fulfilled]: (state, { payload }) => {
      state.loading = false;
      state.products = payload.data;
      state.count = payload.count;
      state.pageIndex = payload.pageIndex;
      state.pageSize = payload.pageSize;
      state.pageCount = payload.pageCount;
      state.resultByPage = payload.resultByPage;
      state.error = null;
    },
    [getProductPagination.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const {
  searchPagination,
  setPageIndex,
  updatePrice,
  resetPagination,
  updateCategory,
  updateRating,
} = productPaginationSlice.actions;

export const productPaginationReducer = productPaginationSlice.reducer;
