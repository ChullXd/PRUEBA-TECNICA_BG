import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions,
  Button,
  IconButton,
} from "@mui/material";
import Swal from "sweetalert2";
import { useForm } from "../../hooks";
import { Product } from "../interface";
import { PriceField } from "./PriceField";
import { Add } from "@mui/icons-material";
import {
  useLazyGetProductsQuery,
  usePostProductMutation,
} from "../../services";
import { useProductStore } from "../../shared";

interface Props {
  open: boolean;
  onClose: () => void;
  onSave: () => void;
  onCancel: () => void;
}

export const ProductDialogAdd = ({ open = false, onClose }: Props) => {
  const { onSetProducts } = useProductStore();
  const { formState, onChange, isFormValid, errors } = useForm<Product>(
    {
      id: null,
      name: "",
      description: "",
      productPrices: [],
      quantity: 0,
    },
    {
      name: [(value) => value.length > 2, "Ingrese un nombre válido"],
      description: [(value) => value.length > 2, "Ingrese un codigo válido"],
      productPrices: [
        (value) => value.every((p) => p.price > 0 && p.store.length > 2),
        "Ingrese almenos un precio valido",
      ],
      quantity: [(value) => value > 0, "Ingrese un correo válido"],
    }
  );
  const [fetchPostProduct, { isLoading }] = usePostProductMutation();
  const [fetchGetProducts, { isLoading: isLoadingProducts }] =
    useLazyGetProductsQuery();

  const onPressSave = async () => {
    console.log(JSON.stringify(formState));
    return await fetchPostProduct(formState)
      .unwrap()
      .then(async () => await fetchGetProducts().unwrap().then(onSetProducts))
      .catch((error) => {
        Swal.fire("Error", error?.data?.detail ?? "Ocurrió un error", "error");
        throw error;
      });
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Agregar Producto</DialogTitle>
      <DialogContent>
        <TextField
          autoFocus
          margin="dense"
          label="Nombre"
          type="text"
          fullWidth
          value={formState.name}
          onChange={({ target: { value } }) => onChange("name", value)}
          error={!!errors.name}
          helperText={errors.name}
        />
        <TextField
          margin="dense"
          name="descripcion"
          label="Descripción"
          type="text"
          fullWidth
          value={formState.description}
          onChange={({ target: { value } }) => onChange("description", value)}
          error={!!errors.description}
          helperText={errors.description}
        />
        <TextField
          margin="dense"
          name="cantidad"
          label="Cantidad"
          type="number"
          fullWidth
          value={formState.quantity}
          onChange={({ target: { value } }) =>
            onChange("quantity", Number(value))
          }
          error={!!errors.quantity}
          helperText={errors.quantity}
        />
        <>
          {formState.productPrices.map((price, index) => (
            <PriceField
              key={index}
              price={price}
              onPriceChange={(value) =>
                onChange("productPrices", [
                  ...formState.productPrices.slice(0, index),
                  value,
                  ...formState.productPrices.slice(index + 1),
                ])
              }
              onDeletePrice={() =>
                onChange("productPrices", [
                  ...formState.productPrices.slice(0, index),
                  ...formState.productPrices.slice(index + 1),
                ])
              }
            />
          ))}
          <IconButton
            onClick={() =>
              onChange("productPrices", [
                ...formState.productPrices,
                {
                  id: null,
                  price: 0,
                  store: "",
                },
              ])
            }
          >
            <Add />
          </IconButton>
        </>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={onClose}
          color="error"
          disabled={isLoading || isLoadingProducts}
        >
          Cancelar
        </Button>
        <Button
          onClick={async () => {
            await onPressSave()
              .then(() => {
                Swal.fire({
                  title: "Producto agregado",
                  text: "Producto agregado correctamente",
                  icon: "success",
                  confirmButtonColor: "#3085d6",
                });
              })
              .finally(() => onClose());
          }}
          disabled={!isFormValid() || isLoading || isLoadingProducts}
          color="primary"
        >
          Guardar
        </Button>
      </DialogActions>
    </Dialog>
  );
};
