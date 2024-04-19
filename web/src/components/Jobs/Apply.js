import React from "react";
import withAuth from "../../HOC/withAuth";

const Apply = ({ onClick }) => {
  return <button onClick={onClick}>Apply</button>;
};

const ApplyWithAuth = withAuth(Apply);

export default ApplyWithAuth;
