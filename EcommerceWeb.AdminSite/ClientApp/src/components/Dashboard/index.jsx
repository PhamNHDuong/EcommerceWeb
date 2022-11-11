import React from "react";
import { Link } from "react-router-dom";

class Dashboard extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="dashboard d-flex flex-column bg-black text-white position-fixed">
        <div className="dashboard-item title fs-1 pb-3">
          <Link to={"/admin"}>Admin</Link>
        </div>
        <div className="dashboard-item">
          <Link to="product">Product</Link>
        </div>
        <div className="dashboard-item">
          <Link to="category">Category</Link>
        </div>
      </div>
    );
  }
}

export default Dashboard;
