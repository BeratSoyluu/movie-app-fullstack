function StarRating({ value, max = 10 }) {
  // value: puan (örn 7.8), max: kaç yıldız (10)
  const percentage = (value / max) * 100;

  return (
    <div style={{ display: "inline-block", position: "relative", fontSize: "16px", lineHeight: 1 }}>
      {/* Boş yıldızlar (arka plan) */}
      <div style={{ color: "#444" }}>
        {"★".repeat(max)}
      </div>
      {/* Dolu yıldızlar (altın, üstte, yüzdeye göre kırpılmış) */}
      <div
        style={{
          color: "var(--gold)",
          position: "absolute",
          top: 0,
          left: 0,
          width: `${percentage}%`,
          overflow: "hidden",
          whiteSpace: "nowrap",
        }}
      >
        {"★".repeat(max)}
      </div>
    </div>
  );
}

export default StarRating;