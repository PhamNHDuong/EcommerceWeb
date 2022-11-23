import axios from "axios";
import { URI } from "./commons";

export const getAllCategory = async () => {
  const res = await axios.get(`${URI}/Categories`);
  return res;
};
export const getAvailableCategory = async () => {
  const res = await axios.get(`${URI}/Categories/available`);
  return res;
};
export const createCategory = async (bodyFormData) => {
  const res = await axios({
    method: "POST",
    url: `${URI}/Categories/create`,
    data: bodyFormData,
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return res;
};

export const getByCategory = async (id) => {
  const res = await axios.get(`${URI}/Categories/${id}`);
  return res;
};

export const updateCategory = async (id, bodyData) => {
  const res = await axios.put(`${URI}/Categories/update?id=${id}`, bodyData);
  return res;
};

export const disableCategory = async (id) => {
  const res = await axios.patch(`${URI}/Categories/disable?id=${id}`);
  return res;
};

export const enableCategory = async (id, bodyData) => {
  const res = await axios.patch(`${URI}/Categories/enable?id=${id}`);
  return res;
};
