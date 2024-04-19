import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { AuthProvider } from "./contexts/AuthContext";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import Header from "./components/Layout/Header";
import Footer from "./components/Layout/Footer";
import JobListing from "./components/Jobs/JobListing";
import Apply from "./components/Jobs/Apply";
import PrivateRoutes from "./components/Private/ProtectedRoutes";
import NavigationBar from "./components/Layout/NavigationBar";
import "./App.css";

function App() {
  return (
    <AuthProvider>
      <Router>
        <div className="app-container">
          <Header />
          <NavigationBar />
          <div className="content">
            <Routes>
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route path="/" element={<JobListing />} />
              <Route path="/job" element={<JobListing />} />
              <Route path="/apply" element={<Apply />} />
              <Route element={<PrivateRoutes />}>
                {/* protected routes here */}
              </Route>
            </Routes>
          </div>
          <Footer />
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;
