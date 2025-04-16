import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "..";

export const useAppStore = () => {
  //*Auth
  const auth = useSelector((store: RootState) => store.auth);
  const product = useSelector((store: RootState) => store.product);

  const dispatch = useDispatch<AppDispatch>();
  return { auth, product, dispatch };
};
