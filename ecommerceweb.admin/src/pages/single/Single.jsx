import "./single.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import Navbar from "../../components/navbar/Navbar";
import { useState, useEffect } from "react";
import {
  getAllCategory,
  getByCategory,
  updateCategory,
} from "../../services/categoryService";
import { useNavigate, useParams } from "react-router-dom";
import { getByProduct, updateProduct } from "../../services/productService";

const Single = (props) => {
  const [categoryData, setCategoryData] = useState([]);
  let { productId } = useParams();
  let { categoryId } = useParams();
  const [cID, setCID] = useState("");
  const [productName, setProductName] = useState("");
  const [productPrice, setProductPrice] = useState("");
  const [productStock, setProductStock] = useState("");
  const [productDescription, setproductDescription] = useState("");

  let nav = useNavigate();

  useEffect(() => {
    if (props.matrix === "product") {
      const res = getAllCategory();
      const detailres = getByProduct(productId);
      detailres
        .then((res) => {

          setCID(res.data.categoryCategoryId);
          setProductName(res.data.name);
          setProductPrice(res.data.price);
          setProductStock(res.data.stock);
          setproductDescription(res.data.description);
        })
        .catch((err) => {
        });
      res
        .then((res) => {
          setCategoryData(...[res.data]);
        })
        .catch((err) => {
        });
    } else {
      const detailres = getByCategory(categoryId);
      detailres
        .then((res) => {
          setCID(categoryId);
          setProductName(res.data.name);
          setproductDescription(res.data.description);
        })
        .catch((err) => {
        });
    }
  }, []);

  const handleCancel = () => {
    if (props.matrix === "product") nav("/products");
    else nav("/categories");
  };

  const handleUpdate = () => {
    if (props.matrix === "product") {
      const data = {
        name: productName,
        stock: productStock,
        price: productPrice,
        description: productDescription,
        categoryCategoryId: cID,
      };
      const res = updateProduct(productId, data);
      res
        .then((res) => {
          alert("Update Successfully");
          nav("/products");
        })
        .catch((err) => {
          alert("Can't update product");
        });
    } else {
      const data = {
        name: productName,
        description: productDescription,
      };  
      const res = updateCategory(categoryId, data);
      res
        .then((res) => {
          alert("Update Successfully");
          nav("/categories");
        })
        .catch((err) => {
          alert("Can't update category");
        });
    }
  };

  //if (!productName) return <h1>No Data</h1>;
  return (
    <div className="single">
      <Sidebar />
      <div className="singleContainer">
        <Navbar />
        <div className="top">
          <div className="left">
            <div className="item">
              {/* <img
                src=""
                alt=""
                className="itemImg"
              /> */}
              <div className="details">
                {props.matrix === "product" ? (
                  <>
                    <div className="select">
                      <select
                        className="sl"
                        onChange={(e) => {
                          setCID(e.target.value);
                        }}
                      >
                        <>
                          {categoryData.map((items) => (
                            <option
                              value={items.categoryId}
                              selected={cID === items.categoryId}
                            >
                              {items.name}
                            </option>
                          ))}
                        </>
                      </select>
                    </div>
                    <div className="details">
                      <div className="detailItem">
                        <label className="itemKey">Product Id</label>
                        <input
                          className="itemValue"
                          type="text"
                          value={productId}
                          disabled
                        />
                      </div>
                      <div className="detailItem">
                        <label className="itemKey">Name</label>
                        <input
                          className="itemValue"
                          type="text"
                          name="name"
                          value={productName}
                          required
                          onChange={(e) => setProductName(e.target.value)}
                        />
                      </div>
                      <div className="detailItem">
                        <label className="itemKey">Price</label>
                        <input
                          className="itemValue"
                          type="number"
                          value={productPrice}
                          onChange={(e) => setProductPrice(e.target.value)}
                        />
                      </div>
                      <div className="detailItem">
                        <label className="itemKey">Stock</label>
                        <input
                          className="itemValue"
                          type="number"
                          value={productStock}
                          onChange={(e) => setProductStock(e.target.value)}
                        />
                      </div>
                      <div className="detailItem">
                        <label className="itemKey">Description</label>
                        <input
                          className="itemValue"
                          type="text"
                          value={productDescription}
                          onChange={(e) =>
                            setproductDescription(e.target.value)
                          }
                        />
                      </div>
                    </div>
                    <div className="btn">
                      <button
                        className="Send"
                        onClick={handleUpdate}
                        disabled={
                          !(
                            productName &&
                            productPrice &&
                            productStock &&
                            productDescription
                          )
                        }
                      >
                        Update
                      </button>
                      <button className="Send" onClick={handleCancel}>
                        Cancel
                      </button>
                    </div>
                  </>
                ) : (
                  <>
                    <div className="details">
                      <div className="detailItem">
                        <label className="itemKey">Category Id</label>
                        <input
                          className="itemValue"
                          type="text"
                          value={categoryId}
                          disabled
                        />
                      </div>
                      <div className="detailItem">
                        <label className="itemKey">Name</label>
                        <input
                          className="itemValue"
                          type="text"
                          name="name"
                          value={productName}
                          required
                          onChange={(e) => setProductName(e.target.value)}
                        />
                      </div>
                      <div className="detailItem">
                        <label className="itemKey">Description</label>
                        <input
                          className="itemValue"
                          type="text"
                          value={productDescription}
                          onChange={(e) =>
                            setproductDescription(e.target.value)
                          }
                        />
                      </div>
                    </div>
                    <div className="btn">
                      <button
                        className="Send"
                        onClick={handleUpdate}
                        disabled={!(productName && productDescription)}
                      >
                        Update
                      </button>
                      <button className="Send" onClick={handleCancel}>
                        Cancel
                      </button>
                    </div>
                  </>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Single;
