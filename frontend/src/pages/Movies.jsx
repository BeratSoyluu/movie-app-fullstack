import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";
import { getRole, logout } from "../api/auth";
import StarRating from "../components/StarRating";
import "../App.css";

function Movies() {
  const [movies, setMovies] = useState([]);
  const [search, setSearch] = useState("");
  const navigate = useNavigate();
  const role = getRole();

  const loadPopular = async () => {
    const response = await api.get("/movies/popular");
    setMovies(response.data.results);
  };

  useEffect(() => {
    loadPopular();
  }, []);

  const handleSearch = async () => {
    if (search.trim() === "") {
      loadPopular();
    } else {
      const response = await api.get(`/movies/search?query=${search}`);
      setMovies(response.data.results);
    }
  };

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <div className="layout">
      {/* Sol menü */}
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

      {/* İçerik */}
      <div className="content">
        <h2>Popüler Filmler</h2>
        <div className="search-bar">
          <input
            type="text"
            placeholder="Film adı ara..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && handleSearch()}
          />
          <button onClick={handleSearch}>Ara</button>
        </div>

        <div className="grid">
          {movies.map((movie, index) => (
            <div
              key={movie.id}
              className="card"
              style={{ animationDelay: `${index * 0.05}s` }}
              onClick={() => navigate(`/movies/${movie.id}`)}
            >
              <div className="card-poster">
                <img
                  src={
                    movie.poster_path
                      ? `https://image.tmdb.org/t/p/w300${movie.poster_path}`
                      : "https://via.placeholder.com/300x450/1a1f2e/f5c518?text=Afiş+Yok"
                  }
                  alt={movie.title}
                />
                <div className="card-overlay">
                  <StarRating value={movie.vote_average} />
                </div>
              </div>
              <div className="card-info">
                <div className="card-title">{movie.title}</div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default Movies;