import axios from "axios";
import { URI } from "./commons";

export const getAllProduct = async () => {
  const res = await axios.get(`${URI}/Products`);
  return res;
  /*     const ress = await axios({
        method:'post',
        data:{
            name: "",
            id: "",
        },
        headers:{
            'content-type':'application/json',
        }
    })
    return ress.json(); */
};
export const createProduct = async (bodyFormData) => {
  // axios.post(`${URI}/Products/create`, bodyFormData)
  //   .then(function (response) {
  //     console.log(response);
  //   })
  //   .catch(function (error) {
  //     console.log(error);
  //   });
  const res = await axios({
    method: "POST",
    url: `${URI}/Products/create`,
    data: bodyFormData,
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return res;
};

export const getByProduct = async (id) => {
  const res = await axios.get(`${URI}/Products/a-${id}`);
  return res;
};

export const updateProduct = async (id, bodyData) => {
  const res = await axios.put(`${URI}/Products/update?id=${id}`, bodyData);
  return res;
};

export const disableProduct = async (id) => {
  const res = await axios.patch(`${URI}/Products/disable?id=${id}`);
  return res;
};

export const enableProduct = async (id, bodyData) => {
  const res = await axios.patch(`${URI}/Products/enable?id=${id}`);
  return res;
};
