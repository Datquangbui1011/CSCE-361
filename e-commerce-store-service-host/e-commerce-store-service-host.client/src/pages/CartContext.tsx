import React, { createContext, useContext, useState } from "react"

// Define the structure of a cart item
export interface CartItem {
  id: number
  name: string
  price: number
  image: string
  quantity: number
}
// Define the context type
interface CartContextType {
  cartItems: CartItem[]
  addToCart: (item: CartItem) => void
  updateQuantity: (id: number, quantity: number) => void
  removeFromCart: (id: number) => void
}
// Create the context
const CartContext = createContext<CartContextType | undefined>(undefined)
// Create a provider component
export const CartProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    // State to hold cart items
    const [cartItems, setCartItems] = useState<CartItem[]>([])
    // Function to add an item to the cart
    const addToCart = (item: CartItem) => {
        setCartItems((prev) => {
          const existingItem = prev.find((cartItem) => cartItem.id === item.id)
          if (existingItem) {
            return prev.map((cartItem) =>
              cartItem.id === item.id
                ? { ...cartItem, quantity: cartItem.quantity + item.quantity }
                : cartItem
            )
          }
          return [...prev, item]
        })
      }
    // Function to update the quantity of an item in the cart
  const updateQuantity = (id: number, quantity: number) => {
    setCartItems((prev) =>
      prev.map((cartItem) =>
        cartItem.id === id ? { ...cartItem, quantity } : cartItem
      )
    )
  }
    // Function to remove an item from the cart
  const removeFromCart = (id: number) => {
    setCartItems((prev) => prev.filter((cartItem) => cartItem.id !== id))
  }

  

  return (
    <CartContext.Provider value={{ cartItems, addToCart, updateQuantity, removeFromCart }}>
      {children}
    </CartContext.Provider>
  )
}
    
export const useCart = () => {
  const context = useContext(CartContext)
  if (!context) {
    throw new Error("useCart must be used within a CartProvider")
  }
  return context
}