import React, { Fragment } from "react";
import Loader from "../layout/Loader";
import Product from "../product/Product";

const Products = ({ products, loading }) => {
  if (loading) {
    return <Loader />;
  }

  return (
    <Fragment>
      {products ? (
        products.map((product) => (
          <Product key={product.id} product={product} />
        ))
      ) : (
        <Loader />
      )}
    </Fragment>
  );
};

export default Products;
