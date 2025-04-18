import { useState } from "react"
import { Link } from "react-router-dom"
import { sampleProducts } from "@/pages/product"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import Navbar from "@/components/ui/Navbar"

export default function ProductPage() {
  const [selectedCategory, setSelectedCategory] = useState("All") // State for selected category

  // Get unique categories from products
  const categories = ["All", ...new Set(sampleProducts.map((product) => product.category))]

  // Filter products based on selected category
  const filteredProducts =
    selectedCategory === "All"
      ? sampleProducts
      : sampleProducts.filter((product) => product.category === selectedCategory)

  return (
    <div className="min-h-screen bg-background text-foreground">
      {/* Navbar */}
      <Navbar />

      <div className="px-6 py-8">
        {/* Page Title */}
        <h1 className="text-3xl font-bold mb-6">All Products</h1>

        {/* Category Filter */}
        <div className="flex items-center gap-4 mb-6">
          {categories.map((category) => (
            <Button
              key={category}
              variant={selectedCategory === category ? "default" : "outline"}
              onClick={() => setSelectedCategory(category)}
              className={`capitalize ${
                selectedCategory === category ? "bg-primary text-white" : "hover:bg-gray-100"
              }`}
            >
              {category}
            </Button>
          ))}
        </div>

        {/* Products Grid */}
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          {filteredProducts.map((product) => (
            <Card key={product.id} className="hover:shadow-lg transition-shadow">
              <CardHeader>
                <img
                  src={product.image}
                  alt={product.name}
                  className="w-full h-48 object-contain rounded-lg"
                />
              </CardHeader>
              <CardContent>
                <CardTitle className="text-lg font-semibold">{product.name}</CardTitle>
                <CardDescription className="text-sm text-gray-600">{product.category}</CardDescription>
                <p className="text-lg font-bold mt-2">{product.price}</p>
              </CardContent>
              <div className="p-4">
                <Link to={`/product/${product.id}`}>
                  <Button className="w-full bg-primary text-white hover:bg-primary-dark">
                    View Details
                  </Button>
                </Link>
              </div>
            </Card>
          ))}
        </div>

        {/* No Products Found */}
        {filteredProducts.length === 0 && (
          <div className="text-center text-muted-foreground mt-6">
            <p>No products found in this category.</p>
          </div>
        )}
      </div>
    </div>
  )
}