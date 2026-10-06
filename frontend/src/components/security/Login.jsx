import React, { useEffect, useState } from "react";
import Loader from "../layout/Loader";
import { Fragment } from "react";
import { Link, useNavigate } from "react-router-dom";
import Metadata from "../layout/Metadata";
import { useAlert } from "react-alert";
import { useDispatch, useSelector } from "react-redux";
import { login } from "../../actions/userAction";

const Login = () => {
  const navigate = useNavigate();
  const alert = useAlert();
  const dispatch = useDispatch();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const { errors, isAuthenticated, loading } = useSelector(
    (state) => state.security,
  );

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/");
    }
    if (errors) {
      errors.map((error) => alert.error(error));
    }
  }, [isAuthenticated, errors, navigate, alert]);

  const submitHandler = (e) => {
    e.preventDefault();
    dispatch(login(email, password));
  };

  if (loading)
  {
    return <p><Loader /></p>;
  }

  return (
    <Fragment>
      <Metadata title="Login" />
      <div className="flex justify-center py-12">
        <form
          className="w-full max-w-md overflow-hidden rounded-lg border border-gray-200 bg-white shadow-lg"
          onSubmit={submitHandler}
        >
          <div className="border-b-4 border-brand bg-navy px-8 py-6">
            <h1 className="text-2xl font-semibold text-white">Sign in</h1>
            <p className="mt-1 text-sm text-gray-300">
              Welcome back! Log in to keep shopping.
            </p>
          </div>

          <div className="space-y-5 px-8 py-6">
            <div>
              <label
                htmlFor="email_field"
                className="mb-1 block text-sm font-medium text-gray-700"
              >
                Email
              </label>
              <input
                type="email"
                id="email_field"
                name="email"
                autoComplete="email"
                required
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="w-full rounded border border-gray-300 px-3 py-2 outline-none focus:border-brand focus:ring-2 focus:ring-brand/40"
              />
            </div>

            <div>
              <div className="mb-1 flex items-center justify-between">
                <label
                  htmlFor="password_field"
                  className="text-sm font-medium text-gray-700"
                >
                  Password
                </label>
                <Link
                  to="/password/forgot"
                  className="text-sm text-blue-600 hover:text-[#fa9c23] hover:underline"
                >
                  Forgot password?
                </Link>
              </div>
              <input
                type="password"
                id="password_field"
                name="password"
                autoComplete="current-password"
                required
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="w-full rounded border border-gray-300 px-3 py-2 outline-none focus:border-brand focus:ring-2 focus:ring-brand/40"
              />
            </div>

            <button
              id="login_button"
              type="submit"
              className="w-full rounded bg-brand py-3 font-semibold text-slate-900 transition hover:bg-[#fa9c23]"
            >
              Login
            </button>
          </div>

          <div className="border-t border-gray-200 bg-gray-50 px-8 py-4 text-center text-sm text-gray-600">
            New customer?{" "}
            <Link
              to="/register"
              className="font-medium text-blue-600 hover:text-[#fa9c23] hover:underline"
            >
              Create your account
            </Link>
          </div>
        </form>
      </div>
    </Fragment>
  );
};

export default Login;
