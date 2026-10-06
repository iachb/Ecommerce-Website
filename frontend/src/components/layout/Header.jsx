import React, { Fragment } from "react";
import Search from "./Search";
import { useDispatch, useSelector } from "react-redux";
import { useAlert } from "react-alert";
import { Link } from "react-router-dom";
import { logout } from "../../slices/securitySlice";

const Header = () => {
  const { user, loading } = useSelector((state) => state.security);
  const dispatch = useDispatch();
  const alert = useAlert();

  const logoutHandler = () => {
    dispatch(logout());
    alert.success("Logged out successfully");
  };

  return (
    <Fragment>
      <nav className="flex flex-wrap items-center gap-4 bg-navy px-6 py-3">
        <div className="shrink-0">
          <Link to="/">
            <img src="/images/logo_vaxi.png" alt="logo" className="h-10" />
          </Link>
        </div>

        <div className="order-3 flex w-full justify-center md:order-none md:min-w-0 md:flex-1">
          <Search />
        </div>

        <div className="ml-auto flex shrink-0 items-center gap-4 md:ml-0">
          <Link to="/cart" className="flex items-center gap-2 text-white">
            Cart
            <span className="rounded bg-brand px-2 py-0.5 text-sm font-bold text-black">
              2
            </span>
          </Link>

          {user ? (
            <details className="relative">
              <summary className="flex cursor-pointer list-none items-center gap-2 text-white [&::-webkit-details-marker]:hidden">
                <figure className="h-8 w-8 overflow-hidden rounded-full bg-brand">
                  {user.avatar ? (
                    <img
                      src={user.avatar}
                      alt={user.name}
                      className="h-full w-full object-cover"
                    />
                  ) : (
                    <span className="flex h-full w-full items-center justify-center font-bold text-slate-900">
                      {user.name?.charAt(0).toUpperCase()}
                    </span>
                  )}
                </figure>
                <span>{user.name}</span>
              </summary>

              <div className="absolute right-0 z-10 mt-2 w-44 overflow-hidden rounded-lg border border-gray-200 bg-white py-1 shadow-lg">
                {user.roles?.includes("ADMIN") && (
                  <Link
                    to="/dashboard"
                    className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                  >
                    Dashboard
                  </Link>
                )}
                <Link
                  to="/orders/me"
                  className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                >
                  Orders
                </Link>
                <Link
                  to="/profile"
                  className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                >
                  Profile
                </Link>
                <button
                  onClick={logoutHandler}
                  className="block w-full px-4 py-2 text-left text-sm text-red-600 hover:bg-gray-100"
                >
                  Logout
                </button>
              </div>
            </details>
          ) : (
            !loading && (
              <Link
                to="/login"
                className="rounded bg-brand px-6 py-2 font-medium text-slate-900"
              >
                Login
              </Link>
            )
          )}
        </div>
      </nav>
    </Fragment>
  );
};

export default Header;
