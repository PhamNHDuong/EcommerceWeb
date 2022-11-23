import "./new.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import Navbar from "../../components/navbar/Navbar";
import DriveFolderUploadOutlinedIcon from "@mui/icons-material/DriveFolderUploadOutlined";
import { useState, useEffect } from "react";
import {
  createCategory,
  getAvailableCategory,
} from "../../services/categoryService";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { createProduct } from "../../services/productService";
import { productInputs, categoryInputs } from "../../formSource";
import { createProductImage } from "../../services/imageService";

const New = (props) => {
  const [file, setFile] = useState("");
  const [byteArray, setByteArray] = useState([]);
  const [category, setCategory] = useState([]);
  const [inputs, setInputs] = useState([]);
  const { handleSubmit, register, getValues } = useForm();

  let nav = useNavigate();

  useEffect(() => {
    const res = getAvailableCategory();
    res
      .then((res) => {
        setCategory(...[res.data]);
      })
      .catch((err) => {
      });

    if (props.matrix === "product") {
      setInputs(productInputs);
    } else {
      setInputs(categoryInputs);
    }
  }, []);

  function readFileDataAsBase64(e) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();

      reader.onload = (event) => {
        resolve(event.target.result);
      };

      reader.onerror = (err) => {
        reject(err);
      };

      reader.readAsDataURL(e);
    });
  }

  const onSubmit = (data) => {
    var bodyFormData = new FormData();
    if (props.matrix === "product") {
      bodyFormData.append("CategoryCategoryId", data.category);
      bodyFormData.append("Name", data.name);
      bodyFormData.append("Price", data.price);
      bodyFormData.append("Description", data.description);
      bodyFormData.append("Stock", data.stock);
      bodyFormData.append("ImageBin", byteArray);

      const res = createProduct(bodyFormData);
      res
        .then(function (response) {
          //handle success
          alert("Product created successfully!");
          nav("/products");
        })
        .catch(function (err) {
          //handle error
          alert("Fail to create new product");
        });
    } else {
      bodyFormData.append("Name", data.name);
      bodyFormData.append("Description", data.description);
      const res = createCategory(bodyFormData);
      res
        .then(function (response) {
          //handle success
          alert("Category created successfully!");
          nav("/categories");
        })
        .catch(function (err) {
          //handle error
          alert("Fail to create new category");
        });
    }
    //bodyFormData.append('ProductImages', data.getValues('category'))
  };

  const handleCancel = () => {
    if (props.matrix === "product") nav("/products");
    else nav("/categories");
  };

  return (
    <div className="new">
      <Sidebar />
      <div className="newContainer">
        <Navbar />
        <div className="bottom">
          <div className="left">
            <img
              src={
                file
                  ? URL.createObjectURL(file)
                  : "https://icon-library.com/images/no-image-icon/no-image-icon-0.jpg"
              }
              alt=""
            />
            <div className="formInput">
              <label htmlFor="file">
                Image: <DriveFolderUploadOutlinedIcon className="icon" />
              </label>
              <input
                type="file"
                id="file"
                onChange={(e) => {
                  setFile(e.target.files[0]);
                  readFileDataAsBase64(e.target.files[0])
                    .then((res) => {
                      setByteArray(res);
                    })
                    .catch((res) => alert("No image"));
                }}
                style={{ display: "none" }}
              />
            </div>
          </div>
          <div className="right">
            <form onSubmit={handleSubmit(onSubmit)}>
              {props.matrix === "product" ? (
                <>
                  <div className="select">
                    <select className="sl" {...register("category")}>
                      <>
                        <option category={0} hidden>
                          Select Category
                        </option>
                        {category.map((items) => (
                          <option value={items.categoryId}>{items.name}</option>
                        ))}
                      </>
                    </select>
                  </div>
                  {inputs.map((input) => (
                    <div>
                      <label>{input.label}</label>
                      <input
                        type={input.type}
                        {...register(input.label)}
                        placeholder={input.placeholder}
                      />
                    </div>
                  ))}
                </>
              ) : (
                <>
                  {inputs.map((input) => (
                    <div>
                      <label>{input.label}</label>
                      <input
                        type={input.type}
                        {...register(input.label)}
                        placeholder={input.placeholder}
                      />
                    </div>
                  ))}
                </>
              )}
              <div className="btn">
                <button className="Send" type="submit">
                  Create
                </button>
                <button onClick={handleCancel}>Cancel</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default New;
