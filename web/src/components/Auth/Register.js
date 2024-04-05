import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Register.css";
import { RegisterUser } from "../../utils/Auth";

function Register() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [fname, setFname] = useState("");
  const [lname, setLname] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await RegisterUser(email, password, fname, lname);
    if (result.success) {
      alert(result.message);
      navigate("/jobs");
    } else {
      alert(result.message);
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
            required={true}
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="form-control"
          />
        </div>
        <div className="form-group">
          <input
            type="password"
            placeholder="Password"
            required={true}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="form-control"
          />
        </div>
        <div className="form-group">
          <input
            placeholder="First name"
            value={fname}
            onChange={(e) => setFname(e.target.value)}
            className="form-control"
          />
        </div>
        <div className="form-group">
          <input
            placeholder="Last name"
            value={lname}
            onChange={(e) => setLname(e.target.value)}
            className="form-control"
          />
        </div>
        <button type="submit" className="register-button">
          Register
        </button>
      </form>
    </div>
  );
}

export default Register;
