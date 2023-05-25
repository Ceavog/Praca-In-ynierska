import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Container from "@mui/material/Container";
import Button from "@mui/material/Button";
import { Link } from "react-router-dom";
import {useCookies} from "react-cookie";

function Navi() {
  const [cookies, setCookie, removeCookie] = useCookies();
  function handleLogout() {
    removeCookie("logged_in");
    window.location.replace("/login");

  }
  return (
    <AppBar position="static">
      <Container>
        <Toolbar disableGutters>
          <Box sx={{ flexGrow: 1, display: "flex", justifyContent: "space-between" }}>
            <Box>
              <Button sx={{ my: 2, color: "white" }} component={Link} to="/order">
                Zamów
              </Button>
              <Button sx={{ my: 2, color: "white" }} component={Link} to="/settings">
                Menu
              </Button>
              <Button sx={{ my: 2, color: "white" }} component={Link} to="/order-list">
                Lista Zamówień
              </Button>
            </Box>
            <Box>
              <Button sx={{ my: 2, color: "white" }} component={Link} to="/profile">
                Profil
              </Button>
              <Button onClick={() => {handleLogout()}} sx={{ my: 2, color: "white" }}>
                Wyloguj
              </Button>
            </Box>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default Navi;
