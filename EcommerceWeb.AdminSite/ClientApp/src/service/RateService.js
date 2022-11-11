import axios from "axios";

const header = {
  "content-type": "application/json",
};

const baseUrl = "http://localhost:8080/api/v1/rate";

export const getAllRateForProduct = (id) => {
  return axios({
    headers: header,
    url: baseUrl,
    method: "GET",
  });
};

export const addNewPost = (data) => {
  return axios({
    headers: header,
    url: baseUrl,
    method: "POST",
    data: data,
  });
};
