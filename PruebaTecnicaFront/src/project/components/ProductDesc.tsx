import { Typography } from "@mui/material";

interface Props {
  desc: string;
}

export const ProductDesc = ({ desc }: Props) => {
  return <Typography>{desc}</Typography>;
};
