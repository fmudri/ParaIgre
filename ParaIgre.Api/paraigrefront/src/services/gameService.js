import axios from 'axios';
import { authHeader } from './authService';

const API_URL = 'http://localhost:5165';

export const getGames = async () => {
    const response = await axios.get(`${API_URL}/games`, { headers: authHeader() });
    return response.data;
};

export const getTags = async () => {
    const response = await axios.get(`${API_URL}/tags`, { headers: authHeader() });
    return response.data;
};

export const createGame = async (gameData) => {
    const response = await axios.post(`${API_URL}/games`, gameData, { headers: authHeader() });
    return response.data;
};

export const updateGame = async (id, gameData) => {
    const response = await axios.put(`${API_URL}/games/${id}`, gameData, { headers: authHeader() });
    return response.data;
};

export const deleteGame = async (id) => {
    await axios.delete(`${API_URL}/games/${id}`, { headers: authHeader() });
}; 