import React from "react";
import { Link } from "react-router-dom";

const Product = ({ product }) => {
  const default_image = "./images/default_product.png";
  return (
    <div className="flex h-full flex-col rounded border border-gray-200 bg-white p-3 shadow-sm">
      <img
        className="mx-auto h-40 w-52 object-contain"
        src={product.images[0] ? product.images[0] : default_image}
        alt={product.name}
      />
      <div className="flex flex-1 flex-col pt-3">
        <h5 className="text-base">
          <Link
            to={`/product/${product.id}`}
            className="text-gray-800 hover:text-[#fa9c23]"
          >
            {product.name}
          </Link>
        </h5>

        <div className="mt-1 flex items-center">
          {/* rating-outer/-inner: FontAwesome star trick kept in App.css */}
          <div className="rating-outer">
            <div
              className="rating-inner"
              style={{ width: `${(product.rating / 5) * 100}%` }}
            ></div>
          </div>
          <span className="ml-2 text-sm text-gray-500">
            ({product.totalReviews} Reviews)
          </span>
        </div>

        <p className="mt-2 text-2xl font-semibold">${product.price}</p>

        <Link
          to={`/product/${product.id}`}
          className="mt-auto rounded bg-[#fa9c23] px-4 py-2 text-center text-white"
        >
          See Details
        </Link>
      </div>
    </div>
  );
};

export default Product;
