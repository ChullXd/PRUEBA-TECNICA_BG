import { baseApiConfig, LOGIN_BASE_URL } from "../config";
import { JWTInfo, LoginInfo } from "../../../auth";
import { createApi } from "@reduxjs/toolkit/query/react";
export const authController = createApi({
  reducerPath: "authController",
  baseQuery: baseApiConfig,
  endpoints: (build) => ({
    //POST
    postLogin: build.mutation<JWTInfo, LoginInfo>({
      query: (body) => ({
        url: LOGIN_BASE_URL,
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body,
      }),
    }),
  }),
});

export const { usePostLoginMutation } = authController;
