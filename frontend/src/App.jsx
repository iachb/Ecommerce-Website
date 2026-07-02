import "./App.css";
import Header from "./components/layout/Header";
import Footer from "./components/layout/Footer";
import Home from "./components/layout/Home";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import ProductDetail from "./components/product/ProductDetail";

function App() {
  return (
    <Router>
      <div className="App">
        <Header />
        <div className="mx-auto min-h-[70vh] max-w-7xl px-4">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/product/:id" element={<ProductDetail />} />
          </Routes>
        </div>
        <Footer />
      </div>
    </Router>
  );
}

export default App;
