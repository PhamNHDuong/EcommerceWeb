import React from "react";
import { Outlet} from "react-router-dom";
import Footer from "./Footer";
import Header from "./Header";

class Layout extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="main-app">
        <Header />
        <Outlet />
        <Footer />
      </div>
    );
  }
}

export default Layout;
