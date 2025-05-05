export async function addToCart({
    userId,
    ProductId,
    cartId,
    quantity,
                                }: {
    userId: string;
    ProductId: string;
    cartId: string;
    quantity: number;
}) {
    const res = await fetch("http://localhost:5004/api/CartItem/add", {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({userId, ProductId, cartId, quantity}),
    })
    
    if (!res.ok){
        const error = await res.text()
        throw new Error(`Add to cart failed: ${error}`)
    }
    
    return await res.json()
}

