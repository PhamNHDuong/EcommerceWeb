import axios from "axios";
import { URI } from "./commons";

export const createProductImage = async (data) => {
    const res = await axios.post(`${URI}/Images/`, data);
    return res;
  };