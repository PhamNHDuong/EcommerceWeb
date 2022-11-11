import CreateProduct from "./CreateProduct";
import ViewAndEditProduct from "./ViewAndEditProduct";

const Product = (props) => {
  if (props.action === "add") return <CreateProduct />;
  else return <ViewAndEditProduct product={props.product} />;
};

export default Product;
