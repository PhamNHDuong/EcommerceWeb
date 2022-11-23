import axios from "axios";
import { URI } from "./commons";

export const getAllUser = async () => {
  const res = await axios.get(`${URI}/Users/getUsers`);
  console.log(typeof res);
  return res;
};

export const disableUser = async (id) => {
  const res = await axios.patch(`${URI}/Users/disable?id=${id}`);
  return res;
};

export const enableUser = async (id, bodyData) => {
  const res = await axios.patch(`${URI}/Users/enable?id=${id}`);
  return res;
};

export const loginAdmin = async (login) => {
  const res = await axios.post(`${URI}/Users/login`, login);
  return res;
}