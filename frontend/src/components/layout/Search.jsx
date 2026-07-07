import React from "react";
import { useState } from "react";
import { useDispatch } from "react-redux";
import {
  resetPagination,
  searchPagination,
} from "../../slices/productPaginationSlice";
import { useNavigate } from "react-router-dom";

const Search = () => {
  
  const navigate = useNavigate();
  const [keyword, setKeyword] = useState("");
  const dispatch = useDispatch();

  const searchHandler = (event) => {
    event.preventDefault();
    if (keyword.trim()) {
      dispatch(searchPagination({ search: keyword }));
    } else {
      dispatch(resetPagination());
    }
  };

  return (
    <form onSubmit={searchHandler} className="w-full max-w-md">
      <div className="flex items-center overflow-hidden rounded-full border border-gray-300 bg-white shadow-sm focus-within:border-blue-500 focus-within:ring-1 focus-within:ring-blue-500">
        <input
          type="text"
          placeholder="Search..."
          className="w-full px-4 py-2 text-sm text-gray-700 outline-none"
          onChange={(e) => setKeyword(e.target.value)}
        />
        <button
          type="submit"
          className="flex items-center justify-center px-4 py-2 text-gray-500 hover:text-blue-600"
        >
          <i className="fa fa-search"></i>
        </button>
      </div>
    </form>
  );
};

export default Search;
