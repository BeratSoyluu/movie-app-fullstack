import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Movies from "./pages/Movies";
import MovieDetail from "./pages/MovieDetail";
import Actors from "./pages/Actors";
import Suggest from "./pages/Suggest";
import ProtectedRoute from "./components/ProtectedRoute";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/movies" element={<ProtectedRoute><Movies /></ProtectedRoute>} />
        <Route path="/movies/:id" element={<ProtectedRoute><MovieDetail /></ProtectedRoute>} />
        <Route path="/actors" element={<ProtectedRoute><Actors /></ProtectedRoute>} />
        <Route path="/suggest" element={<ProtectedRoute><Suggest /></ProtectedRoute>} />
        <Route path="/" element={<Navigate to="/login" />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;