import { useState, ChangeEvent, MouseEvent } from 'react'
import { useNavigate } from 'react-router-dom'
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { useMutation } from '@tanstack/react-query'
import axios from 'axios'

interface AddToCartProps {
    cartId: string
    productId: string
}

interface CartItemDto {
    CartId: string
    ProductId: string
    Quantity: number
}

export default function AddToCart({ cartId, productId }: AddToCartProps) {
    const [quantity, setQuantity] = useState<number>(1)
    const navigate = useNavigate()

    const mutation = useMutation<unknown, Error, CartItemDto>({
        mutationFn: async (payload) => {
            const { data } = await axios.post(
                'http://localhost:5004/api/CartItem', // ← singular “CartItem”
                payload
            )
            return data
        },
        onSuccess: () => {
            navigate('/cart')
        },
        onError: (err) => {
            console.error('Add to cart error:', err)
        },
    })

    const { mutate, status, error } = mutation
    const loading = status === 'pending'
    const errorState = status === 'error'

    const handleAddToCart = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        mutate({
            CartId: cartId,
            ProductId: productId,
            Quantity: quantity,
        })
    }

    const handleQuantityChange = (e: ChangeEvent<HTMLInputElement>) => {
        const val = Number(e.target.value)
        setQuantity(!isNaN(val) && val > 0 ? val : 1)
    }

    return (
        <div className="flex items-center space-x-2">
            <Input
                type="number"
                min={1}
                value={quantity}
                onChange={handleQuantityChange}
                className="w-16"
                disabled={loading}
            />
            <Button onClick={handleAddToCart} disabled={loading}>
                {loading ? 'Adding…' : 'Add to Cart'}
            </Button>
            {errorState && (
                <p className="text-red-500 text-sm mt-1">
                    {error?.message ?? 'Failed to add to cart'}
                </p>
            )}
        </div>
    )
}
