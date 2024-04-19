import React from "react";
import withAuth from "../../HOC/withAuth";

const Apply = ({ onClick }) => {
  return (
    <button
      style={{
        backgroundColor: "#4CAF50",
        color: "white",
        padding: "10px 20px",
        border: "none",
        borderRadius: "5px",
        cursor: "pointer",
        transition: "all 0.3s ease-out",
      }}
      onClick={onClick}
    >
      Apply
    </button>
  );
};

const ApplyWithAuth = withAuth(Apply);

export default ApplyWithAuth;
