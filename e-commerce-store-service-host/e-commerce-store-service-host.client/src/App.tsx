import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import Home from "./pages/Home"
import ProductsPage from "./pages/ProductPage" // Ensure the file exists at this path or update the path if necessary
import Login from "./pages/Login"
import Register from "./pages/Register"
import Cart from "./pages/Cart"
import Checkout from "./pages/Checkout"
import OrderConfirmation from "./pages/OrderConfirmation"
import { ThemeProvider } from "./components/theme-provider"
import CategoryPage from "./pages/CategoryPage"
import ProductDetails from "./pages/ProductDetails"
import { CartProvider } from "./pages/CartContext"


function App() {
    return (
        <ThemeProvider defaultTheme="light" storageKey="vite-ui-theme">
            <CartProvider>
                <Router>
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/products" element={<ProductsPage />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/cart" element={<Cart />} />
                        <Route path="/checkout" element={<Checkout />} />
                        <Route path="/confirmation" element={<OrderConfirmation />} />
                        <Route path="/category/:category" element={<CategoryPage />} />
                        <Route path="/product/:productId" element={<ProductDetails />} />
                        <Route path="*" element={<div>404 Not Found</div>} />
                    </Routes>
                </Router>
            </CartProvider>
        </ThemeProvider>
    )
}

export default App