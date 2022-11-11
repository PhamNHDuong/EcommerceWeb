import { useEffect, useState } from "react";
import { useParams, Navigate } from "react-router-dom";
import { getAllCategory } from "../../service/CategoryService.js";
import { getAllProductTradingByCateId } from "../../service/ProductService.js";
import { Card, Pagination } from "react-bootstrap";

import {
  Box,
  List,
  ListItemButton,
  ListItemText,
  FormControl,
  InputLabel,
  MenuItem,
  Rating,
  Select,
} from "@mui/material";

const Home = (props) => {
  const [cate, setCate] = useState([]);
  const [selected, setSelected] = useState(0);
  const [product, setProduct] = useState([]);
  const [proId, setProId] = useState();
  const [sortBy, setSortBy] = useState(0);
  const [activePage, setActivePage] = useState(1);

  let { type } = useParams();
  if (!type) {
    type = "None";
  }

  const getData = () => {
    let token = localStorage.getItem("token");
    getAllCategory(token)
      .then((res) => {
        setCate(res.data);
      })
      .catch((error) => {
        console.log(error);
      });

    const id = selected === 0 ? "all" : selected;
    getAllProductTradingByCateId(id)
      .then((res) => {
        setProduct(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  useEffect(() => {
    getData();
  }, [selected]);

  let n = product.length / 20;
  let items = [];
  for (let number = 1; number <= n; number++) {
    items.push(
      <Pagination.Item
        key={number}
        active={number === activePage}
        onClick={(e) => setActivePage(number)}
      >
        {number}
      </Pagination.Item>
    );
  }

  const sort = (e) => {
    let value = e.target.value;
    if (value === 1) {
      product.sort((a, b) => {
        return a.price - b.price;
      });
    } else if (value === 2) {
      product.sort((a, b) => {
        return b.price - a.price;
      });
    } else if (value === 3) {
      product.sort((a, b) => {
        return a.rate - b.rate;
      });
    } else if (value === 4) {
      product.sort((a, b) => {
        return b.rate - a.rate;
      });
    }
    setSortBy(value);
  };
  return (
    <>
      {proId && <Navigate to={`/home/view/${proId}`} />}

      <div
        className="home"
        style={{
          margin: "3rem 0",
          textAlign: " -webkit-center",
          width: "100%",
        }}
      >
        <div className="main-home d-flex">
          <div className="category">
            <Box
              sx={{ width: "100%", maxWidth: 360, bgcolor: "background.paper" }}
            >
              <List component="nav" aria-label=" folder">
                <ListItemButton
                  selected={selected === 0}
                  onClick={(e) => setSelected(0)}
                >
                  <ListItemText primary="All Category" />
                </ListItemButton>
                {cate.map((ele) => (
                  <ListItemButton
                    selected={selected === ele.id}
                    onClick={(e) => setSelected(ele.id)}
                  >
                    <ListItemText primary={ele.name} />
                  </ListItemButton>
                ))}
              </List>
            </Box>
          </div>
          <div className="show-product">
            <div className="view" style={{ border: "1px solid gainsboro" }}>
              <div className="me-2 filter text-start">
                <FormControl sx={{ m: 1, minWidth: 100 }}>
                  <InputLabel id="demo-simple-select-autowidth-label">
                    Sort By
                  </InputLabel>
                  <Select
                    labelId="demo-simple-select-autowidth-label"
                    id="demo-simple-select-autowidth"
                    value={sortBy}
                    onChange={sort}
                    autoWidth
                    label="Sort By"
                  >
                    <MenuItem value={0}>
                      <em>All</em>
                    </MenuItem>
                    <MenuItem value={1}>Prices Increase</MenuItem>
                    <MenuItem value={2}>Prices Decrease</MenuItem>
                    <MenuItem value={3}>Stars Increase</MenuItem>
                    <MenuItem value={4}>Stars Decrease</MenuItem>
                  </Select>
                </FormControl>
              </div>
              <div className="view-all-product d-flex flex-wrap justify-content-evenly">
                {product.map((ele, idx) => (
                  <Card
                    key={idx}
                    onClick={(e) => setProId(ele.proId)}
                    border="light"
                    style={{
                      width: "18rem",
                      marginTop: "2rem",
                      padding: ".5rem",
                    }}
                    className="cart"
                  >
                    <Card.Img variant="top" src={ele.thumbnail} />
                    <Card.Body>
                      <Card.Title>{ele.name}</Card.Title>
                      <Card.Subtitle className="text-muted">
                        <span> $ {ele.price} </span>
                        <br></br>
                        <span>
                          <Rating
                            name="half-rating-read small"
                            defaultValue={ele.rate}
                            precision={0.5}
                            readOnly
                          />
                        </span>
                      </Card.Subtitle>
                    </Card.Body>
                  </Card>
                ))}
              </div>

              <div className="pagination justify-content-center m-1">
                <Pagination>{items}</Pagination>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Home;
