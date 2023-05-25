import { Box, Container, Typography } from "@mui/material";
import { useUser } from "api/providers/user-provider";

const Profile = () => {
  const userContext = useUser();
  const user = userContext?.state.authUser;

  return (
    <Container>
      <Box
        maxWidth="lg"
        sx={{
          maxHeight: "20rem",
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          p: "2rem",
          mx: "auto",
        }}
      >
        <Typography variant="h2" component="h1" sx={{ color: "#1f1e1e", fontWeight: 500 }}>
          Profil Użytkownika
        </Typography>
        <Box sx={{ mt: 2 }}>
          <Typography gutterBottom>
            <strong>Adres Email: </strong> {user?.login}
          </Typography>
        </Box>
      </Box>
    </Container>
  );
};

export default Profile;
