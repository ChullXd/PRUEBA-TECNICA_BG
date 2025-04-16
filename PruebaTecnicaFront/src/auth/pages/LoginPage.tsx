import { Alert, Button, Grid, TextField } from "@mui/material";
import { AuthLayout, LoginInfo } from "..";
import { useForm } from "../../hooks";
import { useAuthStore } from "../../shared";
import { usePostLoginMutation } from "../../services";
import { jwtDecode } from "jwt-decode";
import Swal from "sweetalert2";

let user = "";
let pass = "";

if (process.env.NODE_ENV === "development") {
  user = "admin";
  pass = "A!avc89043";
}

export const LoginPage = () => {
  const { onLogin } = useAuthStore();
  const [fetchLogin, { isLoading }] = usePostLoginMutation();

  const {
    formState: { username, password },
    onChange,
    isFormValid,
    errors,
  } = useForm<LoginInfo>(
    {
      username: user,
      password: pass,
    },
    {
      username: [(value) => value.length > 2, "Ingrese un correo válido"],
      password: [
        (value) => value.length >= 6,
        "El password debe tener más de 6 letras.",
      ],
    }
  );

  const onPressLogin = async () => {
    await fetchLogin({
      username,
      password,
    })
      .unwrap()
      .then(async (jwt) => {
        const decoded: {
          email: string;
          exp: number;
        } = jwtDecode(jwt.token);
        await onLogin({ ...jwt, userName: decoded.email });
      })
      .catch((error) => {
        Swal.fire("Error", error?.data?.detail ?? "Ocurrió un error", "error");
      });
  };
  return (
    <AuthLayout title={"Login"}>
      <form>
        <Grid container>
          <Grid item xs={12} sx={{ mt: 2 }}>
            <TextField
              disabled={false}
              label="Correo"
              type="email"
              placeholder="correo@correo.com"
              fullWidth
              value={username}
              onChange={({ target: { value } }) => onChange("username", value)}
              error={!!errors.username}
              helperText={errors.username}
            />
          </Grid>
          <Grid item xs={12} sx={{ mt: 2 }}>
            <TextField
              disabled={false}
              label="Contraseña"
              type="password"
              placeholder="Contraseña"
              fullWidth
              value={password}
              onChange={({ target: { value } }) => onChange("password", value)}
              error={!!errors.password}
              helperText={errors.password}
            />
          </Grid>
          <Grid container spacing={2} sx={{ mb: 2, mt: 1 }}>
            <Grid item xs={12} display={"none"}>
              <Alert severity="error">hola</Alert>
            </Grid>
            <Grid item xs={12} sm={6}>
              <Button
                disabled={!isFormValid() || isLoading}
                variant="contained"
                fullWidth
                type="submit"
                onClick={onPressLogin}
              >
                Login
              </Button>
            </Grid>
          </Grid>
        </Grid>
      </form>
    </AuthLayout>
  );
};
