import { Button } from "@/components/ui/button"
import { Card, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { useCart } from "@/pages/CartContext"
import { Link } from "react-router-dom"

export default function Cart() {
    // Access cart items and functions from CartContext
    const { cartItems, updateQuantity, removeFromCart } = useCart()

    // Calculate the total price
    const total = cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0)

    return (
        <div className="min-h-screen px-6 py-8 bg-background text-foreground">
            <h1 className="text-2xl font-bold mb-6">Your Shopping Cart</h1>

            {cartItems.length === 0 ? (
                // Display message when the cart is empty
                <div className="flex flex-col items-center justify-center space-y-4 py-20">
                    <h2 className="text-2xl font-semibold">Your cart is empty</h2>
                    <p className="text-muted-foreground">Looks like you haven't added anything yet.</p>
                    <Link to="/">
                        <Button>Back to Home</Button>
                    </Link>
                </div>
            ) : (
                // Display cart items and total price
                <div className="grid gap-4">
                    {cartItems.map((item) => (
                        <Card key={item.id}>
                            <CardHeader className="pb-2 flex items-center gap-4">
                                <img
                                    src={item.image}
                                    alt={item.name}
                                    className="w-16 h-16 object-cover rounded"
                                />
                                <div>
                                    <CardTitle className="text-lg">{item.name}</CardTitle>
                                    <CardDescription>
                                        ${item.price.toFixed(2)} x{' '}
                                        <input
                                            type="number"
                                            value={item.quantity}
                                            min={1}
                                            onChange={(e) =>
                                                updateQuantity(item.id, parseInt(e.target.value, 10))
                                            }
                                            className="w-12 border border-gray-300 rounded text-center"
                                        />
                                    </CardDescription>
                                </div>
                            </CardHeader>
                            <CardFooter>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    onClick={() => removeFromCart(item.id)}
                                >
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
                            <Link to="/checkout">
                                <Button>Checkout</Button>
                            </Link>
                        </CardFooter>
                    </Card>
                </div>
            )}
        </div>
    )
}
