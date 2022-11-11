import axios from "axios";

const baseUrl = "http://localhost:8080/api/v1/auth/";
const header = {
  "content-type": "application/json",
};
export const LogIn = (data) => {
  return axios({
    headers: header,
    url: baseUrl + "signin",
    data: data,
    method: "POST",
  });
};

export const Register = (data) => {
  return axios({
    headers: header,
    url: baseUrl + "register",
    data: data,
    method: "POST",
  });
};
