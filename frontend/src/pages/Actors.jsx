import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";
import { getRole, logout } from "../api/auth";
import "../App.css";

function Actors() {
  const [actors, setActors] = useState([]);
  const [search, setSearch] = useState("");
  const navigate = useNavigate();
  const role = getRole();

  const loadPopular = async () => {
    const response = await api.get("/actors/popular");
    setActors(response.data.results);
  };

  useEffect(() => {
    loadPopular();
  }, []);

  const handleSearch = async () => {
    if (search.trim() === "") {
      loadPopular();
    } else {
      const response = await api.get(`/actors/search?query=${search}`);
      setActors(response.data.results);
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
        <h2>Popüler Oyuncular</h2>
        <div className="search-bar">
          <input
            type="text"
            placeholder="Oyuncu adı ara..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && handleSearch()}
          />
          <button onClick={handleSearch}>Ara</button>
        </div>

        <div className="grid">
          {actors.map((actor, index) => (
            <div
              key={actor.id}
              className="card"
              style={{ animationDelay: `${index * 0.05}s`, cursor: "default" }}
            >
              <div className="card-poster">
                <img
                  src={
                    actor.profile_path
                      ? `https://image.tmdb.org/t/p/w300${actor.profile_path}`
                      : "https://via.placeholder.com/300x450/1a1f2e/f5c518?text=Fotoğraf+Yok"
                  }
                  alt={actor.name}
                />
              </div>
              <div className="card-info">
                <div className="card-title">{actor.name}</div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default Actors;