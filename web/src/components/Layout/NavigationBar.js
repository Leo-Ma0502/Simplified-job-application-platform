import { useAuth } from "../../contexts/AuthContext";
import Logout from "../Auth/Logout";
import { useNavigate } from "react-router-dom";

const NavigationBar = () => {
  const { loggedIn } = useAuth();
  const navigate = useNavigate();
  return (
    <div className="navigation-bar">
      {loggedIn ? (
        <div>
          <h1>Welcome, {localStorage.getItem("name")}</h1>
          <Logout />
        </div>
      ) : (
        <div>
          <button
            className="navigation-bar-button"
            onClick={() => navigate("./login")}
          >
            Login
          </button>
          <button
            className="navigation-bar-button"
            onClick={() => navigate("./register")}
          >
            Register
          </button>
        </div>
      )}
      <button
        className="navigation-bar-button"
        onClick={() => navigate("./job")}
      >
        Jobs
      </button>
    </div>
  );
};

export default NavigationBar;
