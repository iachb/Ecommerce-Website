import React, { Fragment, useEffect } from "react";
import MetaData from "./MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProducts } from "../../actions/productAction";
import Product from "../product/Product";
import Loader from "./Loader";
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
      <section id="products" className="container mt-5">
        <div className="row">
          <Products products={products} col={4} loading={loading} />
        </div>
      </section>
    </Fragment>
  );
};

export default Home;
