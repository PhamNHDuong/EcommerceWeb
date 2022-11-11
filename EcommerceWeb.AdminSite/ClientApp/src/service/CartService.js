import axios from "axios";

const baseUrl = "http://localhost:8080/api/v1/cart";

export const getCartByAccId = (accId, token) => {
  return axios({
    headers: {
      "content-type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    url: baseUrl + `/${accId}`,
    method: "GET",
  });
};

export const addProductToCart = (data, token) => {
  return axios({
    headers: {
      "content-type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    url: baseUrl,
    method: "POST",
    data: data,
  });
};
