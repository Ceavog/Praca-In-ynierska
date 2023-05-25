import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { backendEndpoints } from "api/endpoints/endpoints";
import FormInput from "components/text-control";
import { useForm, SubmitHandler, FormProvider } from "react-hook-form";
import API from "api";
import { useNavigate } from "react-router-dom";
import { useUser } from "api/providers/user-provider";
import { useEffect } from "react";

type FormData = {
  email: string;
  password: string;
};

const Register = () => {
  const navigate = useNavigate();
  const methods = useForm<FormData>();
  const userContext = useUser();

  const onSubmitHandler: SubmitHandler<FormData> = (values: FormData) => {
    const { email, password } = values;

    if (email && password) {
      API.post(backendEndpoints.registerUser.replace(":login", email).replace(":password", password)).then((res) => {
        navigate("/login");
      });
    }
  };

  useEffect(() => {
    if (userContext?.state.authUser) {
      navigate("/order");
    }
  }, []);

  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
         Rejestracja
        </Typography>
        <FormProvider {...methods}>
          <Box sx={{ mt: 1 }} component="form" noValidate autoComplete="off" onSubmit={methods.handleSubmit(onSubmitHandler)}>
            <FormInput label="Adres Email" type="email" name="email" focused required fullWidth margin="normal" />
            <FormInput type="password" label="Hasło" name="password" required focused fullWidth margin="normal" />
            <FormInput type="password" label="Potwierdź Hasło" name="confirmPassword" required focused fullWidth margin="normal" />
            <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }}>
              Zarejestruj
            </Button>
            <Grid container>
              <Grid item xs>
                <Link href="/login" variant="body2">
                  Masz już konto? Zaloguj się
                </Link>
              </Grid>
            </Grid>
          </Box>
        </FormProvider>
      </Box>
    </Container>
  );
};

export default Register;
