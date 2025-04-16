import { Product } from "../../project/interface";
import { setActiveProduct, setProducts } from "../products";
import { useAppStore } from "./useAppStore";

export const useProductStore = () => {
  const {
    product: { activeProduct, products },
    dispatch,
  } = useAppStore();

  const onSetActiveProduct = (product: Product) => {
    dispatch(setActiveProduct(product));
  };

  const onSetProducts = (products: Product[]) => {
    dispatch(setProducts(products));
  };

  return { activeProduct, products, onSetActiveProduct, onSetProducts };
};
