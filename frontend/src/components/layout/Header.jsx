import React, { Fragment } from "react";

const Header = () => {
  return (
    <Fragment>
      <nav className="flex flex-wrap items-center gap-4 bg-navy px-6 py-3">
        <div className="shrink-0">
          <img src="/images/logo_vaxi.png" alt="logo" className="h-10" />
        </div>

        <div className="order-3 w-full md:order-none md:min-w-0 md:flex-1">
          <div className="flex">
            <input
              type="text"
              placeholder="Enter Product Name ..."
              className="min-w-0 flex-1 rounded-l bg-white px-4 py-2 text-slate-900 focus:outline-none"
            />
            <button
              className="rounded-r bg-brand px-4 text-slate-900"
              aria-label="Search"
            >
              <i className="fa fa-search" aria-hidden="true" />
            </button>
          </div>
        </div>

        <div className="ml-auto flex shrink-0 items-center gap-3 md:ml-0">
          <button className="rounded bg-brand px-6 py-2 font-medium text-slate-900">
            Login
          </button>
          <span className="text-white">Cart</span>
          <span className="rounded bg-brand px-2 py-0.5 text-sm font-bold text-black">
            2
          </span>
        </div>
      </nav>
    </Fragment>
  );
};

export default Header;
