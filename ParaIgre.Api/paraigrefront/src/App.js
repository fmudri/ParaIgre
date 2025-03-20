import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { useAuth } from './context/AuthContext';
import Login from './components/Login';
import Register from './components/Register';
import ProtectedRoute from './components/ProtectedRoute';
import Games from './components/Games';
import './App.css';

const Navigation = () => {
    const { user, setUser } = useAuth();

    const handleLogout = () => {
        setUser(null);
        localStorage.removeItem('user');
    };

    return (
        <nav className="bg-gray-800 p-4">
            <div className="container mx-auto flex justify-between items-center">
                <div className="flex space-x-4">
                    <Link to="/" className="text-white font-bold">
                        ParaIgre
                    </Link>
                    {user && (
                        <Link to="/games" className="text-white hover:text-gray-300">
                            Games
                        </Link>
                    )}
                </div>
                <div className="space-x-4">
                    {user ? (
                        <>
                            <span className="text-white">Welcome, {user.username}!</span>
                            <button
                                onClick={handleLogout}
                                className="text-white hover:text-gray-300"
                            >
                                Logout
                            </button>
                        </>
                    ) : (
                        <>
                            <Link to="/login" className="text-white hover:text-gray-300">
                                Login
                            </Link>
                            <Link to="/register" className="text-white hover:text-gray-300">
                                Register
                            </Link>
                        </>
                    )}
                </div>
            </div>
        </nav>
    );
};

const Home = () => {
    return (
        <div className="container mx-auto p-4">
            <h1 className="text-3xl font-bold mb-6">Welcome to ParaIgre</h1>
            <p className="text-lg text-gray-700 mb-4">
                Your ultimate platform for managing and discovering video games.
            </p>
            <div className="bg-white p-6 rounded-lg shadow-md">
                <h2 className="text-2xl font-semibold mb-4">Getting Started</h2>
                <ul className="list-disc list-inside space-y-2 text-gray-600">
                    <li>Browse our collection of games</li>
                    <li>Add new games to the database</li>
                    <li>Tag and categorize games</li>
                    <li>Manage your game collection</li>
                </ul>
                <Link
                    to="/games"
                    className="inline-block mt-6 bg-indigo-600 text-white px-6 py-2 rounded-md hover:bg-indigo-700 transition-colors"
                >
                    Explore Games
                </Link>
            </div>
        </div>
    );
};

function App() {
    return (
        <Router>
            <AuthProvider>
                <div className="min-h-screen bg-gray-100">
                    <Navigation />
                    <Routes>
                        <Route
                            path="/"
                            element={
                                <ProtectedRoute>
                                    <Home />
                                </ProtectedRoute>
                            }
                        />
                        <Route
                            path="/games"
                            element={
                                <ProtectedRoute>
                                    <Games />
                                </ProtectedRoute>
                            }
                        />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                    </Routes>
                </div>
            </AuthProvider>
        </Router>
    );
}

export default App;
