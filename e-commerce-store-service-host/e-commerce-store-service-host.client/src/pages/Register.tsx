import { Button } from "@/components/ui/button"
import {
  Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Link, useNavigate } from "react-router-dom"
import { useState } from "react"
import { useMutation } from "@tanstack/react-query"
import axios from "axios"

export default function Register() {
  const navigate = useNavigate()
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    password: "",
    confirmPassword: "",
  })

  const mutation = useMutation({
    mutationFn: async (newUser: {
      name: string
      email: string
      password: string
      confirmPassword: string
    }) => {
      return await axios.post("http://localhost:5004/api/user/register", newUser)
    },
    onSuccess: () => {
      navigate("/login")
    },
    onError: (error) => {
      console.error("Registration error:", error)
    },
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.id]: e.target.value })
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    if (formData.password !== formData.confirmPassword) {
      alert("Passwords do not match")
      return
    }
    mutation.mutate({
      name: formData.name,
      email: formData.email,
      password: formData.password,
      confirmPassword: formData.confirmPassword,
    })
  }

  return (
    <div className="flex items-center justify-center min-h-screen px-4 bg-background text-foreground">
      <Card className="w-full max-w-sm shadow-md">
        <form onSubmit={handleSubmit}>
          <CardHeader>
            <CardTitle>Create an Account</CardTitle>
            <CardDescription>Sign up to start shopping with us!</CardDescription>
          </CardHeader>
          <CardContent className="space-y-4">
            <div className="space-y-2">
              <Label htmlFor="name">Full Name</Label>
              <Input id="name" type="text" placeholder="John Doe" value={formData.name} onChange={handleChange} required />
            </div>
            <div className="space-y-2">
              <Label htmlFor="email">Email</Label>
              <Input id="email" type="email" placeholder="you@example.com" value={formData.email} onChange={handleChange} required />
            </div>
            <div className="space-y-2">
              <Label htmlFor="password">Password</Label>
              <Input id="password" type="password" value={formData.password} onChange={handleChange} required />
            </div>
            <div className="space-y-2">
              <Label htmlFor="confirmPassword">Confirm Password</Label>
              <Input id="confirmPassword" type="password" value={formData.confirmPassword} onChange={handleChange} required />
            </div>
            {mutation.isError && (
              <div className="text-sm text-red-500">
                {(mutation.error as Error).message}
              </div>
            )}
          </CardContent>
          <CardFooter className="flex flex-col gap-2">
            <Button className="w-full" type="submit" disabled={mutation.isPending}>
              {mutation.isPending ? "Creating Account..." : "Sign Up"}
            </Button>
            <Link to="/login" className="text-sm text-muted-foreground hover:underline text-center">
              Already have an account? Sign in
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
