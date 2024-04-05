import React, { createContext, useContext, useState } from "react";

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [loggedIn, setloggedIn] = useState(localStorage.getItem("loggedIn"));

  const login = () => {
    localStorage.setItem("loggedIn", true);
    setloggedIn(true);
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
