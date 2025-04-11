import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Link } from "react-router-dom"
import { useState } from "react"

const sampleProducts = [
    { id: 1, name: "Wireless Headphones", price: "$59.99" },
    { id: 2, name: "Bluetooth Speaker", price: "$39.99" },
    { id: 3, name: "Smart Watch", price: "$129.99" },
    { id: 4, name: "Gaming Mouse", price: "$29.99" },
    { id: 5, name: "LED Monitor", price: "$149.99" },
    { id: 6, name: "USB-C Cable", price: "$9.99" },
]

export default function Home() {
    const [category, setCategory] = useState("all")

    return (
        <div className="min-h-screen bg-background text-foreground">
            {/* Navbar */}
            <header className="flex items-center justify-between px-6 py-4 shadow-md">
                <Link to="/" className="text-xl font-bold">
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

            {/* Filters */}
            <div className="flex justify-end px-6 py-4">
                <Select onValueChange={setCategory}>
                    <SelectTrigger className="w-[200px]">
                        <SelectValue placeholder="Filter by category" />
                    </SelectTrigger>
                    <SelectContent>
                        <SelectItem value="all">All</SelectItem>
                        <SelectItem value="electronics">Electronics</SelectItem>
                        <SelectItem value="accessories">Accessories</SelectItem>
                        <SelectItem value="gaming">Gaming</SelectItem>
                    </SelectContent>
                </Select>
            </div>

            {/* Product Grid */}
            <main className="grid grid-cols-1 gap-6 px-6 pb-12 sm:grid-cols-2 lg:grid-cols-3">
                {sampleProducts.map((product) => (
                    <Card key={product.id} className="hover:shadow-lg transition-shadow">
                        <CardHeader>
                            <CardTitle>{product.name}</CardTitle>
                            <CardDescription>{product.price}</CardDescription>
                        </CardHeader>
                        <CardContent>
                            <Button className="w-full">Add to Cart</Button>
                        </CardContent>
                    </Card>
                ))}
            </main>
        </div>
    )
}
