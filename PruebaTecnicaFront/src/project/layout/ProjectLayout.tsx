import { Box, Toolbar } from "@mui/material";
import { Children } from "../../interfaces";
import { NavBar } from "./NavBar";

export const ProjectLayout = ({ children }: Children) => {
  return (
    <Box
      sx={{ display: "flex" }}
      className="box-shadow animate__animated animate__fadeIn"
    >
      <NavBar />
      <Box component={"main"} sx={{ flexGrow: 1, p: 0.1 }}>
        <Toolbar />
        {children}
      </Box>
    </Box>
  );
};
