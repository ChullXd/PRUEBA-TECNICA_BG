import { MenuOutlined, LogoutOutlined } from "@mui/icons-material";
import {
  AppBar,
  Toolbar,
  IconButton,
  Grid,
  Typography,
  Avatar,
  Box,
} from "@mui/material";
import { useAuthStore } from "../../shared";

export const NavBar = () => {
  const {
    jwtInfo: { userName },
    onLogOut,
  } = useAuthStore();

  const onPressLogOut = () => onLogOut();

  return (
    <AppBar position="fixed">
      <Toolbar>
        <IconButton
          color="inherit"
          edge="start"
          sx={{ mr: 2, display: { sm: "none" } }}
        >
          <MenuOutlined />
        </IconButton>
        <Grid
          container
          direction={"row"}
          justifyContent={"space-between"}
          alignItems={"center"}
        >
          <Box sx={{ display: "flex", alignItems: "center" }}>
            <Box
              sx={{
                display: "flex",
                alignItems: "center",
                bgcolor: "background.paper",
                p: 1,
                borderRadius: 1,
                color: "primary.main",
                mr: 2,
              }}
            >
              <Avatar sx={{ bgcolor: "primary.main", color: "white", mr: 1 }}>
                {userName.charAt(0)}
              </Avatar>
              <Typography variant="h6" noWrap component={"div"}>
                {`${userName}`}
              </Typography>
            </Box>
          </Box>
          <IconButton color="error" onClick={onPressLogOut}>
            <LogoutOutlined />
          </IconButton>
        </Grid>
      </Toolbar>
    </AppBar>
  );
};
