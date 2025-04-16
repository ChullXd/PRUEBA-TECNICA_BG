import { IconButton, TextField } from "@mui/material";
import { ProductPrice } from "../interface";
import { useForm } from "../../hooks";
import { useEffect } from "react";
import { Remove } from "@mui/icons-material";

interface Props {
  price: ProductPrice;
  onPriceChange: (price: ProductPrice) => void;
  onDeletePrice: () => void;
}

export const PriceField = ({ price, onPriceChange, onDeletePrice }: Props) => {
  const { formState, onChange, errors } = useForm<ProductPrice>(
    {
      ...price,
    },
    {
      price: [(value) => value > 0, "Ingrese un precio valido"],
      store: [(value) => value.length > 0, "Ingrese un nombre válido"],
    }
  );

  useEffect(() => {
    onPriceChange(formState);
  }, [formState]);

  return (
    <>
      <TextField
        margin="dense"
        label="Precio"
        type="number"
        fullWidth
        value={formState.price}
        onChange={({ target: { value } }) => onChange("price", Number(value))}
        error={!!errors.price}
        helperText={errors.price}
      />
      <TextField
        margin="dense"
        label="Tienda"
        type="text"
        fullWidth
        value={formState.store}
        onChange={({ target: { value } }) => onChange("store", value)}
        error={!!errors.store}
        helperText={errors.store}
      />
      <IconButton onClick={onDeletePrice} color="error">
        <Remove />
      </IconButton>
    </>
  );
};
