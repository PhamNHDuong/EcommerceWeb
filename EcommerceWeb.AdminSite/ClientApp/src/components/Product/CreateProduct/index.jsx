import React, { useState, useEffect } from "react";
import { FileUploader } from "react-drag-drop-files";
import { getAllCategory } from "../../../service/CategoryService.js";
import { createProduct } from "../../../service/ProductService.js";
import { ref, getDownloadURL, uploadBytesResumable } from "firebase/storage";
import storage from "../../../config/firebaseConfig.js";
const fileTypes = ["JPG", "PNG"];

const CreateProduct = () => {
  const [files, setFiles] = useState([]);
  const [urlImages, setUrlImages] = useState([]);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState(0);
  const [amount, setAmount] = useState(0);
  const [category, setCategory] = useState(0);
  const [listCategory, setListCategory] = useState([]);

  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);
  const [hasUpload, setHasUpload] = useState(true);

  const getDataFromAPI = () => {
    getAllCategory()
      .then((res) => {
        setListCategory(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  useEffect(() => {
    getDataFromAPI();
  }, []);

  const uploadFile = (file) => {
    const tempFiles = [...files];
    for (const element of file) {
      tempFiles.push(element);
    }
    setFiles(tempFiles);
  };

  const onFocusHandle = () => {
    setError(null);
    setSuccess(null);
  };

  const uploadImages = () => {
    if (files) {
      let count = 0;
      files.forEach((ele) => {
        const storageRef = ref(
          storage,
          `/images/${name + "_" + ele.name + "_" + ele.lastModified}`
        );
        const uploadTask = uploadBytesResumable(storageRef, ele);
        uploadTask.on(
          "state_changed",
          (snapshot) => {},
          (err) => console.log(err),
          () => {
            // download url
            getDownloadURL(uploadTask.snapshot.ref)
              .then((url) => {
                let list = urlImages;
                list.push(url);
                setUrlImages(list);
                count = count + 1;
                if (count === files.length) {
                  setHasUpload(false);
                }
              })
              .catch((err) => {
                console.log(err);
              });
          }
        );
      });
    } else {
      setError("No files image was upload yet!");
    }
  };

  const createNewProduct = async () => {
    if (description && urlImages && name && price && amount && category) {
      // collect data and send to backend
      const data = JSON.stringify({
        name: name,
        description: description,
        amount: amount,
        price: price,
        categoryId: category,
        images: [...urlImages],
      });

      const token = localStorage.getItem("token");

      createProduct(data, token)
        .then((res) => {
          setSuccess("success");
        })
        .catch((err) => {
          console.log(err);
          setError(err.message);
        });
    } else {
      setError("All fields are required!");
    }
  };
  return (
    <div className="product modal-dialog modal-dialog-centered">
      <div className="modal-content">
        <div className="modal-header">
          <h5 className="modal-title" id="staticBackdropLabel">
            Add New Product
            {error && <p className="fs-6 text-danger">{error}</p>}
            {success && <p className="fs-6 text-success">{success}</p>}
          </h5>
          <button
            type="button"
            className="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div className="modal-body">
          <div className="add-product">
            <div className="input-group row g-3 needs-validation">
              {/* Name of product */}
              <div className="mb-3 name-product col-md-12">
                <label htmlFor="nameProductInput" className="form-label">
                  Product Name
                </label>
                <input
                  type="text"
                  className="form-control"
                  id="nameProductInput"
                  placeholder="Enter name of product"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  onFocus={onFocusHandle}
                  required
                />
              </div>

              {/* Show category */}
              <div className="category-product input-group mb-3">
                <label
                  className="input-group-text"
                  htmlFor="categoryGroupSelect"
                >
                  Category
                </label>
                <select
                  className="form-select"
                  id="categoryGroupSelect"
                  onChange={(e) => setCategory(e.target.value)}
                >
                  <option value={0}>None</option>
                  {listCategory.map((cate) => (
                    <option value={cate.id} key={cate.id}>
                      {cate.name}
                    </option>
                  ))}
                </select>
              </div>

              {/* description of  product*/}
              <div className="mb-3 description-product col-md-12">
                <label htmlFor="descriptionProductInput" className="form-label">
                  Product Description
                </label>
                <textarea
                  rows="3"
                  id="descriptionProductInput"
                  cols="30"
                  className="form-control"
                  value={description}
                  onFocus={onFocusHandle}
                  onChange={(e) => setDescription(e.target.value)}
                  required
                ></textarea>
              </div>

              {/* Number of product */}
              <div className="number-product col-md-6">
                <label htmlFor="numberProductInput" className="form-label">
                  Number of Product
                </label>
                <input
                  id="numberProductInput"
                  type="number"
                  className="form-control"
                  placeholder="Number of Product"
                  min={0}
                  max={1000}
                  value={amount}
                  onFocus={onFocusHandle}
                  onChange={(e) => setAmount(e.target.value)}
                  required
                />
              </div>

              {/* Price of product */}
              <div className="price-product col-md-6">
                <label htmlFor="priceProductInput" className="form-label">
                  Price:
                </label>
                <div className="input-group has-validation">
                  <span className="input-group-text" id="inputGroupPrepend">
                    $
                  </span>
                  <input
                    id="priceProductInput"
                    type="number"
                    className="form-control"
                    placeholder="Price"
                    min={0}
                    max={1000}
                    value={price}
                    onFocus={onFocusHandle}
                    onChange={(e) => setPrice(e.target.value)}
                    required
                  />
                </div>
              </div>

              {/* upload image */}
              <div className="images-product col-md-12">
                <FileUploader
                  handleChange={uploadFile}
                  name="file"
                  types={fileTypes}
                  multiple={true}
                />
                {/* TODO: Make it work */}
                {files.length !== 0 ? (
                  <div>
                    {files.map((file) => (
                      <p key={file.name}>{file.name}</p>
                    ))}
                  </div>
                ) : (
                  <div>No file upload yet</div>
                )}
              </div>

              {/* Button create new product */}
              <div className="col-md-6 btn-save ">
                <button
                  className="btn-primary btn"
                  onClick={uploadImages}
                  disabled={!hasUpload}
                >
                  Upload Image
                </button>
              </div>

              <div className="col-md-6 btn-save ">
                <button
                  className="btn-primary btn"
                  onClick={createNewProduct}
                  disabled={hasUpload}
                >
                  Create New Product
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CreateProduct;
