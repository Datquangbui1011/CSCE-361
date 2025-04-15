import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Link, useParams } from "react-router-dom"
import { motion } from "framer-motion"
import { sampleProducts } from "@/pages/product" // Import centralized product data



export default function CategoryPage() {
  const { category } = useParams()
  const filteredProducts = sampleProducts.filter(
    (product) => product.category.toLowerCase() === (category ?? "").toLowerCase()
  )

  return (
    <div className="min-h-screen bg-background text-foreground">
      {/* Navbar */}
      <header className="flex items-center justify-between px-6 py-4 shadow-md">
        <Link to="/" className="text-xl font-bold">eCommerceStore</Link>
        <div className="flex items-center gap-4">
          <Link to="/login"><Button variant="outline">Sign In</Button></Link>
          <Link to="/register"><Button variant="outline">Sign Up</Button></Link>
          <Link to="/cart"><Button>Cart</Button></Link>
        </div>
      </header>

      {/* Category Products */}
      <motion.main
        className="grid grid-cols-1 gap-6 px-6 py  sm:grid-cols-2 lg:grid-cols-3 py-12"
        initial={{ opacity: 0, y: 30 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.7 }}
      >
        {filteredProducts.length > 0 ? (
          filteredProducts.map((product) => (
            <Card key={product.id} className="hover:shadow-lg transition-shadow">
              <CardHeader>
                <CardTitle>{product.name}</CardTitle>
                <CardDescription>{product.price}</CardDescription>
              </CardHeader>
              <CardContent className="flex flex-col items-center gap-4">
                <img src={product.image} alt={product.name} className="w-32 h-32 object-contain" />
                <Button className="w-full">Add to Cart</Button>
              </CardContent>
            </Card>
          ))
        ) : (
          <p className="text-center col-span-full">No products found in this category.</p>
        )}
      </motion.main>
    </div>
  )
}