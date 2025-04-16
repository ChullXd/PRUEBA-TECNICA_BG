import { Typography } from "@mui/material";

interface Props {
  id: string;
}

export const ProductId = ({ id }: Props) => {
  return <Typography>{id}</Typography>;
};
