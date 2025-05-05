import { useParams, Link } from "react-router-dom"
import { sampleProducts } from "@/pages/product"
import { Button } from "@/components/ui/button"
import { Star } from "lucide-react"
import { useState } from "react"
import { useCart } from "@/pages/CartContext"
import Navbar from "../components/ui/Navbar" // Adjusted the path to the Navbar component
// import {addToCart} from "@/utils/AddtoCart"

export default function ProductDetails() {
  const { addToCart } = useCart()

  const { productId } = useParams<{ productId: string }>()
  const product = sampleProducts.find((item) => item.id === parseInt(productId || "", 10))

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

  const [quantity, setQuantity] = useState(1)
  const [rating, setRating] = useState(product.rating)

  const handleQuantityChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setQuantity(parseInt(event.target.value, 10))
  }

  const handleAddToCart = () => {
    console.log("Adding product to cart:", product)
    addToCart({
      id: product.id,
      name: product.name,
      price: parseFloat(product.price.replace("$", "")),
      image: product.image,
      quantity,
    })
  }
  
  // const handleAddToCart = async() => {
  //   try{
  //     const userId = "replace-with-real-user-id"
  //     const cartId = "replace-with-real-cart-id"
  //    
  //     await addToCart({
  //       ProductId: productId,
  //       cartId,
  //       quantity,
  //     })
  //    
  //     alert(`Added ${quantity} of ${product.name} to your cart!`)
  //   } catch (err){
  //     console.error(err)
  //     alert("Failed to add item to cart")
  //   }
  // }
  

  return (
    <div className="min-h-screen bg-background text-foreground">
      <Navbar /> {/* Use the Navbar component here */}

      <div className="flex items-center justify-center min-h-[calc(100vh-64px)] px-6 py-8">
        <div className="max-w-6xl grid grid-cols-1 lg:grid-cols-2 gap-50">
          <div className="flex justify-center items-center">
            <img src={product.image} alt={product.name} className="w-full max-w-lg h-auto object-contain rounded-lg shadow-md" />
          </div>
          <div className="space-y-6">
            <h1 className="text-3xl font-bold">{product.name}</h1>
            <p className="text-xl font-semibold">Price: <span className="text-primary">{product.price}</span></p>
            <div>
              <label htmlFor="quantity" className="block text-sm font-medium text-muted-foreground">Quantity:</label>
              <select id="quantity" value={quantity} onChange={handleQuantityChange} className="mt-1 block w-20 p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary">
                {[...Array(10).keys()].map((num) => (
                  <option key={num + 1} value={num + 1}>{num + 1}</option>
                ))}
              </select>
            </div>
            <div>
              <label className="block text-sm font-medium text-muted-foreground">Rating:</label>
              <div className="flex items-center gap-2 mt-1">
                {[...Array(5)].map((_, i) => (
                  <Star key={i} onClick={() => setRating(i + 1)} className={`w-6 h-6 cursor-pointer ${i < rating ? "text-yellow-500 fill-yellow-500" : "text-gray-300"}`} />
                ))}
                <span className="text-sm text-muted-foreground">({rating} / 5)</span>
              </div>
            </div>
            <p className="text-sm text-muted-foreground">
              {product.description.summary}
              <ul>
                {Object.entries(product.description.specifications).map(([key, value]) => (
                  <li key={key}><strong>{key}:</strong> {value}</li>
                ))}
              </ul>
            </p>
            <Button className="w-full bg-primary text-white hover:bg-primary-dark" onClick={handleAddToCart}>
              Add {quantity} to Cart
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}