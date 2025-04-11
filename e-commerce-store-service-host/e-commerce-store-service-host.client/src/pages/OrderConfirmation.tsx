import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Link } from "react-router-dom"

export default function OrderConfirmation() {
    // Mock order summary - replace with real data
    const orderSummary = {
        orderId: "#123456",
        items: [
            { name: "Wireless Mouse", quantity: 1, price: 29.99 },
            { name: "Mechanical Keyboard", quantity: 1, price: 89.99 },
        ],
        total: 119.98,
    }

    return (
        <div className="min-h-screen bg-background px-4 py-10 text-foreground">
            <div className="max-w-xl mx-auto text-center space-y-6">
                <h1 className="text-4xl font-bold">Thank You for Your Purchase!</h1>
                <p className="text-muted-foreground">Your order has been placed successfully.</p>

                <Card>
                    <CardHeader>
                        <CardTitle>Order Summary</CardTitle>
                        <CardDescription>Order ID: {orderSummary.orderId}</CardDescription>
                    </CardHeader>
                    <CardContent className="space-y-2">
                        {orderSummary.items.map((item, index) => (
                            <div key={index} className="flex justify-between text-sm">
                                <span>{item.name} (x{item.quantity})</span>
                                <span>${(item.quantity * item.price).toFixed(2)}</span>
                            </div>
                        ))}
                        <hr />
                        <div className="flex justify-between font-semibold">
                            <span>Total</span>
                            <span>${orderSummary.total.toFixed(2)}</span>
                        </div>
                    </CardContent>
                </Card>

                <Link to="/">
                    <Button>Back to Home</Button>
                </Link>
            </div>
        </div>
    )
}
