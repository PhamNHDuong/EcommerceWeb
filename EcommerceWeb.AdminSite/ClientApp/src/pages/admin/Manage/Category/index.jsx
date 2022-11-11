import React, { useEffect, useState } from "react";
import Category from "../../../../components/Category/";
import moment from "moment";
import { getAllCategory } from "../../../../service/CategoryService.js";

const CategoryManage = () => {
  const [data, setData] = useState([]);

  const getDataFromAPI = () => {
    getAllCategory()
      .then((res) => {
        setData(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  useEffect(() => {
    getDataFromAPI();
    console.log(data);
  }, []);

  return (
    <div className="main-admin category-manage d-flex flex-column align-items-center justify-content-center">
      <div className="show-category mb-3">
        <table className="table table-responsive">
          <thead className="table-dark">
            <tr>
              <th scope="col">#</th>
              <th scope="col">Name</th>
              <th scope="col">Description</th>
              <th scope="col">Create Date</th>
              <th scope="col">Last Update</th>
            </tr>
          </thead>
          <tbody className="table-group-divider">
            {data.map((item) => (
              <>
                <tr
                  key={item.id}
                  className=""
                  data-bs-toggle="modal"
                  data-bs-target={"#viewCate" + item.id}
                >
                  <th scope="row">{item.status}</th>
                  <td>{item.name}</td>
                  <td>{item.description}</td>
                  <td>{moment(item.createDate).format("LLL")}</td>
                  <td>{moment(item.updateDate).format("LLL")}</td>
                </tr>
                {/* Modal to add category */}
                <div
                  className="modal fade"
                  id={"viewCate" + item.id}
                  data-bs-backdrop="static"
                  data-bs-keyboard="false"
                  tabIndex="-1"
                  aria-labelledby="staticBackdropLabel"
                  aria-hidden="true"
                >
                  <Category
                    action="View"
                    id={item.id}
                    cate_name={item.name}
                    cate_des={item.description}
                  />
                </div>
              </>
            ))}
          </tbody>
        </table>
      </div>

      <div className="manage">
        <button
          type="button"
          data-bs-toggle="modal"
          data-bs-target="#addCate"
          className="btn btn-primary me-1"
        >
          Add New Category
        </button>

        {/* Modal to add category */}
        <div
          className="modal fade"
          id="addCate"
          data-bs-backdrop="static"
          data-bs-keyboard="false"
          tabIndex="-1"
          aria-labelledby="staticBackdropLabel"
          aria-hidden="true"
        >
          <Category action="Add" />
        </div>
      </div>
    </div>
  );
};

export default CategoryManage;

// TODO: Add field number of book have same category
