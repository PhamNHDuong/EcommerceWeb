import "./datatable.scss";
import { DataGrid, GridRowParams } from "@mui/x-data-grid";
import {
  userColumns,
  productColumns,
  categoryColumns,
} from "../../datatablesource";
import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import {
  disableUser,
  enableUser,
  getAllUser,
} from "../../services/userService";
import {
  disableProduct,
  enableProduct,
  getAllProduct,
} from "../../services/productService";
import {
  disableCategory,
  enableCategory,
  getAllCategory,
} from "../../services/categoryService";

const Datatable = (props) => {
  const [data, setData] = useState([]);
  const [columnData, setColumn] = useState([]);
  const [selectedData, setSeleted] = useState([]);

  const onRowSelect = (event) => {
    setSeleted(event.id);
  };

  const initializeData = () => {
    let res;
    if (props.matrix === "user") {
      res = getAllUser();
      res
        .then((res) => {
          setData(...[res.data]);
        })
        .catch((err) => {

        });
      setColumn(userColumns);
    } else {
      if (props.matrix === "product") {
        res = getAllProduct();
        res
          .then((res) => {
            setData(...[res.data]);
          })
          .catch((err) => {

          });
        setColumn(productColumns);
      } else {
        res = getAllCategory();
        res
          .then((res) => {
            setData(...[res.data]);
          })
          .catch((err) => {

          });
        setColumn(categoryColumns);
      }
    }
  };

  const handleDisable = () => {
    let res;
    if (selectedData == null) {
      alert("Please choose row!");
    }
    if (props.matrix === "user") {
      res = disableUser(selectedData);
      res
        .then((res) => {
          initializeData();
          alert("Disable user successfully!");
        })
        .catch((err) => {
          alert("User is already disable!");
        });
    } else {
      if (props.matrix === "product") {
        res = disableProduct(selectedData);
        res
          .then((res) => {
            initializeData();
            alert("Disable product successfully!");
          })
          .catch((err) => {
            alert("Product is already disable!");
          });
      } else {
        res = disableCategory(selectedData);
        res
          .then((res) => {
            initializeData();
            alert("Disable category successfully!");
          })
          .catch((err) => {
            alert("Category is already disable!");
          });
      }
    }
  };

  const handleEnable = () => {
    let res;
    if (props.matrix === "user") {
      res = enableUser(selectedData);
      res
        .then((res) => {
          initializeData();
          alert("Enable user successfully!");
        })
        .catch((err) => {
          alert("User is already enabled!");
        });
    } else {
      if (props.matrix === "product") {
        res = enableProduct(selectedData);
        res
          .then((res) => {
            initializeData();
            alert("Enable product successfully!");
          })
          .catch((err) => {
            alert("Product is already enable!");
          });
      } else {
        res = enableCategory(selectedData);
        res
          .then((res) => {
            initializeData();
            alert("Enable category successfully!");
          })
          .catch((err) => {
            alert("Category is already enabled!");
          });
      }
    }
  };

  useEffect(() => {
    initializeData();
  }, [props]);

  const actionColumn = (id) => [
    {
      field: "action",
      headerName: "Action",
      width: 200,
      renderCell: (params) => {
        return (
          <div className="cellAction">
            {props.matrix === "user" ? (
              <>
                <div className="disableButton">Disable</div>
                <div className="enableButton">Enable</div>
              </>
            ) : (
              <>
                <div>
                  <Link to={`${id}`} style={{ textDecoration: "none" }}>
                    <div className="viewButton">View</div>
                  </Link>
                </div>
                <div className="disableButton" onClick={handleDisable}>
                  Disable
                </div>
                <div className="enableButton" onClick={handleEnable}>
                  Enable
                </div>
              </>
            )}
          </div>
        );
      },
    },
  ];

  return (
    <div className="datatable">
      <div className="datatableTitle">
        {props.matrix === "user" ? null : props.matrix === "product" ? (
          <Link to="/products/new" className="link">
            Add New Product
          </Link>
        ) : (
          <Link to="/categories/new" className="link">
            Add New Category
          </Link>
        )}
      </div>
      <DataGrid
        getRowId={
          props.matrix === "user"
            ? (row) => row.aUserId
            : props.matrix === "product"
            ? (row) => row.productId
            : (row) => row.categoryId
        }
        rows={data}
        columns={columnData.concat(actionColumn(`${selectedData}`))}
        pageSize={9}
        rowsPerPageOptions={[9]}
        onRowClick={onRowSelect}
      />
    </div>
  );
};

export default Datatable;
