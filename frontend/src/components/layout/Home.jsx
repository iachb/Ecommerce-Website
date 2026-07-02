import React, { Fragment, useEffect } from "react";
import MetaData from "./MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProducts } from "../../actions/productAction";
import { useAlert } from "react-alert";
import Products from "../products/Products";

const Home = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(getProducts());
  }, [dispatch]);

  const { products, loading, error } = useSelector((state) => state.products);

  const alert = useAlert();

  useEffect(() => {
    if (error) {
      alert.error(error);
    }
  }, [error, alert]);

  return (
    <Fragment>
      <MetaData title={"Best products online"} />
      <section id="products" className="mt-10">
        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
          <Products products={products} loading={loading} />
        </div>
      </section>
    </Fragment>
  );
};

export default Home;
