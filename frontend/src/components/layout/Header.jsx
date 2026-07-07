import React, { Fragment } from "react";
import Search from "./Search";

const Header = () => {
  return (
    <Fragment>
      <nav className="flex flex-wrap items-center gap-4 bg-navy px-6 py-3">
        <div className="shrink-0">
          <img src="/images/logo_vaxi.png" alt="logo" className="h-10" />
        </div>

        <div className="order-3 flex w-full justify-center md:order-none md:min-w-0 md:flex-1">
          <Search />
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
