import axios from "axios";
import { config } from "../constants/appConstants";

const token = localStorage.getItem("token"); // Retrieve the token from local storage for authorization headers

const BASE_URL = config.url.API_URL;
axios.defaults.baseURL = BASE_URL;

axios.defaults.headers.common = {'Authorization': `Bearer ${token}`} // Set the default authorization header for all axios requests

export default axios;
