import { Link } from "react-router-dom"
import { Button } from "@/components/ui/button"

export default function Navbar() {
  return (
    <header className="flex items-center justify-between px-6 py-4 shadow-md bg-white">
      <Link to="/" className="text-xl font-bold text-primary">eCommerceStore</Link>
      <div className="flex items-center gap-4">
        <Link to="/login"><Button variant="outline">Sign In</Button></Link>
        <Link to="/register"><Button variant="outline">Sign Up</Button></Link>
        <Link to="/cart"><Button>Cart</Button></Link>
      </div>
    </header>
  )
}