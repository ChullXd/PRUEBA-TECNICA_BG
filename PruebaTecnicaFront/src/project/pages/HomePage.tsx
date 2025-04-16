import { Add } from "@mui/icons-material";
import {
  OptionsTable,
  ProductDesc,
  ProductId,
  ProductPrices,
} from "../components";
import { ProductName } from "../components/ProductName";
import { BasePage } from "../template";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Typography,
  Fab,
} from "@mui/material";
import { useEffect, useState } from "react";
import { ProductDialogAdd, ProductDialogEdit } from "../dialog";
import { useProductStore } from "../../shared";
import {
  useDeleteProductMutation,
  useLazyGetProductsQuery,
} from "../../services";
import { Product } from "../interface";
import Swal from "sweetalert2";

const columns = [
  { id: "id", label: "Usuario" },
  { id: "name", label: "Nombre" },
  { id: "desc", label: "Apellido" },
  { id: "price", label: "Departamento" },
  { id: "price", label: "Cargo" },
  { id: "price", label: "Email" },
  { id: "options", label: "Opciones" },
];

export const HomePage = () => {
  const [isEditVisible, setisEditVisible] = useState(false);
  const [isAddVisible, setisAddVisible] = useState(false);

  const { onSetActiveProduct, products, onSetProducts } = useProductStore();
  const [fetchGetProducts, { isLoading }] = useLazyGetProductsQuery();
  const [fetchDeleteProduct] = useDeleteProductMutation();

  const onPressDeleteProduct = async (product: Product) => {
    await fetchDeleteProduct(product.id ?? "")
      .unwrap()
      .then(async () => {
        Swal.fire({
          title: "Eliminado",
          text: "Producto eliminado correctamente",
          icon: "success",
          confirmButtonColor: "#3085d6",
        });
        await fetchGetProducts().unwrap().then(onSetProducts);
      })
      .catch((error) => {
        Swal.fire("Error", error?.data?.detail ?? "Ocurrió un error", "error");
      });
  };

  useEffect(() => {
    fetchGetProducts().unwrap().then(onSetProducts);
  }, []);

  return (
    <BasePage>
      {products.length === 0 ? (
        <Typography>{`${
          isLoading ? " Cargando..." : "No hay productos para mostrar"
        } `}</Typography>
      ) : (
        <TableContainer component={Paper}>
          <Table stickyHeader aria-label="tabla de productos">
            <TableHead>
              <TableRow>
                {columns.map((column) => (
                  <TableCell key={column.id} align={"center"}>
                    {column.label}
                  </TableCell>
                ))}
              </TableRow>
            </TableHead>
            <TableBody>
              {products.map((product) => (
                <TableRow hover role="checkbox" key={product.id}>
                  {columns.map((column) => (
                    <TableCell
                      key={column.id}
                      align={"center"}
                      sx={{ maxWidth: { sm: 100 } }}
                    >
                      {
                        {
                          Usuario: <ProductId id={product.id ?? ""} />,
                          Nombre: <ProductName name={product.name} />,
                          Apellido: (
                            <ProductDesc desc={product.description} />
                          ),
                          Departamento: (
                            <ProductPrices
                              prices={product.productPrices ?? []}
                              desc={product.quantity <= 10}
                            />
                          ),
                          Cargo: <Typography>{product.quantity}</Typography>,
                          Email: <Typography>{product.quantity}</Typography>,
                          Opciones: (
                            <OptionsTable
                              onEdit={() => {
                                onSetActiveProduct(product);
                                setisEditVisible(true);
                              }}
                              onDelete={() => {
                                Swal.fire({
                                  title: "¿Estás seguro?",
                                  text: "¡No podrás revertir esta acción!",
                                  icon: "warning",
                                  showCancelButton: true,
                                  confirmButtonColor: "#3085d6",
                                  cancelButtonColor: "#d33",
                                  confirmButtonText: "Sí, eliminar",
                                  cancelButtonText: "Cancelar",
                                  showLoaderOnConfirm: true,
                                }).then(async (result) => {
                                  if (result.isConfirmed) {
                                    // Lógica para eliminar el producto
                                    // Por ejemplo, una llamada a una API para eliminar el producto
                                    await onPressDeleteProduct(product);
                                    // Mostrar notificación de éxito
                                  }
                                });
                              }}
                            />
                          ),
                        }[column.label]
                      }
                    </TableCell>
                  ))}
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
      <Fab
        color="primary"
        aria-label="add"
        style={{
          position: "fixed",
          bottom: 16,
          right: 16,
        }}
        onClick={() => setisAddVisible(true)}
      >
        <Add />
      </Fab>
      <ProductDialogAdd
        open={isAddVisible}
        onClose={() => setisAddVisible(false)}
        onSave={() => {}}
        onCancel={() => setisAddVisible(false)}
      />
      <ProductDialogEdit
        open={isEditVisible}
        onClose={() => setisEditVisible(false)}
      />
    </BasePage>
  );
};
