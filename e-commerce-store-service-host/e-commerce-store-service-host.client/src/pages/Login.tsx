import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"

import { useMutation } from "@tanstack/react-query"
import { useState } from "react"
import axios from "axios"
import { useNavigate, Link } from "react-router-dom"
import { useAuth } from "@/context/AuthenContext";



export default function Login() {
    const { setIsLoggedIn } = useAuth();
    const navigate = useNavigate()
    const [formData, setFormData] = useState({
        email: "",
        password: "",
    })

    const mutation = useMutation({
    mutationFn: async (user: { email: string; password: string }) => {
      return await axios.post("http://localhost:5004/api/user/login", user)
    },
    onSuccess: () => {
        setIsLoggedIn(true);
        navigate("/");
    },
    onError: (error) => {
      console.error("Login error:", error)
    },
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.id]: e.target.value })
    }
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault()
        mutation.mutate({
            email: formData.email,
            password: formData.password,
        })
    }

    return (
        <div className="flex items-center justify-center min-h-screen px-4 bg-background text-foreground">
            <Card className="w-full max-w-sm shadow-md">
                <form onSubmit={handleSubmit}>
                    <CardHeader>
                    <CardTitle>Login</CardTitle>
                    <CardDescription>Enter your email and password to access your account.</CardDescription>
                </CardHeader>
                <CardContent className="space-y-4">
                    <div className="space-y-2">
                        <Label htmlFor="email">Email</Label>
                        <Input id="email" type="email" placeholder="you@example.com" value={formData.email} onChange={handleChange} required/>
                    </div>
                    <div className="space-y-2">
                        <Label htmlFor="password">Password</Label>
                        <Input id="password" type="password" value={formData.password} onChange={handleChange} required/>
                    </div>
                    {mutation.isError && (
                        <div className="text-red-500 text-sm">
                            {mutation.error instanceof Error ? mutation.error.message : "An error occurred"}
                        </div>
                    )}
    
                </CardContent>
                <CardFooter className="flex flex-col gap-2">
                    <Button className="w-full" type="submit" disabled={mutation.isPending}>
                        {mutation.isPending ? "Logging in..." : "Login"}
                    </Button>
                    <Link to="/register" className="text-sm text-muted-foreground hover:underline">
                        Don't have an account? Sign up
                    </Link>
                    <Link to="/" className="text-sm text-muted-foreground hover:underline text-center">
                        Back to Home
                    </Link>
                </CardFooter>
                </form>  
            </Card>
        </div>
    )
}
