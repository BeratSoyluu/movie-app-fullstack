import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";
import { getRole, logout } from "../api/auth";
import "../App.css";

function Suggest() {
  const [movieName, setMovieName] = useState("");
  const [message, setMessage] = useState("");
  const [suggestions, setSuggestions] = useState([]);
  const navigate = useNavigate();
  const role = getRole();

  const loadSuggestions = async () => {
    const response = await api.get("/suggestions/mine");
    setSuggestions(response.data);
  };

  useEffect(() => {
    loadSuggestions();
  }, []);

  const handleSuggest = async () => {
    if (movieName.trim() === "") {
      setMessage("Lütfen bir film adı girin.");
      return;
    }
    try {
      await api.post("/suggestions", { movieName: movieName });
      setMessage("Film önerisi gönderildi! 🎬");
      setMovieName("");
      loadSuggestions();
      setTimeout(() => setMessage(""), 3000);
    } catch (err) {
      setMessage("Hata: Film adı boş olamaz.");
    }
  };

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <div className="layout">
      <div className="sidebar">
        <h3>🎬 Menü</h3>
        {(role === "Movie" || role === "Admin") && (
          <>
            <div className="menu-item" onClick={() => navigate("/movies")}>Popüler Film Listele</div>
            <div className="menu-item" onClick={() => navigate("/suggest")}>Film Öner</div>
          </>
        )}
        {(role === "Actor" || role === "Admin") && (
          <div className="menu-item" onClick={() => navigate("/actors")}>Popüler Oyuncu Listele</div>
        )}
        <div className="menu-item logout" onClick={handleLogout}>Çıkış</div>
      </div>

      <div className="content">
        <h2>Film Öner</h2>
        <div className="suggest-box">
          <p className="suggest-text">İzlenmesini önerdiğin bir film var mı? Adını yaz, ekleyelim.</p>
          <div className="search-bar">
            <input
              type="text"
              placeholder="Film adı..."
              value={movieName}
              onChange={(e) => setMovieName(e.target.value)}
              onKeyDown={(e) => e.key === "Enter" && handleSuggest()}
            />
            <button onClick={handleSuggest}>Gönder</button>
          </div>
          {message && <div className="toast">{message}</div>}
        </div>

        {suggestions.length > 0 && (
          <div className="suggest-box" style={{ marginTop: 24 }}>
            <h3 style={{ color: "var(--gold)", marginBottom: 16 }}>Önerdiğin Filmler</h3>
            {suggestions.map((s) => (
              <div key={s.id} className="suggestion-item">
                <span>🎬 {s.movieName}</span>
                <span className="suggestion-date">
                  {new Date(s.createdAt).toLocaleDateString("tr-TR")}
                </span>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default Suggest;