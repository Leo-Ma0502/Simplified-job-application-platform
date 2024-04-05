import React, { createContext, useContext, useState } from "react";
import { LoginUser } from "./Auth";

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [loggedIn, setloggedIn] = useState(localStorage.getItem("loggedIn"));

  const login = async (email, password) => {
    const result = await LoginUser(email, password);
    if (result.success) {
      alert(result.message);
      localStorage.setItem("loggedIn", true);
      setloggedIn(true);
      return true;
    } else {
      alert(result.message);
      return false;
    }
  };

  const logout = () => {
    localStorage.setItem("loggedIn", false);
    setloggedIn(false);
  };

  return (
    <AuthContext.Provider value={{ loggedIn, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
