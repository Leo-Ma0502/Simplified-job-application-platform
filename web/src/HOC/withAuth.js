import React, { useState, useEffect } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useNavigate, useLocation } from "react-router-dom";

const withAuth = (WrappedComponent) => {
  return (props) => {
    const { loggedIn } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();
    const [pendingAction, setPendingAction] = useState(null);

    useEffect(() => {
      console.log("Checking pending action. Logged in:", loggedIn);
      if (loggedIn && pendingAction) {
        pendingAction();
        setPendingAction(null);
      }
    }, [loggedIn, pendingAction]);

    const handleAction = () => {
      console.log("Handle action called. Logged in:", loggedIn);
      if (!loggedIn) {
        alert("Please log in to perform this action.");
        setPendingAction(() => props.onClick);
        navigate("/login", { state: { from: location } });
        return;
      }
      if (props.onClick) {
        props.onClick();
      } else {
        console.error("onClick function is required");
      }
    };

    return <WrappedComponent {...props} onClick={handleAction} />;
  };
};

export default withAuth;
