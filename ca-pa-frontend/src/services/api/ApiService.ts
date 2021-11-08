import { CommonError } from '../../errors/common.error'
import axios, { AxiosRequestConfig, AxiosInstance } from 'axios'
import qs from 'qs'
import { RasaClient } from './_generated/test-generated-axios-backend-api'

const axiosInstance: AxiosInstance = axios.create({
  baseURL: process.env.REACT_APP_BACKEND_URL,
  timeout: 30000,
  paramsSerializer: (params) => {
    return qs.stringify(params, { indices: false, skipNulls: true }).replaceAll('%5B', '.').replaceAll('%5D', '');
  },
  withCredentials: false, // maybe remove after backend integration
});

export const rasaClient = new RasaClient(undefined, axiosInstance)

export const setAccessToken = (token: string) => {
  axios.defaults.headers.common = { 'Authorization': `Bearer ${token}` }
}

// Add a request interceptor
axiosInstance.interceptors.request.use(
  function (config) {
    // Do something before request is sent
    console.log(config);

    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

// Add a response interceptor
axiosInstance.interceptors.response.use(
  function (response) {
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    return response;
  },
  async function (errorObj) {
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    if (errorObj.code === 'ECONNABORTED' || errorObj.response.status === 408) {
      return Promise.reject(CommonError.TIMEOUT);
    }
    const error = errorObj.response;
    if (error) {
      if (error.status === 401) {
        return Promise.reject(CommonError.UNAUTHORIZED);
      }
      else if (error.status === 403) {
        return Promise.reject(CommonError.FORBIDDEN);
      }
      else if (error.status === 409) {
        throw error.data;
      }
      else if (error.data) {
        // error from server
        return Promise.reject('backend.error.' + error.data.error);
      }
      else if (error.status === 500) {
        // unhandled error
        return Promise.reject(CommonError.STATUS_500);
      }
      else if (error instanceof SyntaxError) {
        return Promise.reject(CommonError.SYNTAX);
      }
    }

    return Promise.reject(CommonError.UNKNOWN_API_RELATED);
  }
);

/**
 * Send request
 */

async function send({
  method,
  url,
  data,
  responseType = 'json',
  headers = {},
  params,
  customConfig = {}
}: any) {
  const config = {
    method,
    url,
    data,
    params,
    headers,
    responseType,
    ...customConfig,
  } as AxiosRequestConfig;

  if (method === 'GET') {
    config.params = params;
    config.data = null;
  }

  // const token = localStorage.getItem('accessToken');
  // if (token) {
  //   config.headers.Authorization = `Bearer ${token}`;
  // }

  return axiosInstance
    .request(config)
    .then((response) => (config.responseType === 'blob' ? response : response.data))
    .catch((error) => Promise.reject(error));
}

/**
 * ApiService
 */

const ApiService = {
  get: (url: string, params?: any, customConfig?: AxiosRequestConfig) => {
    return send({
      method: 'GET',
      url,
      params,
      customConfig,
    } as any);
  },

  delete: (url: string, params?: any, customConfig?: AxiosRequestConfig) => {
    return send({
      method: 'DELETE',
      url,
      params,
      customConfig,
    } as any);
  },

  deleteWithData: (url: string, data: any, params: any, customConfig?: AxiosRequestConfig) => {
    return send({
      method: 'DELETE',
      url,
      data,
      params,
      customConfig,
    });
  },

  post: (url: string, data?: any, customConfig?: AxiosRequestConfig) => {
    return send({
      method: 'POST',
      url,
      data,
      customConfig,
    } as any);
  },

  put: (url: string, data: any, customConfig?: AxiosRequestConfig) => {
    return send({
      method: 'PUT',
      url,
      data,
      customConfig,
    } as any);
  },

  patch: (url: string, data: any, customConfig?: AxiosRequestConfig) => {
    return send({
      method: 'PATCH',
      url,
      data,
      customConfig,
    } as any);
  },

  getBlob: (url: string, params: any, customConfig?: AxiosRequestConfig) => {
    return send({
      method: 'GET',
      url,
      responseType: 'blob',
      params,
      customConfig,
    } as any);
  },

  postFormData: (url: string, data: any, customConfig: AxiosRequestConfig) => {
    return send({
      method: 'POST',
      url,
      data,
      headers: { 'Content-Type': 'multipart/form-data' },
      customConfig,
    } as any);
  },

  putFormData: (url: string, data: any, customConfig: AxiosRequestConfig) => {
    return send({
      method: 'PUT',
      url,
      data,
      headers: { 'Content-Type': 'multipart/form-data' },
      customConfig,
    } as any);
  },
};

export default ApiService;
