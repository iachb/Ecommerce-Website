import axios from "axios";
import {config} from "../constants/AppConstants";

const BASE_URL = config.url.API_URL;
axios.default.baseUrl = BASE_URL;

export default axios;