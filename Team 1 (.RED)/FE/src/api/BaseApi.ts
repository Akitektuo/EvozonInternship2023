import axios, { AxiosInstance, AxiosRequestConfig } from "axios";

const httpConfig: AxiosRequestConfig = {
  baseURL: process.env.REACT_APP_BASE_URL,
  timeout: 30000,
  headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json'
  }
};

export const BaseApi: AxiosInstance = axios.create(httpConfig);