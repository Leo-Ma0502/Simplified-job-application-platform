import { Outlet, Navigate, createRoutesFromChildren } from "react-router-dom";
import { useAuth } from "../../utils/AuthContext";

function PrivateRoutes() {
  const { loggedIn } = useAuth();
  console.log("....", loggedIn);
  return loggedIn ? <Outlet /> : <Navigate to="/login" />;
}

export default PrivateRoutes;
