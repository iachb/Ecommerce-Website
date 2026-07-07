import React, { Fragment, useEffect, useState } from "react";
import MetaData from "./layout/MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProducts, getProductPagination } from "../actions/productAction";
import { getCategories } from "../actions/categoryAction";
import { useAlert } from "react-alert";
import Products from "./products/Products";
import Pagination from "react-js-pagination";
import {
  setPageIndex,
  updateCategory,
  updatePrice,
  updateRating,
} from "../slices/productPaginationSlice";
import Slider from "rc-slider";
import "rc-slider/assets/index.css";

const { createSliderWithTooltip } = Slider;
const Range = createSliderWithTooltip(Slider.Range);

const Home = () => {
  const [price, setPrice] = useState([1, 1000]);
  const dispatch = useDispatch();

  const { categories } = useSelector((state) => state.category);

  useEffect(() => {
    dispatch(getProducts());
    dispatch(getCategories());
  }, [dispatch]);

  const {
    products,
    count,
    pageIndex,
    loading,
    error,
    resultByPage,
    search,
    pageSize,
    minPrice,
    maxPrice,
    category,
    rating,
  } = useSelector((state) => state.productPagination);

  const alert = useAlert();

  useEffect(() => {
    if (error) {
      alert.error(error);
    }
  }, [error, alert]);

  useEffect(() => {
    dispatch(
      getProductPagination({
        pageIndex: pageIndex,
        pageSize: pageSize,
        search: search,
        minPrice: minPrice,
        maxPrice: maxPrice,
        categoryId: category,
        rating: rating,
      }),
    );
  }, [
    dispatch,
    search,
    pageIndex,
    pageSize,
    minPrice,
    maxPrice,
    category,
    rating,
  ]);

  function setCurrentPageNo(pageNumber) {
    dispatch(
      setPageIndex({
        pageIndex: pageNumber,
      }),
    );
  }

  function onChangePrice(value) {
    setPrice(value);
  }

  function onAfterChange(value) {
    dispatch(updatePrice({ price: value }));
  }

  function onChangeCategory(category) {
    dispatch(
      updateCategory({
        category: category.id,
      }),
    );
  }

  function onChangeStar(star) {
    dispatch(updateRating({ rating: star }));
  }

  return (
    <Fragment>
      <MetaData title={"Best products online"} />
      <section id="products" className="mt-10">
        <div className="flex flex-col gap-6 md:flex-row">
          <div className="w-full shrink-0 md:w-64">
            <div className="rounded-lg border border-gray-200 p-5">
              <Range
                marks={{ 1: "$1", 1000: "$1000" }}
                min={1}
                max={1000}
                defaultValue={[1, 1000]}
                tipFormatter={(value) => `$${value}`}
                value={price}
                tipProps={{
                  placement: "top",
                  visible: true,
                }}
                onChange={onChangePrice}
                onAfterChange={onAfterChange}
              />
              <hr className="mt-5" />
              <div className="mt-5">
                <h4 className="mb-3 text-lg font-medium">Categories</h4>
                <ul>
                  {categories.map((cat) => (
                    <li
                      key={cat.id}
                      className={`cursor-pointer py-1 hover:text-blue-500 ${
                        category === cat.id
                          ? "font-medium text-blue-600"
                          : "text-gray-700"
                      }`}
                      onClick={() => onChangeCategory(cat)}
                    >
                      {cat.name}
                    </li>
                  ))}
                </ul>
              </div>
              <hr className="my-5" />
              <div className="mt-5">
                <h4 className="mb-3">Ratings</h4>
                <ul className="pl-0">
                  {[5, 4, 3, 2, 1].map((star) => (
                    <li
                      key={star}
                      className={`cursor-pointer py-1 hover:text-blue-500 ${
                        rating === star ? "text-blue-600" : "text-gray-700"
                      }`}
                      onClick={() => onChangeStar(star)}
                    >
                      <div className="rating-outer">
                        <div
                          className="rating-inner"
                          style={{ width: `${star * 20}%` }}
                        ></div>
                      </div>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
          </div>
          <div className="grid flex-1 grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
            <Products products={products} loading={loading} />
          </div>
        </div>
      </section>
      <div className="flex justify-center mt-6">
        <Pagination
          activePage={pageIndex}
          itemsCountPerPage={pageSize}
          totalItemsCount={count}
          onChange={setCurrentPageNo}
          nextPageText={">"}
          prevPageText={"<"}
          firstPageText={"<<"}
          lastPageText={">>"}
          itemClass="page-item"
          linkClass="page-link"
        />
      </div>
    </Fragment>
  );
};

export default Home;
