import axios from "axios";


const baseUrl = "http://localhost:8080/category";

export const getAllCategory = (token) => {
  return axios({
    headers: {
      "content-type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    url: baseUrl,
    method: "GET",
  });
};

export const getAllIdAndNameCategory = () => {
  return null;
};

export const updateCategory = (id, name, des) => {
  return axios({
    headers: { "content-type": "application/json" },
    method: "PUT",
    url: `http://localhost:8080/category/${id}`,
    data: {
      cateName: name,
      cateDescription: des,
    },
  });
};

export const deleteCategory = (id) => {
  return axios({
    headers: { "content-type": "application/json" },
    method: "PATCH",
    url: `http://localhost:8080/category/${id}`,
  });
};

export const createCategory = (name, des) =>{
  return axios({
    headers: { "content-type": "application/json" },
    method: "POST",
    url: `http://localhost:8080/category/`,
    data: {
      cateName: name,
      cateDescription: des,
    },
  })
}
// TODO: move different api relate to category to here.
