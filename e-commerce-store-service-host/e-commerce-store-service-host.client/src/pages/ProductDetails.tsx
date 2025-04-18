import { useParams, Link } from "react-router-dom"
import { sampleProducts } from "@/pages/product"
import { Button } from "@/components/ui/button"
import { Star } from "lucide-react"
import { useState } from "react"

export default function ProductDetails() {
  // Get the productId from the URL
  const { productId } = useParams<{ productId: string }>()

  // Find the product in the sampleProducts array
  const product = sampleProducts.find((item) => item.id === parseInt(productId || "", 10))

  // If the product is not found, display an error message
  if (!product) {
    return (
      <div className="min-h-screen flex flex-col items-center justify-center bg-background text-foreground">
        <h1 className="text-2xl font-bold">Product Not Found</h1>
        <p className="text-muted-foreground mt-2">The product you are looking for does not exist.</p>
        <Link to="/">
          <Button className="mt-4">Back to Home</Button>
        </Link>
      </div>
    )
  }

  // State for quantity and rating
  const [quantity, setQuantity] = useState(1)
  const [rating, setRating] = useState(product.rating)

  // Handle quantity change
  const handleQuantityChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setQuantity(parseInt(event.target.value, 10))
  }

  // Handle rating change
  const handleRatingChange = (newRating: number) => {
    setRating(newRating)
  }

  // Render the product details
  return (
    <div className="min-h-screen bg-background text-foreground">
      {/* Navbar */}
      <header className="flex items-center justify-between px-6 py-4 shadow-md bg-white">
        <Link to="/" className="text-xl font-bold text-primary">
          eCommerceStore
        </Link>
        <div className="flex items-center gap-4">
          <Link to="/login">
            <Button variant="outline">Sign In</Button>
          </Link>
          <Link to="/register">
            <Button variant="outline">Sign Up</Button>
          </Link>
          <Link to="/cart">
            <Button>Cart</Button>
          </Link>
        </div>
      </header>

      {/* Main Content */}
      <div className="flex items-center justify-center min-h-[calc(100vh-64px)] px-6 py-8">
        <div className="max-w-6xl grid grid-cols-1 lg:grid-cols-2 gap-50">
          {/* Left Side: Product Image */}
          <div className="flex justify-center items-center">
            <img
              src={product.image}
              alt={product.name}
              className="w-full max-w-lg h-auto object-contain rounded-lg shadow-md"
            />
          </div>

          {/* Right Side: Product Details */}
          <div className="space-y-6">
            {/* Product Name */}
            <h1 className="text-3xl font-bold">{product.name}</h1>

            {/* Price */}
            <p className="text-xl font-semibold">
              Price: <span className="text-primary">{product.price}</span>
            </p>

            {/* Quantity */}
            <div>
              <label htmlFor="quantity" className="block text-sm font-medium text-muted-foreground">
                Quantity:
              </label>
              <select
                id="quantity"
                value={quantity}
                onChange={handleQuantityChange}
                className="mt-1 block w-20 p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary"
              >
                {[...Array(10).keys()].map((num) => (
                  <option key={num + 1} value={num + 1}>
                    {num + 1}
                  </option>
                ))}
              </select>
            </div>

            {/* Rating */}
            <div>
              <label className="block text-sm font-medium text-muted-foreground">Rating:</label>
              <div className="flex items-center gap-2 mt-1">
                {[...Array(5)].map((_, i) => (
                  <Star
                    key={i}
                    onClick={() => handleRatingChange(i + 1)}
                    className={`w-6 h-6 cursor-pointer ${
                      i < rating ? "text-yellow-500 fill-yellow-500" : "text-gray-300"
                    }`}
                  />
                ))}
                <span className="text-sm text-muted-foreground">({rating} / 5)</span>
              </div>
            </div>

            {/* Description */}
            <p className="text-sm text-muted-foreground">
              {typeof product.description === "string" ? (
                product.description
              ) : product.description && typeof product.description === "object" ? (
                <div>
                  <p>{product.description.summary}</p>
                  <ul>
                    {Object.entries(product.description.specifications).map(([key, value]) => (
                      <li key={key}>
                        <strong>{key}:</strong> {value}
                      </li>
                    ))}
                  </ul>
                </div>
              ) : (
                "No description available for this product."
              )}
            </p>
            
            
            
            {/* Add to Cart Button */}
            <Button className="w-full bg-primary text-white hover:bg-primary-dark">
              Add {quantity} to Cart
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}