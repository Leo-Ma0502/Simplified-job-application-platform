import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "./Register.css";
import { RegisterUser } from "../../utils/Auth";
import { useAuth } from "../../utils/AuthContext";

function Register() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [fname, setFname] = useState("");
  const [lname, setLname] = useState("");
  const navigate = useNavigate();
  const { register } = useAuth();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const registerSuccess = await register(email, password, fname, lname);
    if (registerSuccess) {
      navigate("/job");
    }
  };

  return (
    <div className="register-container">
      <form className="register-form" onSubmit={handleSubmit}>
        <h2>Register</h2>
        <div className="form-group">
          <input
            type="email"
            placeholder="Email"
            required
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="form-control"
          />
        </div>
        <div className="form-group">
          <input
            type="password"
            placeholder="Password"
            required
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="form-control"
          />
        </div>
        <div className="form-group">
          <input
            placeholder="First name"
            required
            value={fname}
            onChange={(e) => setFname(e.target.value)}
            className="form-control"
          />
        </div>
        <div className="form-group">
          <input
            placeholder="Last name"
            required
            value={lname}
            onChange={(e) => setLname(e.target.value)}
            className="form-control"
          />
        </div>
        <button type="submit" className="register-button">
          Register
        </button>
        <div className="login-link">
          <Link to={"/login"}>Login with an existing account</Link>
        </div>
      </form>
    </div>
  );
}

export default Register;
