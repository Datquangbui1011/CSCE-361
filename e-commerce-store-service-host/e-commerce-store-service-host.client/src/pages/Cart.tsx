import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { useState } from "react"
import { Link } from "react-router-dom"

const sampleCartItems = [
    { id: 1, name: "Wireless Headphones", price: 59.99, quantity: 1 },
    { id: 2, name: "Gaming Mouse", price: 29.99, quantity: 2 },
]

export default function Cart() {
    const [cartItems, setCartItems] = useState(sampleCartItems)

    const total = cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0)

    return (
        <div className="min-h-screen px-6 py-8 bg-background text-foreground">
            <h1 className="text-2xl font-bold mb-6">Your Shopping Cart</h1>

            {cartItems.length === 0 ? (
                <div className="flex flex-col items-center justify-center space-y-4 py-20">
                    <h2 className="text-2xl font-semibold">Your cart is empty</h2>
                    <p className="text-muted-foreground">Looks like you haven't added anything yet.</p>
                    <Link to="/">
                        <Button>Back to Home</Button>
                    </Link>
                </div>            ) : (
                <div className="grid gap-4">
                    {cartItems.map((item) => (
                        <Card key={item.id}>
                            <CardHeader className="pb-2">
                                <CardTitle className="text-lg">{item.name}</CardTitle>
                                <CardDescription>${item.price.toFixed(2)} x {item.quantity}</CardDescription>
                            </CardHeader>
                            <CardFooter>
                                <Button variant="outline" size="sm" onClick={() => {
                                    setCartItems(cartItems.filter(i => i.id !== item.id))
                                }}>
                                    Remove
                                </Button>
                            </CardFooter>
                        </Card>
                    ))}
                    <Card>
                        <CardHeader>
                            <CardTitle>Total</CardTitle>
                            <CardDescription>${total.toFixed(2)}</CardDescription>
                        </CardHeader>
                        <CardFooter className="flex justify-between">
                            <Link to="/">
                                <Button variant="outline">Continue Shopping</Button>
                                </Link>
                                <Link to="/Checkout">
                                    <Button>Checkout</Button>
                            </Link>
                        </CardFooter>
                    </Card>
                </div>
            )}
        </div>
    )
}
