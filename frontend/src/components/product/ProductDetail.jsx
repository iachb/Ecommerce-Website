import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { getProductById } from "../../actions/productAction";
import { useAlert } from "react-alert";
import Loader from "../layout/Loader";

const ProductDetail = () => {
  const dispatch = useDispatch();
  const alert = useAlert();

  const { id } = useParams();
  const { loading, error, product } = useSelector((state) => state.product);

  const [current, setCurrent] = useState(0);
  const [showReview, setShowReview] = useState(false);

  useEffect(() => {
    dispatch(getProductById(id));
    if (error != null) {
      alert.error(error);
    }
  }, [dispatch, id, error, alert]);

  if (!product || loading) {
    return <Loader />;
  }

  const images = product.images?.length ? product.images : [];
  const prev = () => setCurrent((c) => (c === 0 ? images.length - 1 : c - 1));
  const next = () => setCurrent((c) => (c === images.length - 1 ? 0 : c + 1));

  return (
    <div className="grid grid-cols-1 gap-10 py-10 lg:grid-cols-2">
      {/* Image carousel */}
      <div>
        <div className="relative">
          {images.length > 0 && (
            <img
              src={images[current].url}
              alt={product.name}
              className="w-full rounded"
            />
          )}
          {images.length > 1 && (
            <>
              <button
                onClick={prev}
                aria-label="Previous image"
                className="absolute left-2 top-1/2 -translate-y-1/2 rounded-full bg-black/40 px-3 py-1 text-white hover:bg-black/60"
              >
                ‹
              </button>
              <button
                onClick={next}
                aria-label="Next image"
                className="absolute right-2 top-1/2 -translate-y-1/2 rounded-full bg-black/40 px-3 py-1 text-white hover:bg-black/60"
              >
                ›
              </button>
            </>
          )}
        </div>
        {images.length > 1 && (
          <div className="mt-3 flex justify-center gap-2">
            {images.map((img, i) => (
              <button
                key={img.id ?? i}
                onClick={() => setCurrent(i)}
                aria-label={`Go to image ${i + 1}`}
                className={`h-2.5 w-2.5 rounded-full ${
                  i === current ? "bg-[#fa9c23]" : "bg-gray-300"
                }`}
              />
            ))}
          </div>
        )}
      </div>

      {/* Details */}
      <div>
        <h3 className="text-2xl font-semibold">{product.name}</h3>
        <p className="text-sm text-gray-500">Product # {product.id}</p>

        <hr className="my-4" />

        <div className="flex items-center">
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

        <hr className="my-4" />

        <p className="text-3xl font-bold">${product.price}</p>

        <div className="mt-3 flex items-center gap-4">
          <div className="inline-flex items-center">
            <button className="rounded-l bg-red-500 px-3 py-1 text-white">
              -
            </button>
            <input
              type="number"
              className="w-12 border-y border-gray-300 py-1 text-center outline-none"
              value="1"
              readOnly
            />
            <button className="rounded-r bg-blue-600 px-3 py-1 text-white">
              +
            </button>
          </div>

          <button
            type="button"
            className="rounded-full bg-[#fa9c23] px-8 py-2 text-white"
          >
            Add to Cart
          </button>
        </div>

        <hr className="my-4" />

        <p>
          Status:{" "}
          <span
            className={
              product.stock > 0
                ? "font-bold text-green-600"
                : "font-bold text-red-600"
            }
          >
            {product.stock > 0 ? "In Stock" : "Out of Stock"}
          </span>
        </p>

        <hr className="my-4" />

        <h4 className="text-lg font-medium">Description:</h4>
        <p className="text-gray-700">{product.description}</p>

        <hr className="my-4" />

        <p className="text-sm text-gray-500">
          Sold by: <strong>{product.vendor}</strong>
        </p>

        <button
          type="button"
          onClick={() => setShowReview(true)}
          className="mt-4 rounded-full bg-[#fa9c23] px-8 py-2 text-white"
        >
          Submit Your Review
        </button>
      </div>

      {/* Review modal */}
      {showReview && (
        <div
          className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4"
          onClick={() => setShowReview(false)}
        >
          <div
            className="w-full max-w-md rounded bg-white p-6"
            onClick={(e) => e.stopPropagation()}
          >
            <div className="mb-4 flex items-center justify-between">
              <h5 className="text-lg font-semibold">Submit Review</h5>
              <button
                onClick={() => setShowReview(false)}
                aria-label="Close"
                className="text-2xl leading-none text-gray-500 hover:text-gray-800"
              >
                &times;
              </button>
            </div>

            <ul className="stars">
              {Array.from({ length: 5 }, (_, i) => (
                <li key={i} className="star">
                  <i className="fa fa-star" />
                </li>
              ))}
            </ul>

            <textarea
              name="review"
              id="review"
              className="mt-3 w-full rounded border border-gray-300 p-2 outline-none"
            ></textarea>

            <button
              onClick={() => setShowReview(false)}
              className="float-right mt-3 rounded-full bg-[#fa9c23] px-6 py-2 text-white"
            >
              Submit
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default ProductDetail;
