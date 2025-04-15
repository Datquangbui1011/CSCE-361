import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import Home from "./pages/Home"
import Login from "./pages/Login"
import Register from "./pages/Register"
import Cart from "./pages/Cart"
import Checkout from "./pages/Checkout"
import OrderConfirmation from "./pages/OrderConfirmation"
import { ThemeProvider } from "./components/theme-provider"
import CategoryPage from "./pages/CategoryPage"



function App() {
    return (
        <ThemeProvider defaultTheme="light" storageKey="vite-ui-theme">
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/cart" element={<Cart />} />
                <Route path="/checkout" element={<Checkout />} />
                <Route path="/confirmation" element={<OrderConfirmation />} />
                <Route path="/category/:category" element={<CategoryPage />} />
            </Routes>
        </Router>
        </ThemeProvider>
    )
}

export default App