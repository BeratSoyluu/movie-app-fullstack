import { useState } from "react";

function StarInput({ value, onChange, max = 10 }) {
  const [hover, setHover] = useState(0);

  return (
    <div style={{ display: "inline-flex", gap: "4px", alignItems: "center" }}>
      {[...Array(max)].map((_, i) => {
        const starValue = i + 1;
        const filled = starValue <= (hover || value);
        return (
          <span
            key={starValue}
            onClick={() => onChange(starValue)}
            onMouseEnter={() => setHover(starValue)}
            onMouseLeave={() => setHover(0)}
            style={{
              cursor: "pointer",
              fontSize: "28px",
              color: filled ? "var(--gold)" : "#444",
              transition: "transform 0.15s ease, color 0.15s ease",
              transform: hover === starValue ? "scale(1.3)" : "scale(1)",
            }}
          >
            ★
          </span>
        );
      })}
      {(hover || value) > 0 && (
        <span style={{ marginLeft: "10px", color: "var(--gold)", fontSize: "20px", fontWeight: 600 }}>
          {hover || value}/10
        </span>
      )}
    </div>
  );
}

export default StarInput;