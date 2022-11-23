import "./navbar.scss";
import SearchOutlinedIcon from "@mui/icons-material/SearchOutlined";
import LanguageOutlinedIcon from "@mui/icons-material/LanguageOutlined";
import { useEffect, useState } from "react";
import Cookies from "js-cookie";

const Navbar = () => {
  const [username, setUsername] = useState();

  useEffect(() => {
    if (
      Cookies.get("username") !== undefined &&
      Cookies.get("role") === "Admin"
    ) {
      setUsername(Cookies.get("username"));
    } else if (window.location.href !== "http://localhost:3000/login") {
      document.location.href = "http://localhost:3000/login";
    }
  }, []);

  return (
    <div className="navbar">
      <div className="wrapper">
        <div className="search">
          <input type="text" placeholder="Search..." />
          <SearchOutlinedIcon className="icon" />
        </div>
        <div className="items">
          <div className="item">
            <LanguageOutlinedIcon className="icon" />
            English
          </div>
          <div className="item">
            <text> {username}</text>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
