import React, { createContext, useContext, useState } from "react";
import { LoginUser, RegisterUser } from "../utils/Auth";

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [loggedIn, setloggedIn] = useState(
    localStorage.getItem("loggedIn") === "true"
  );

  const register = async (email, password, fname, lname) => {
    const result = await RegisterUser(email, password, fname, lname);
    if (result.success) {
      alert(result.message);
      localStorage.setItem("loggedIn", "true");
      localStorage.setItem("name", result.data.firstName);
      setloggedIn(localStorage.getItem("loggedIn") === "true");
      return true;
    } else {
      alert(result.message);
      localStorage.removeItem("name");
      return false;
    }
  };

  const login = async (email, password) => {
    const result = await LoginUser(email, password);
    if (result.success) {
      alert(result.message);
      localStorage.setItem("loggedIn", "true");
      localStorage.setItem("name", result.name);
      setloggedIn(localStorage.getItem("loggedIn") === "true");
      return true;
    } else {
      alert(result.message);
      localStorage.removeItem("name");
      return false;
    }
  };

  const logout = () => {
    localStorage.setItem("loggedIn", "false");
    localStorage.removeItem("name");
    setloggedIn(false);
  };

  return (
    <AuthContext.Provider value={{ loggedIn, register, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
