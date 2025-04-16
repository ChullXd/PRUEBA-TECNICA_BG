import { Typography } from "@mui/material";

interface Props {
  name: string;
}

export const ProductName = ({ name }: Props) => {
  return <Typography>{name}</Typography>;
};
