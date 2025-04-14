import {
  Box,
  Button,
  Container,
  TextField,
  Typography,
  Paper
} from '@mui/material';
import { useFormik } from 'formik';
import * as Yup from 'yup';

const Login = () => {
  const formik = useFormik({
    initialValues: {
      email: '',
      password: ''
    },
    validationSchema: Yup.object({
      email: Yup.string().email('Correo inválido').required('Requerido'),
      password: Yup.string().min(6, 'Mínimo 6 caracteres').required('Requerido')
    }),
    onSubmit: (values) => {
      // Aquí iría tu lógica de login (ej. axios.post(...) )
      console.log('Login con:', values);
    }
  });

  return (
    <Container maxWidth="sm">
      <Paper elevation={3} sx={{ p: 4, mt: 8 }}>
        <Typography variant="h5" component="h1" gutterBottom>
          Iniciar Sesión
        </Typography>
        <form onSubmit={formik.handleSubmit}>
          <TextField
            fullWidth
            id="email"
            name="email"
            label="Correo electrónico"
            margin="normal"
            value={formik.values.email}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.email && Boolean(formik.errors.email)}
            helperText={formik.touched.email && formik.errors.email}
          />
          <TextField
            fullWidth
            id="password"
            name="password"
            label="Contraseña"
            type="password"
            margin="normal"
            value={formik.values.password}
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            error={formik.touched.password && Boolean(formik.errors.password)}
            helperText={formik.touched.password && formik.errors.password}
          />
          <Box mt={2}>
            <Button color="primary" variant="contained" fullWidth type="submit">
              Entrar
            </Button>
          </Box>
        </form>
      </Paper>
    </Container>
  );
};

export default Login;
