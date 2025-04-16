import { Box, useTheme } from "@mui/material";
import { Children } from "../../interfaces";

export const BasePage = ({ children }: Children) => {
  const theme = useTheme();
  return (
    <Box
      sx={{
        backgroundColor: theme.palette.background.default,
        minHeight: "100vh",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      {children}
    </Box>
  );
};
