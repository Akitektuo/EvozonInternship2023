import axios, { AxiosInstance, AxiosRequestConfig } from "axios";
import { userServiceStore } from "../services/userService/UserService.store";

const httpConfig: AxiosRequestConfig = {
  baseURL: process.env.REACT_APP_BASE_URL,
  timeout: 50000,
  headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json'
  }
};

export const getAuthorizationHeader = () => ({headers: {Authorization: `Bearer ${userServiceStore.userData?.token}`}});

export const BaseApi: AxiosInstance = axios.create(httpConfig);