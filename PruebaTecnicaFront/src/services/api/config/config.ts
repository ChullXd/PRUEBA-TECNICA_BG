import { fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BASE_API, BASE_URL } from "./routes";
import { RootState } from "../../../shared";

export const baseApiConfig = fetchBaseQuery({
  baseUrl: BASE_URL + BASE_API,
  prepareHeaders: (headers, { getState }) => {
    headers.set("Accept", "application/json");
    headers.set("Content-Type", "application/json");
    headers.set(
      "Authorization",
      `Bearer ${(getState() as RootState).auth.jwtInfo.token}`
    );
    return headers;
  },
});
