import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Link } from "react-router-dom"

export default function Login() {
    return (
        <div className="flex items-center justify-center min-h-screen px-4 bg-background text-foreground">
            <Card className="w-full max-w-sm shadow-md">
                <CardHeader>
                    <CardTitle>Login</CardTitle>
                    <CardDescription>Enter your email and password to access your account.</CardDescription>
                </CardHeader>
                <CardContent className="space-y-4">
                    <div className="space-y-2">
                        <Label htmlFor="email">Email</Label>
                        <Input id="email" type="email" placeholder="you@example.com" />
                    </div>
                    <div className="space-y-2">
                        <Label htmlFor="password">Password</Label>
                        <Input id="password" type="password" />
                    </div>
                </CardContent>
                <CardFooter className="flex flex-col gap-2">
                    <Button className="w-full">Login</Button>
                    <Link to="/register" className="text-sm text-muted-foreground hover:underline">
                        Don't have an account? Sign up
                    </Link>
                    <Link to="/" className="text-sm text-muted-foreground hover:underline text-center">
                        Back to Home
                    </Link>
                </CardFooter>
            </Card>
        </div>
    )
}
