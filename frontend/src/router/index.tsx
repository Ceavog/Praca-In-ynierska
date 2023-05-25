import Order from "modules/order";
import SettingsProducts from "modules/settings/products";
import Profile from "modules/settings/profile";
import { Navigate, RouteObject } from "react-router-dom";
import Layout from "../components/layout";
import Login from "../modules/guest/components/login";
import Register from "../modules/guest/components/register";
import Cookies from 'js-cookie';
import OrderList from "modules/orderList";


interface ProtectedRouteProps {
  children: React.ReactNode;
}


const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  const isLoggedIn = Cookies.get('logged_in');

  if (isLoggedIn) {
    return <>{children}</>;
  } else {
    return <Navigate to="/login" replace />;
  }
};


const authRoutes: RouteObject = {
  path: "*",
  children: [
    {
      path: "login",
      element: <Login />,
    },
    {
      path: "register",
      element: <Register />,
    },
  ],
};

const normalRoutes: RouteObject = {
  path: "*",
  element: (
      <ProtectedRoute>
        <Layout />
      </ProtectedRoute>
  ),
  children: [
    {
      path: "*",
      element: <Navigate to="/login" replace />,
    },
    {
      path: "settings",
      element: <SettingsProducts />,
    },
    {
      path: "order",
      element: <Order />,
    },
    {
      path: "order-list",
      element: <OrderList />,
    },
    {
      path: "profile",
      children: [
        {
          path: "",
          element: <Profile />,
        },
      ],
    },
  ],
};



const routes: RouteObject[] = [authRoutes, normalRoutes];


export default routes;
