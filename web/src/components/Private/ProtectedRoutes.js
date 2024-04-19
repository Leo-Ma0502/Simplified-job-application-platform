import { Outlet, Navigate } from "react-router-dom";
import { useAuth } from "../../contexts/AuthContext";

function PrivateRoutes() {
  const { loggedIn } = useAuth();
  console.log("login status: ", loggedIn);
  return loggedIn === true ? <Outlet /> : <Navigate to="/login" />;
}

export default PrivateRoutes;
