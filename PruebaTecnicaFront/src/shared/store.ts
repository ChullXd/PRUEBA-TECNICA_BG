import { authSlice } from "./auth";

import { authController, productController } from "../services";
import { configureStore } from "@reduxjs/toolkit";
import { productSlice } from "./products";

export const store = configureStore({
  reducer: {
    //*Auth
    auth: authSlice.reducer,

    //*Product
    product: productSlice.reducer,
    //*Api
    [authController.reducerPath]: authController.reducer,
    [productController.reducerPath]: productController.reducer,
  },

  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    })
      .concat(authController.middleware)
      .concat(productController.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
