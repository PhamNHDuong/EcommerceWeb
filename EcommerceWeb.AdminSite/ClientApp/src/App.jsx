import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Admin, Home, SignIn, SignUp } from "./pages";

import Layout from "./layout";
import { ProductManage, CategoryManage } from "./pages/admin/Manage";
import Cart from "./pages/Cart";
import ViewProduct from "./components/ViewProduct/";

const App = () => {
  return (
    <div className="app">
      <BrowserRouter>
        <Routes>
          <Route path="admin" element={<Admin />}>
            <Route path="product" element={<ProductManage />} />
            <Route path="category" element={<CategoryManage />} />
          </Route>

          <Route path="login" element={<SignIn />} />
          <Route path="register" element={<SignUp />} />
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route path="/cart" element={<Cart />} />
            <Route path="/home/:type" element={<Home />} />
            <Route path="/home/view/:id" element={<ViewProduct />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default App;