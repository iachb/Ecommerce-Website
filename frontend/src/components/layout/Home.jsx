import React, { Fragment, useEffect } from "react";
import MetaData from "./MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProducts, getProductPagination } from "../../actions/productAction";
import { useAlert } from "react-alert";
import Products from "../products/Products";
import Pagination from "react-js-pagination";
import { setPageIndex } from "../../slices/productPaginationSlice";

const Home = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(getProducts());
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

  return (
    <Fragment>
      <MetaData title={"Best products online"} />
      <section id="products" className="mt-10">
        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
          <Products products={products} loading={loading} />
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
