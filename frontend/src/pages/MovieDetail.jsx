import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../api/axios";
import StarRating from "../components/StarRating";
import StarInput from "../components/StarInput";
import "../App.css";

function MovieDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [movie, setMovie] = useState(null);
  const [score, setScore] = useState(0);
  const [note, setNote] = useState("");
  const [message, setMessage] = useState("");

  const loadMovie = async () => {
    const response = await api.get(`/movies/${id}`);
    setMovie(response.data);
  };

  useEffect(() => {
    loadMovie();
  }, [id]);

  const handleAddReview = async () => {
    if (score === 0) {
      setMessage("Lütfen bir puan seçin.");
      return;
    }
    try {
      await api.post("/reviews", {
        movieId: Number(id),
        score: Number(score),
        note: note,
      });
      setMessage("Puan ve not eklendi!");
      setScore(0);
      setNote("");
      loadMovie();
    } catch (err) {
      setMessage("Hata: Puan 1-10 arasında olmalı.");
    }
  };

  if (!movie) return <p style={{ padding: 32 }}>Yükleniyor...</p>;

  return (
    <div className="detail-page">
      <button className="back-btn" onClick={() => navigate(-1)}>← Geri</button>

      <div className="detail-hero">
        <div className="detail-poster">
          <img
            src={
              movie.posterPath
                ? `https://image.tmdb.org/t/p/w300${movie.posterPath}`
                : "https://via.placeholder.com/300x450/1a1f2e/f5c518?text=Afiş+Yok"
            }
            alt={movie.title}
          />
        </div>

        <div className="detail-info">
          <h1>{movie.title}</h1>
          <p className="detail-overview">{movie.overview}</p>

          <div className="score-row">
            <span className="score-label">TMDB Puanı</span>
            <StarRating value={movie.tmdbVoteAverage} />
            <span className="score-value">{movie.tmdbVoteAverage.toFixed(1)}</span>
          </div>

          <div className="score-row">
            <span className="score-label">Ortalama Kullanıcı Puanı</span>
            <StarRating value={movie.averageScore} />
            <span className="score-value">{movie.averageScore.toFixed(1)}</span>
          </div>

          <div className="score-row">
            <span className="score-label">Senin Puanın</span>
            {movie.userScore ? (
              <>
                <StarRating value={movie.userScore} />
                <span className="score-value">{movie.userScore}</span>
              </>
            ) : (
              <span style={{ color: "var(--text-muted)" }}>Henüz puan vermedin</span>
            )}
          </div>
        </div>
      </div>

      <div className="detail-section">
        <h3>Notlar</h3>
        {movie.notes.length === 0 ? (
          <p style={{ color: "var(--text-muted)" }}>Henüz not yok.</p>
        ) : (
          movie.notes.map((n, index) => (
            <div key={index} className="note-card">{n}</div>
          ))
        )}
      </div>

      <div className="detail-section review-form">
        <h3>Puan ve Not Ekle</h3>
        <StarInput value={score} onChange={setScore} />
        <textarea
          placeholder="Notunuz (opsiyonel)"
          value={note}
          onChange={(e) => setNote(e.target.value)}
        />
        <br />
        <button className="submit-btn" onClick={handleAddReview}>Ekle</button>
        {message && <p className="message">{message}</p>}
      </div>
    </div>
  );
}

export default MovieDetail;