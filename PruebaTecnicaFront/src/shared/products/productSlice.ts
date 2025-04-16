import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Product } from "../../project/interface";

interface InitialStateInterface {
  activeProduct: Product;
  products: Product[];
}

const initialState: InitialStateInterface = {
  activeProduct: {
    id: null,
    name: "",
    description: "",
    productPrices: [],
    quantity: 0,
  },
  products: [],
};

export const productSlice = createSlice({
  name: "product",
  initialState,
  reducers: {
    setProducts: (state, action: PayloadAction<Product[]>) => {
      state.products = action.payload;
    },
    setActiveProduct: (state, action: PayloadAction<Product>) => {
      state.activeProduct = action.payload;
    },
  },
});

export const { setActiveProduct, setProducts } = productSlice.actions;
