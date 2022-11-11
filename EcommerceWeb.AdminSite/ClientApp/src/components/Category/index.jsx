import React, { useState, useEffect } from "react";
import {
  deleteCategory,
  updateCategory,
  createCategory,
} from "../../service/CategoryService.js";

const Category = (props) => {
  const [hasEdit, setHasEdit] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);
  const [cateName, setCateName] = useState(props.cate_name);
  const [cateDescription, setCateDescription] = useState(props.cate_des);

  const initialView = () => {
    let action = props.action;
    if (action === "View") {
      setHasEdit(true);
    }
  };

  useEffect(() => {
    initialView();
  }, []);

  const handleClick_EditButton = () => {
    setHasEdit(false);
  };

  const handleChangeStatusButton = async () => {
    let id = props.id;
    deleteCategory(id)
      .then((res) => {
        setSuccess("Status was change to: " + res.data.status);
      })
      .catch((err) => {
        setError(err.message);
      });
  };

  const handleSaveButton = async () => {
    let action = props.action;

    let id = props.id;
    let name = cateName;
    let des = cateDescription;

    if (name && des) {
      if (action === "View") {
        updateCategory(id, name, des)
          .then(() => {
            setSuccess("Category was changed successfully");
          })
          .catch((err) => {
            console.log(err);
            setError(err);
          });

        setHasEdit(true);
      } else {
        createCategory(name, des)
          .then(() => {
            setSuccess("Category was created successfully");
            setCateName("");
            setCateDescription("");
          })
          .catch((err) => {
            console.log(err);
          });
      }
    } else {
      setError("All field are required");
    }
  };

  const handleTextChange = () => {
    setSuccess("");
    setError(null);
  };

  let title = "Create New Category";
  if (props.action === "View") {
    title = "Category";
  }
  return (
    <div className="modal-dialog modal-dialog-centered">
      <div className="modal-content">
        <div className="modal-header">
          <h5 className="modal-title" id="staticBackdropLabel">
            {title}
            {error && (
              <p className="text-danger fs-6 text-opacity-50">{error}</p>
            )}

            {success && (
              <p className="text-success fs-6 text-opacity-50">{success}</p>
            )}
          </h5>
          <button
            type="button"
            className="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div className="modal-body">
          <div className="mb-3">
            <label for="name_category" className="form-label">
              Category Name
            </label>
            <input
              type="text"
              className="form-control"
              id="name_category"
              placeholder="name..."
              name="cate_name"
              onChange={(e) => setCateName(e.target.value)}
              onFocus={handleTextChange}
              value={cateName}
              disabled={hasEdit}
            />
          </div>
          <div className="mb-3">
            <label for="description_category" className="form-label">
              Category Description
            </label>
            <textarea
              className="form-control"
              id="description_category"
              rows="5"
              name="cate_des"
              onChange={(e) => setCateDescription(e.target.value)}
              value={cateDescription}
              disabled={hasEdit}
              onBlur={handleTextChange}
            ></textarea>
          </div>
        </div>
        <div className="modal-footer">
          <button
            type="button"
            className="btn btn-secondary"
            data-bs-dismiss="modal"
            onClick={() => window.location.reload()}
          >
            Close
          </button>
          {props.id && (
            <>
              <button
                type="button"
                className="btn btn-primary"
                onClick={handleClick_EditButton}
              >
                Edit
              </button>
              <button
                type="button"
                className="btn btn-primary"
                onClick={handleChangeStatusButton}
              >
                Change Status
              </button>
            </>
          )}
          <button
            type="button"
            className="btn btn-primary"
            onClick={handleSaveButton}
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export default Category;
