import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import {
  BranchIcon,
  SearchIcon,
  ShoppingCartIcon,
  LogOutIcon,
} from "../../utils/Icon";

const MainHeader = () => {
  const [searchContent, setSearchContent] = useState("");
  const [username, setUsername] = useState();

  const textChangeHandle = (e) => {
    setSearchContent(e.target.value);
  };

  const searchProduct = () => {
    alert(searchContent);
  };

  useEffect(() => {
    const isExistsUsername = localStorage.getItem("username");
    if (isExistsUsername) {
      setUsername(isExistsUsername);
    }
  }, []);

  const logout = () => {
    localStorage.clear();
    window.location.reload();
  };
  return (
    <div className="main_header d-flex justify-content-between align-items-end w-100">
      <Link to={"/"}>
        <div className="header_branch d-flex align-items-end">
          <img className="branch_icon" src={BranchIcon} alt="icon" />
          <span className="fw-bold">OOK SHOP</span>
        </div>
      </Link>

      <div className="header_search input-group">
        <input
          type="text"
          className="form-control"
          placeholder="Search by author, name, ... "
          name="search_content"
          onChange={textChangeHandle}
          value={searchContent}
        />
        <button
          className="btn btn-outline-primary"
          type="button"
          onClick={searchProduct}
        >
          <img className="search_icon" src={SearchIcon} alt="" />
        </button>
      </div>

      <div className="header_auth d-flex align-items-center">
        {/* TODO: has log in yet? */}
        {username ? (
          <>
            <span className="btn btn-outline-primary">{username}</span>
            <Link to={"cart"} className="shopping-cart me-2 ms-2">
              <img
                className="shop_icon"
                src={ShoppingCartIcon}
                alt="shopping cart"
              />
              {/* TODO: view number of item in cart when cart have item */}
            </Link>
            <img
              className="shop_icon"
              src={LogOutIcon}
              alt="shopping cart"
              onClick={logout}
            />
          </>
        ) : (
          <Link to={"login"}>
            <span className="btn btn-outline-primary">Log In/Register</span>
          </Link>
        )}
      </div>
    </div>
  );
};

export default MainHeader;
