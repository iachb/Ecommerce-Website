import React, { Fragment } from "react";
import Loader from "../layout/Loader";
import Product from "../product/Product";

const Products = ({ products, col, loading }) => {
  if (loading) {
    return <Loader />;
  }

  return (
    <Fragment>
      {products ? (
        products.map((product) => (
          <Product key={product._id} product={product} col={col} />
        ))
      ) : (
        <Loader />
      )}
    </Fragment>
  );
};

export default Products;
