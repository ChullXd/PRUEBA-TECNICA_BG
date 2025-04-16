import { baseApiConfig, PRODUCT_BASE_URL } from "../config";
import { createApi } from "@reduxjs/toolkit/query/react";
import { Product } from "../../../project/interface";
export const productController = createApi({
  reducerPath: "productController",
  baseQuery: baseApiConfig,
  tagTypes: ["Product"],
  endpoints: (build) => ({
    getProducts: build.query<Product[], void>({
      query: () => PRODUCT_BASE_URL,
      providesTags: ["Product"],
    }),
    postProduct: build.mutation<void, Product>({
      query: (body) => ({
        url: PRODUCT_BASE_URL,
        method: "POST",
        body,
      }),
    }),
    putProduct: build.mutation<void, Product>({
      query: (body) => ({
        url: PRODUCT_BASE_URL,
        method: "PUT",
        body,
      }),
    }),
    deleteProduct: build.mutation<void, string>({
      query: (id) => ({
        url: `${PRODUCT_BASE_URL}/${id}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const {
  useLazyGetProductsQuery,
  usePostProductMutation,
  usePutProductMutation,
  useDeleteProductMutation,
} = productController;
