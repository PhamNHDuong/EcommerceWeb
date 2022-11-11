import axios from "axios";

const baseUrl = "http://localhost:8080/api/v1/product";

export const getAllProduct = (token) => {
  return axios({
    headers: {
      "content-type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    url: baseUrl + "/all",
    method: "GET",
  });
};

export const getAllProductTrading = () => {
  return axios({
    headers: {
      "content-type": "application/json",
    },
    url: baseUrl,
    method: "GET",
  });
};

export const getAllProductTradingByCateId = (id) => {
  return axios({
    headers: {
      "content-type": "application/json",
    },
    url: baseUrl + `/category/${id}`,
    method: "GET",
  });
};

export const updateProduct = (id, data, token) => {
  return axios({
    headers: {
      "content-type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    method: "PUT",
    url: baseUrl + `/${id}`,
    data: data,
  });
};

export const deleteProduct = (id, token) => {
  return axios({
    headers: {
      "content-type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    method: "PATCH",
    url: baseUrl + `/${id}`,
  });
};

export const createProduct = (data, token) => {
  return axios({
    headers: {
      "content-type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    method: "POST",
    url: baseUrl,
    data: data,
  });
};

export const getProductById = (id) => {
  return axios({
    headers: { "content-type": "application/json" },
    url: baseUrl + `/${id}`,
    method: "GET",
  });
};
