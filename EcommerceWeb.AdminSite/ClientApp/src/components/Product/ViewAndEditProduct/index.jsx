import React, { useState, useEffect } from "react";
import { FileUploader } from "react-drag-drop-files";
import { getAllCategory } from "../../../service/CategoryService.js";
import {
  deleteProduct,
  updateProduct,
} from "../../../service/ProductService.js";
import { ref, getDownloadURL, uploadBytesResumable } from "firebase/storage";
import storage from "../../../config/firebaseConfig.js";
import { deleteImage } from "../../../service/ProductInageService.js";

const fileTypes = ["JPG", "PNG"];

const ViewAndEditProduct = (props) => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState(0);
  const [amount, setAmount] = useState(0);
  const [category, setCategory] = useState("");
  const [listImage, setListImage] = useState([]);
  const [newImage, setNewImage] = useState([]);
  const [listCategory, setListCategory] = useState([]);
  const [fileImage, setFileImage] = useState([]);
  const [success, setSuccess] = useState(null);
  const [error, setError] = useState(null);
  const [hasEdit, setHasEdit] = useState(true);

  const [step, setStep] = useState(1);

  const initialize = () => {
    setName(props.product.name);
    setDescription(props.product.description);
    setPrice(props.product.price);
    setAmount(props.product.amount);
    const cate = props.product.category;
    setCategory(cate.id + " - " + cate.name);
    const images = props.product.productImages;
    setListImage(images);

    getAllCategory()
      .then((res) => {
        setListCategory(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  useEffect(() => {
    initialize();
  }, []);

  const handleSaveChangeClick = () => {
    setHasEdit(true);
    if (description && name && price && amount && category) {
      // collect data and send to backend
      let cateId = category.split(" ")[0];
      const data = JSON.stringify({
        name: name,
        description: description,
        amount: amount,
        price: price,
        categoryId: cateId,
        images: [...newImage],
      });

      const token = localStorage.getItem("token");

      updateProduct(props.product.proId, data, token)
        .then((res) => {
          setStep(1);
          setSuccess("Update success");
        })
        .catch((err) => {
          setError(err.message);
        });

    } else {
      setError("All fields are required!");
    }
  };

  const handleEditClick = () => {
    setHasEdit(false);
    setStep(3);
  };

  const handleChangeStatus = () => {
    const token = localStorage.getItem("token");
    deleteProduct(props.product.proId , token)
      .then((res) => {
        let mess = res.data.status ? "TRADING" : "STOP TRADE";
        setSuccess(`Product status changed to ${mess} successfully`);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const handleDeleteImage = (e, id) => {
    e.preventDefault();
    deleteImage(id)
      .then((res) => {
        setSuccess("Deleted Images Successfully " + res.status);
      })
      .catch((err) => {
        setError(err.message);
      });
  };

  const uploadFile = (file) => {
    setStep(2);
    const tempFiles = [...fileImage];
    for (const element of file) {
      tempFiles.push(element);
    }
    setFileImage(tempFiles);
  };

  const uploadImages = () => {
    if (fileImage.length !== 0) {
      let count = 0;
      fileImage.forEach((ele) => {
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
                let list = newImage;
                list.push(url);
                setNewImage(list);
                count = count + 1;
                if (count === fileImage.length) {
                  setStep(3);
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

  const getImages = () => {
    let list = [...listImage];
    if (list.length === 0) {
      return <p>No images</p>;
    }

    return (
      <>
        {list.map((ele) => (
          <div className="col-md-4 position-relative" key={ele.id}>
            <img src={ele.imgUrl} className="d-block w-100" alt="..."></img>
            {!hasEdit ? (
              <button
                className="position-absolute top-0 start-100 translate-middle badge rounded-pill btn btn-primary"
                onClick={(e) => handleDeleteImage(e, ele.id)}
              >
                X
              </button>
            ) : (
              ""
            )}
          </div>
        ))}
      </>
    );
  };

  return (
    <div className="product modal-dialog modal-dialog-centered">
      <div className="modal-content">
        <div className="modal-header">
          <h5 className="modal-title" id="staticBackdropLabel">
            View And Edit Product
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
                  required
                  disabled={hasEdit}
                />
              </div>

              {/* Show category */}
              <div className="category-product col-md-12 mb-3">
                <label className="form-label" htmlFor="categoryProductInput">
                  Category
                </label>
                <input
                  type="text"
                  list="categoryList"
                  className="form-control"
                  id="categoryProductInput"
                  placeholder="Enter category of product"
                  onChange={(e) => setCategory(e.target.value)}
                  value={category}
                  required
                  disabled={hasEdit}
                />

                <datalist id="categoryList">
                  {listCategory.map((cate) => (
                    <option value={cate.id + " - " + cate.name} key={cate.id} />
                  ))}
                </datalist>
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
                  onChange={(e) => setDescription(e.target.value)}
                  required
                  disabled={hasEdit}
                ></textarea>
              </div>

              {/* Number of product */}
              <div className="number-product col-md-6">
                <label htmlFor="numberProductInput" className="form-label">
                  Amount
                </label>
                <input
                  id="numberProductInput"
                  type="number"
                  className="form-control"
                  placeholder="Number of Product"
                  min={0}
                  max={1000}
                  value={amount}
                  onChange={(e) => setAmount(e.target.value)}
                  required
                  disabled={hasEdit}
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
                    onChange={(e) => setPrice(e.target.value)}
                    required
                    disabled={hasEdit}
                  />
                </div>
              </div>

              <div className="col-md-12 d-flex flex-wrap">
                {listImage && getImages()}
              </div>

              <div className="col-md-12">
                {!hasEdit ? (
                  <div className="images-product d-block w-100">
                    <FileUploader
                      handleChange={uploadFile}
                      name="file"
                      types={fileTypes}
                      multiple={true}
                    />
                  </div>
                ) : (
                  ""
                )}
                {fileImage.length !== 0 ? (
                  <div>
                    {fileImage.map((file) => (
                      <p key={file.name}>{file.name}</p>
                    ))}
                  </div>
                ) : (
                  ""
                )}
              </div>

              {/* Button create new product */}

              <div className="col-md-3 btn-save ">
                <button
                  className="btn-primary btn"
                  onClick={handleEditClick}
                  disabled={step !== 1}
                >
                  Edit
                </button>
              </div>

              <div className="col-md-3 btn-save ">
                <button
                  className="btn-primary btn"
                  onClick={uploadImages}
                  disabled={step !== 2}
                >
                  Upload Image
                </button>
              </div>

              <div className="col-md-3 btn-save ">
                <button
                  className="btn-primary btn"
                  onClick={handleChangeStatus}
                >
                  {props.product.status ? "Stop Trade" : "Continue Trade"}
                </button>
              </div>

              <div className="col-md-3 btn-save ">
                <button
                  className="btn-primary btn"
                  disabled={step !== 3}
                  onClick={handleSaveChangeClick}
                >
                  Save Change
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ViewAndEditProduct;
