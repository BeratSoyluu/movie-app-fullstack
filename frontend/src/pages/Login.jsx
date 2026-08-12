import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";
import { getRole } from "../api/auth";
import "../App.css";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const redirectByRole = () => {
    const role = getRole();
    if (role === "Actor") {
      navigate("/actors");
    } else {
      navigate("/movies");
    }
  };

  useEffect(() => {
    if (localStorage.getItem("token")) {
      redirectByRole();
    }
  }, []);

  const handleLogin = async () => {
    try {
      const response = await api.post("/auth/login", {
        userName: username,
        password: password,
      });
      localStorage.setItem("token", response.data);
      redirectByRole();
    } catch (err) {
      setError("Kullanıcı adı veya şifre hatalı");
    }
  };

  return (
    <div className="login-page">
      <div className="login-card">
        <div className="login-logo">🎬</div>
        <h1>Film Uygulaması</h1>
        <p className="login-subtitle">Devam etmek için giriş yapın</p>

        <input
          type="text"
          placeholder="Kullanıcı adı"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleLogin()}
        />
        <input
          type="password"
          placeholder="Şifre"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleLogin()}
        />
        <button onClick={handleLogin}>Giriş Yap</button>
        {error && <p className="login-error">{error}</p>}
      </div>
    </div>
  );
}

export default Login;