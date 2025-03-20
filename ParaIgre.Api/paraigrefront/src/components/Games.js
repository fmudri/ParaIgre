import React, { useState, useEffect } from 'react';
import { getGames, getTags, createGame, deleteGame } from '../services/gameService';

const Games = () => {
    const [games, setGames] = useState([]);
    const [tags, setTags] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [newGame, setNewGame] = useState({
        name: '',
        description: '',
        tagIds: []
    });

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        try {
            const [gamesData, tagsData] = await Promise.all([
                getGames(),
                getTags()
            ]);
            setGames(gamesData);
            setTags(tagsData);
            setLoading(false);
        } catch (err) {
            setError('Failed to load data');
            setLoading(false);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await createGame(newGame);
            setNewGame({ name: '', description: '', tagIds: [] });
            loadData();
        } catch (err) {
            setError('Failed to create game');
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteGame(id);
            loadData();
        } catch (err) {
            setError('Failed to delete game');
        }
    };

    const handleTagChange = (tagId) => {
        const updatedTagIds = newGame.tagIds.includes(tagId)
            ? newGame.tagIds.filter(id => id !== tagId)
            : [...newGame.tagIds, tagId];
        setNewGame({ ...newGame, tagIds: updatedTagIds });
    };

    if (loading) return <div className="text-center p-4">Loading...</div>;
    if (error) return <div className="text-red-500 text-center p-4">{error}</div>;

    return (
        <div className="container mx-auto p-4">
            <h2 className="text-2xl font-bold mb-4">Games</h2>
            
            {/* Add New Game Form */}
            <form onSubmit={handleSubmit} className="mb-8 bg-white p-4 rounded shadow">
                <h3 className="text-xl font-semibold mb-4">Add New Game</h3>
                <div className="space-y-4">
                    <div>
                        <label className="block text-sm font-medium text-gray-700">Name</label>
                        <input
                            type="text"
                            value={newGame.name}
                            onChange={(e) => setNewGame({ ...newGame, name: e.target.value })}
                            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                            required
                        />
                    </div>
                    <div>
                        <label className="block text-sm font-medium text-gray-700">Description</label>
                        <textarea
                            value={newGame.description}
                            onChange={(e) => setNewGame({ ...newGame, description: e.target.value })}
                            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                            rows="3"
                            required
                        />
                    </div>
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-2">Tags</label>
                        <div className="space-x-2">
                            {tags.map(tag => (
                                <label key={tag.id} className="inline-flex items-center">
                                    <input
                                        type="checkbox"
                                        checked={newGame.tagIds.includes(tag.id)}
                                        onChange={() => handleTagChange(tag.id)}
                                        className="rounded border-gray-300 text-indigo-600 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
                                    />
                                    <span className="ml-2">{tag.name}</span>
                                </label>
                            ))}
                        </div>
                    </div>
                    <button
                        type="submit"
                        className="bg-indigo-600 text-white px-4 py-2 rounded hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
                    >
                        Add Game
                    </button>
                </div>
            </form>

            {/* Games List */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                {games.map(game => (
                    <div key={game.id} className="bg-white p-4 rounded shadow">
                        <h3 className="text-lg font-semibold">{game.name}</h3>
                        <p className="text-gray-600 mt-2">{game.description}</p>
                        <div className="mt-2 space-x-1">
                            {game.tags.map(tag => (
                                <span
                                    key={tag.id}
                                    className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700"
                                >
                                    {tag.name}
                                </span>
                            ))}
                        </div>
                        <button
                            onClick={() => handleDelete(game.id)}
                            className="mt-4 text-red-600 hover:text-red-800"
                        >
                            Delete
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Games; 