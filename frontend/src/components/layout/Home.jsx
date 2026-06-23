import React, { Fragment, useEffect } from "react";
import MetaData from "./MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProducts } from "../../actions/productAction";
import Product from "../product/Product";
import Loader from "./Loader";
import { useAlert } from "react-alert";

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
          {loading ? (
            <Loader />
          ) : products && products.length > 0 ? (
            products.map((product) => (
              <Product key={product._id} product={product} col={4} />
            ))
          ) : (
            <h1>No products found</h1>
          )}
        </div>
      </section>
    </Fragment>
  );
};

export default Home;
