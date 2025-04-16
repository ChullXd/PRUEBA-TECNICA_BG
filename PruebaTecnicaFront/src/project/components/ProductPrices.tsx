import { Box, Chip } from "@mui/material";
import { ProductPrice } from "../interface";

interface Props {
  prices: ProductPrice[];
  desc?: boolean;
}

export const ProductPrices = ({ prices, desc = false }: Props) => {
  return (
    <Box>
      {prices.map((price) => (
        <Box key={price.id}>
          <Chip
            key={price.id}
            label={`$ ${price.price.toFixed(2)} - $${
              desc ? (price.price * 0.7).toFixed(2) : ""
            } - ${price.store}`}
            sx={{ margin: "4px" }}
          />
        </Box>
      ))}
    </Box>
  );
};
