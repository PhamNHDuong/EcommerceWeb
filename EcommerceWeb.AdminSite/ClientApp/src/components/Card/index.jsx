import moment from "moment";
import { useEffect, useState } from "react";
import { Card } from "react-bootstrap";
import Product from "../Product";

export const CardHorizontal = (props) => {
  const [urlImage, setUrlImage] = useState([]);

  useEffect(() => {
    setUrlImage(props.product.productImages);
  }, []);

  const getImages = () => {
    let list = [...urlImage];
    if (list.length === 0) {
      return <p>No images</p>;
    }

    let firstEle = list.pop();

    return (
      <>
        <div className="carousel-item active" key={firstEle.id}>
          <img src={firstEle.imgUrl} className="d-block w-100" alt="..." />
        </div>
        {list.map((ele) => (
          <div className="carousel-item" key={ele.id}>
            <img src={ele.imgUrl} className="d-block w-100" alt="..." />
          </div>
        ))}
      </>
    );
  };

  return (
    <div className="card mb-3">
      <div className="row g-0">
        <div className="col-md-4">
          <div
            id={"carouselExampleControls" + props.product.proId}
            className="carousel slide"
            data-bs-ride="carousel"
          >
            <div className="carousel-inner">{urlImage && getImages()}</div>
            <button
              className="carousel-control-prev"
              type="button"
              data-bs-target={"#carouselExampleControls" + props.product.proId}
              data-bs-slide="prev"
            >
              <span
                className="carousel-control-prev-icon"
                aria-hidden="true"
              ></span>
              <span className="visually-hidden">Previous</span>
            </button>
            <button
              className="carousel-control-next"
              type="button"
              data-bs-target={"#carouselExampleControls" + props.product.proId}
              data-bs-slide="next"
            >
              <span
                className="carousel-control-next-icon"
                aria-hidden="true"
              ></span>
              <span className="visually-hidden">Next</span>
            </button>
          </div>
        </div>

        <div className="col-md-7">
          <div className="card-body">
            <h6 className="card-subtitle mb-2 text-muted">
              {props.product.category.name}
            </h6>
            <h5 className="card-title">
              {props.product.name} - {Number.parseFloat(props.product.rate).toFixed(1)}
              <span className="fa fa-star star ms-1"></span>-
              <span className=" text-muted ms-1">
                {props.product.status ? "On Trading" : "Stop Trade"}
              </span>
            </h5>
            <h6 className="card-subtitle mb-2 text-muted">
              $ {props.product.price}
            </h6>
            <h6 className="card-subtitle mb-2 text-muted">
              {props.product.amount}
            </h6>
            <p className="card-text">{props.product.description}</p>
            <p className="card-text">
              <small className="text-muted">
                Last update {moment(props.product.lastUpdate).format("LLL")} -
                Create date {moment(props.product.createDate).format("LLL")}
              </small>
            </p>
          </div>
        </div>

        <div className="col-md-1 align-self-end">
          <div className="product-add">
            {/* Button trigger modal */}
            <button
              type="button"
              data-bs-toggle="modal"
              data-bs-target={"#viewProduct" + props.product.proId}
              className="btn btn-primary me-1"
            >
              More
            </button>

            {/* modal */}
            <div
              className="modal fade "
              id={"viewProduct" + props.product.proId}
              data-bs-backdrop="static"
              data-bs-keyboard="false"
              tabIndex="-1"
              aria-labelledby="staticBackdropLabel"
              aria-hidden="true"
            >
              <Product action="view" product={props.product} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export const CardVertical = (props) => {
  return (
    <Card style={{ width: "18rem" }}>
      <Card.Img
        variant="top"
        src="https://sachdenroi.com/wp-content/uploads/2020/04/di-tim-le-song-2-2.jpg"
      />
      <Card.Body>
        <Card.Title className="text-muted fs-5 overflow-hidden">
          Man's Search For Meaning
        </Card.Title>
        <Card.Text>
         
        </Card.Text>
      </Card.Body>
    </Card>
  );
};