import React, { Fragment, useEffect } from "react";
import MetaData from "./MetaData";
import { useDispatch, useSelector } from "react-redux";
import { getProducts } from "../../actions/productAction";
import Product from "../product/Product";

const Home = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(getProducts());
  }, [dispatch]);

  const { products } = useSelector((state) => state.products);

  return (
    <Fragment>
      <MetaData title={"Best products online"} />
      <section id="products" className="container mt-5">
        <div className="row">
          {products ? (
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
