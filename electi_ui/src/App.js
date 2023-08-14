// Import the necessary components and styles
import './App.css';
import RegisterPage from './Components/registerPage'
import LoginPage from './Components/loginPage'
import BookPage from './Components/books'

import {
  BrowserRouter,
  Routes, 
  Route,
} from "react-router-dom";

// Define the main App component
function App() {
  return (
    // Render the application's routing structure
    <BrowserRouter>
    {/* Define the routes using the 'Routes' component */}
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage/>} />
      <Route path="/books" element={<BookPage/>} />

    </Routes>
  </BrowserRouter>
  );
}

export default App;
