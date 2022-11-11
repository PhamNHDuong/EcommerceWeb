import React from "react";
import { Outlet } from "react-router-dom";
import Dashboard from "../../components/Dashboard";


const Admin = () => {
  return (
    <div className="admin d-flex">
      <Dashboard />
      <Outlet />
    </div>
  );
};

export default Admin;
