import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import FormInput from "components/text-control";
import { useForm, SubmitHandler, FormProvider } from "react-hook-form";
import { object, string, TypeOf } from "zod";
import { useNavigate, useLocation } from "react-router-dom";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQuery } from "react-query";
import { useUser } from "api/providers/user-provider";
import React, { useEffect } from "react";
import { requestIndentity, requestLogin } from "api";

const loginSchema = object({
  login: string().min(1, "Login is required"),
  password: string(),
});

export type LoginInput = TypeOf<typeof loginSchema>;

const Login = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const from = ((location.state as any)?.from.pathname as string) || "/";

  const methods = useForm<LoginInput>({
    resolver: zodResolver(loginSchema),
  });

  const userContext = useUser();

  const query = useQuery(["authUser"], requestIndentity, {
    enabled: false,
    retry: 1,
    onSuccess: (data) => {
      userContext?.dispatch({ type: "SET_USER", payload: data });
    },
  });

  useEffect(() => {
    if (userContext?.state.authUser) {
      navigate("/order");
    }
  }, []);

  const { mutate: loginUser } = useMutation((userData: LoginInput) => requestLogin(userData), {
    onSuccess: () => {
      query.refetch();
      navigate(from);
    },
    onError: (error: any) => {
      if (Array.isArray((error as any).response.data.error)) {
        (error as any).response.data.error.forEach((el: any) => alert(el.message));
      } else {
        alert((error as any).response.data.message);
      }
    },
  });

  const {
    reset,
    handleSubmit,
    formState: { isSubmitSuccessful },
  } = methods;

  useEffect(() => {
    if (isSubmitSuccessful) {
      reset();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isSubmitSuccessful]);

  const onSubmitHandler: SubmitHandler<LoginInput> = (values) => {
    loginUser(values);
  };

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
          Logowanie
        </Typography>
        <FormProvider {...methods}>
          <Box sx={{ mt: 1 }} component="form" noValidate autoComplete="off" onSubmit={handleSubmit(onSubmitHandler)}>
            <FormInput label="Adres Email" type="email" name="login" focused required fullWidth margin="normal" />
            <FormInput  label="Hasło" type="password" name="password" required focused fullWidth margin="normal" />
            <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }}>
              Zaloguj
            </Button>
            <Grid container>
              <Grid item xs>
                <Link href="#" variant="body2">
                  Zapomniałeś hasła?
                </Link>
              </Grid>
              <Grid item>
                <Link href="/register" variant="body2">
                  {"Nie masz konta? kliknij aby założyć"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </FormProvider>
      </Box>
    </Container>
  );
};

export default Login;
