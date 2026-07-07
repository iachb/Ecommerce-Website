import { getCategories } from "../actions/categoryAction";

export const initialState = {
  categories: [],
  loading: false,
  error: null,
};

export const categorySlice = createSlice({
  name: "category",
  initialState,
  reducers: {},
  extraReducers: {
    [getCategories.pending]: (state) => {
      state.loading = true;
      state.error = null;
    },
    [getCategories.fulfilled]: (state, {payload}) => {
      state.loading = false;
      state.categories = payload.data;
    },
    [getCategories.rejected]: (state, action) => {
      state.loading = false;
      state.error = action.payload;
    }
  },
});

export const categoryReducer = categorySlice.reducer;