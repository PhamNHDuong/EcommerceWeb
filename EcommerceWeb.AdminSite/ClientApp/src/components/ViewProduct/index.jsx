import { Rating, TextField } from "@mui/material";
import { useEffect, useState } from "react";
import { Button, Carousel } from "react-bootstrap";
import { Link, useParams } from "react-router-dom";
import { addProductToCart, getCartByAccId } from "../../service/CartService";
import { getProductById } from "../../service/ProductService";
import { addNewPost } from "../../service/RateService";

const ViewProduct = () => {
  let { id } = useParams();
  const [product, setProduct] = useState();
  const [newRate, setNewRate] = useState();
  const [newComment, setNewComment] = useState("");
  const [isLogin, setIsLogin] = useState(false);
  const [quantity, setQuantity] = useState(1);
  const [success, setSuccess] = useState();

  useEffect(() => {
    if (id) {
      getProductById(id)
        .then((res) => {
          setProduct(res.data);
        })
        .catch((error) => {
          console.log(error);
        });
    }

    if (localStorage.getItem("accId") !== null) {
      setIsLogin(true);
    }
  }, []);

  const postComment = () => {
    const data = {
      accId: localStorage.getItem("accId"),
      proId: id,
      rate: newRate,
      comment: newComment,
    };

    addNewPost(data)
      .then((res) => {
        setSuccess(res.data.message);
      })
      .catch((error) => {
        setSuccess("You have reviewed this book before");
      });
  };

  const addToCart = (e, id) => {
    e.preventDefault();
    let cartId = localStorage.getItem("cartId");
    let accId = localStorage.getItem("accId");
    let token = localStorage.getItem("token");
    if (!accId) {
      alert("Log In First");
    }

    if (!cartId) {
      getCartByAccId(accId, token)
        .then((res) => {
          localStorage.setItem("cartId", res.data.id);
        })
        .catch((error) => {
          console.log(error);
        });
    }

    if (quantity === 0) {
      alert("Quantity must not be 0");
    }

    const data = {
      proId: id,
      cartId: localStorage.getItem("cartId"),
      quantity: quantity,
    };

    addProductToCart(data, token)
      .then((res) => {
        alert(res.data.message);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  if (product) {
    return (
      <div
        className="show-product w-100 d-flex flex-column align-items-center"
        style={{ margin: "3rem 0" }}
      >
        <div
          className="product-details d-flex w-75 justify-content-center"
          style={{
            padding: "3rem",
            boxShadow: " 0px 1px 4px rgba(0, 0, 0, 0.16)",
          }}
        >
          <div
            className="product-image w-25"
            style={{ width: "20rem", height: "30rem", marginRight: "2rem" }}
          >
            <Carousel>
              {(product.productImages || []).map((img) => (
                <Carousel.Item key={img.id}>
                  <img
                    src={img.imgUrl}
                    alt="First slide"
                    className="w-100 d-block"
                    style={{ width: "20rem", height: "30rem" }}
                  />
                </Carousel.Item>
              ))}
            </Carousel>
          </div>
          <div className="product-info w-75">
            <div className="info">
              <div className="cate text-muted fs-5  ">
                {product.category.name}
              </div>
              <div className="name fs-1 text-break">{product.name}</div>
              <div className="star">
                <Rating
                  name="half-rating-read small"
                  defaultValue={product.rate}
                  precision={0.5}
                  readOnly
                />
              </div>
              <div className="price fs-4 fw-bold">$ {product.price}</div>
            </div>
            <div className="btn d-flex justify-content-start align-content-center">
              <TextField
                type="number"
                label="Quantity"
                value={quantity}
                onChange={(e) => setQuantity(e.target.value)}
                style={{ width: "5rem", marginRight: "1rem" }}
              ></TextField>
              <Button
                variant="primary"
                onClick={(e) => addToCart(e, product.proId)}
              >
                Add To Cart
              </Button>
            </div>
          </div>
        </div>
        <div className="product-show-more w-75">
          <div
            className="product-description w-100 text-center"
            style={{
              padding: "1rem",
              boxShadow: " 0px 1px 4px rgba(0, 0, 0, 0.16)",
              marginTop: "1rem",
            }}
          >
            <div className="fs-5 text-decoration-underline">Description: </div>
            <div className=" text-break">{product.description}</div>
          </div>
          <div
            className="product-rate "
            style={{
              boxShadow: " 0px 1px 4px rgba(0, 0, 0, 0.16)",
              marginTop: "1rem",
              padding: "1rem",
            }}
          >
            <div className="show-rate">
              {(product.productRates || []).map((ele) => (
                <div className="rate">
                  <div className="star">
                    <Rating
                      name="half-rating-read small"
                      defaultValue={ele.rate}
                      precision={0.5}
                      readOnly
                    />
                  </div>
                  <div className="comment">{ele.comment}</div>
                </div>
              ))}
            </div>

            <div
              className="new-rate border-top d-flex justify-content-between"
              style={{ padding: "1rem" }}
            >
              {isLogin ? (
                <>
                  <div className="fs-4 text-muted">
                    Write your comment:<br></br>{" "}
                    <small className="text-success">{success}</small>
                  </div>
                  <Rating
                    name="half-rating-read small"
                    defaultValue={newRate}
                    precision={0.5}
                    onChange={(e) => setNewRate(e.target.value)}
                  />
                  <TextField
                    id="outlined-basic"
                    label="Comment"
                    variant="outlined"
                    value={newComment}
                    onChange={(e) => setNewComment(e.target.value)}
                    type="text"
                    className="w-50"
                  />

                  <Button variant="primary" onClick={postComment}>
                    Post
                  </Button>
                </>
              ) : (
                <div>
                  {" "}
                  Want to write comment? <Link to={"/login"}>Login Now</Link>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    );
  } else {
    return <div>fail</div>;
  }
};

export default ViewProduct;
