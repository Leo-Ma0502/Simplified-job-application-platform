import React from "react";
import { useAuth } from "../../utils/AuthContext";
import { useNavigate } from "react-router-dom";

const Logout = () => {
  const { logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  const logoutButtonStyle = {
    backgroundColor: "#4CAF50",
    color: "white",
    padding: "10px 20px",
    margin: "10px",
    border: "none",
    borderRadius: "5px",
    cursor: "pointer",
    transition: "all 0.3s ease-out",
  };

  return (
    <button
      onClick={handleLogout}
      style={logoutButtonStyle}
      onMouseOver={({ target }) => (target.style.backgroundColor = "#45a049")}
      onMouseOut={({ target }) => (target.style.backgroundColor = "#4CAF50")}
    >
      Logout
    </button>
  );
};

export default Logout;
