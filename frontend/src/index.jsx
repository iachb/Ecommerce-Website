import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import { Provider } from "react-redux";
import store from "./store";
import { positions, transitions, Provider as AlertProvider } from "react-alert";
import AlertTemplate from "react-alert-template-basic";

ReactDOM.render(
  <Provider store={store}>
    <AlertProvider
      template={AlertTemplate}
      timeout={5000}
      offset="30px"
      positions={positions.BOTTOM_CENTER}
      transitions={transitions.SCALE}
    >
      <App />
    </AlertProvider>
  </Provider>,
  document.getElementById("root"),
);
