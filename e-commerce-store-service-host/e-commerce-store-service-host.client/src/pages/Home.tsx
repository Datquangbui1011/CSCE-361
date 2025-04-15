import { AnimatePresence, motion } from "framer-motion"
import { Button } from "@/components/ui/button"
import { Link } from "react-router-dom"
import { useState, useRef } from "react"
import { ChevronLeft, ChevronRight } from "lucide-react"
import homepageImage1 from "@/assets/image/homepage1.webp"
import homepageImage3 from "@/assets/image/Homepage6.jpg"
import homepageImage4 from "@/assets/image/homepage4.jpg"
import homepageImage5 from "@/assets/image/homepage5.jpg"
import homepageImage7 from "@/assets/image/homepage7.jpg"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { sampleProducts } from "@/pages/product" // Import centralized product data

const heroMedia = [
  { type: "image", src: homepageImage1 },
  { type: "image", src: homepageImage3 },
  { type: "image", src: homepageImage4 },
  { type: "image", src: homepageImage5 },
  { type: "image", src: homepageImage7 },
]

export default function Home() {
  const [imageIndex, setImageIndex] = useState(0)
  const srollRef = useRef<HTMLDivElement>(null)

  const handleBackgroundClick = () => {
    setImageIndex((prevIndex) => (prevIndex + 1) % heroMedia.length)
  }

  const scrollLeft = () => {
    if (srollRef.current) {
      srollRef.current.scrollBy({
        left: -300,
        behavior: "smooth",
      })
    }
  }

  const scrollRight = () => {
    if (srollRef.current) {
      srollRef.current.scrollBy({
        left: 300,
        behavior: "smooth",
      })
    }
  }

  const bestSellers = [
    sampleProducts.find((product) => product.name === "iPhone 16"),
    sampleProducts.find((product) => product.name === "Google Pixel 9"),
    sampleProducts.find((product) => product.name === "PlayStation 5 Console"),
    sampleProducts.find((product) => product.name === "MacBook Air M4 Pro"),
    sampleProducts.find((product) => product.name === "Dell G16 Gaming"),
    sampleProducts.find((product) => product.name === "Razer Kraken Headset"),
    sampleProducts.find((product) => product.name === "Samsung Galaxy S25+"),
    sampleProducts.find((product) => product.name === "Asus ROG Zephyrus"),
  ].filter(Boolean) // Remove any undefined values


  return (
    <div className="min-h-screen bg-background text-foreground">
      {/* Navbar */}
      <header className="flex items-center justify-between px-6 py-4 shadow-md">
        <Link to="/" className="text-xl font-bold">eCommerceStore</Link>
        <div className="flex items-center gap-4">
          <Link to="/login"><Button variant="outline">Sign In</Button></Link>
          <Link to="/register"><Button variant="outline">Sign Up</Button></Link>
          <Link to="/cart"><Button>Cart</Button></Link>
        </div>
      </header>

      {/* Background Section */}
      <section
        onClick={handleBackgroundClick}
        className="relative h-[85vh] flex items-center justify-center text-white cursor-pointer overflow-hidden mb-6"
      >
        <AnimatePresence mode="wait">
          <motion.div
            key={heroMedia[imageIndex].src}
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            transition={{ duration: 0.8 }}
            className="absolute inset-0 z-0"
            style={{
              backgroundImage: `url(${heroMedia[imageIndex].src})`,
              backgroundSize: "cover",
              backgroundRepeat: "no-repeat",
              backgroundPosition: "center",
              filter: "brightness(1.2)"
            }}
          />
        </AnimatePresence>
        <div className="absolute inset-0 bg-black/20 z-0" />
        <div className="relative z-10 flex h-full w-full items-end justify-end p-8">
          <Button className="bg-primary text-white hover:bg-primary/90">Shop Now</Button>
        </div>
      </section>

      {/* Dot Indicators */}
      <div className="flex justify-center mb-40">
        {heroMedia.map((_, idx) => (
          <div
            key={idx}
            className={`w-3 h-3 rounded-full mx-1 transition-all duration-300 ${
              idx === imageIndex ? "bg-primary scale-125" : "bg-gray-400"
            }`}
          />
        ))}
      </div>

      {/* Category Section */}
      <motion.div
        className="grid grid-cols-2 gap-6 px-6 py-8 sm:grid-cols-4 mb-30"
        initial={{ opacity: 0, y: 30 }}
        whileInView={{ opacity: 1, y: 0 }}
        viewport={{ once: true }}
        transition={{ duration: 0.6 }}
      >
        {[
          { src: "/src/assets/image/category_phone.png", alt: "Phone", label: "Phone", path: "/category/phone" },
          { src: "/src/assets/image/category_laptop.png", alt: "Laptop", label: "Laptop", path: "/category/laptop" },
          { src: "/src/assets/image/category_accessories.png", alt: "Accessories", label: "Accessories", path: "/category/accessories" },
          { src: "/src/assets/image/category_gaming.png", alt: "Gaming", label: "Gaming", path: "/category/gaming" },
        ].map((item, index) => (
          <Link to={item.path} key={index}>
            <div
              className="flex flex-col items-center justify-center gap-2 p-6 bg-muted/20 border border-border rounded-2xl shadow-sm transition-all duration-300 hover:shadow-lg hover:-translate-y-1 cursor-pointer"
            >
              <img src={item.src} alt={item.alt} className="w-16 h-16 object-contain" />
              <span className="text-base font-semibold text-center">{item.label}</span>
            </div>
          </Link>
        ))}
      </motion.div>

      {/* Video Section */}
      <motion.div
        className="relative w-full h-[85vh] overflow-hidden mb-30"
        initial={{ opacity: 0 }}
        whileInView={{ opacity: 1 }}
        viewport={{ once: true }}
        transition={{ duration: 0.8 }}
      >
        <video
          className="absolute inset-0 w-full h-full object-cover"
          src="/src/assets/image/xlarge.mp4"
          autoPlay
          muted
          loop
          playsInline
        />
        <div className="absolute inset-0 bg-black/20" />
        <div className="relative z-10 flex items-center justify-center h-full">
          <div className="text-white text-center">
            <h2 className="text-2xl font-bold">Discover the Future of Technology</h2>
            <p className="mt-2 text-lg">Explore our latest products and innovations.</p>
            <Button className="mt-4 bg-primary text-white hover:bg-primary/90">Shop Now</Button>
          </div>
        </div>
      </motion.div>

      {/* Best Seller Item */}
      <motion.div
        className="relative px-6 py-8 mb-30"
        initial={{ opacity: 0, y: 30 }}
        whileInView={{ opacity: 1, y: 0 }}
        viewport={{ once: true }}
        transition={{ duration: 0.6 }}
      >
        <h2 className="text-2xl font-bold mb-6">Best Sellers</h2>
        <div className="relative flex items-center">
          <Button
            variant="outline"
            className="absolute left-0 z-10 bg-white rounded-full p-2 shadow-md"
            onClick={scrollLeft}
          >
            <ChevronLeft className="h-6 w-6" />
          </Button>
          <div
          ref={srollRef}
          className="flex overflow-x-auto scroll-smooth gap-6 py-4 px-2 scrollbar-hide"
          style={{ scrollSnapType: "x mandatory" }}
        >
          {bestSellers.map((item, index) => (
            item && (
              <Card
                key={index}
                className="hover:shadow-lg transition-shadow w-[250px] h-[400px] snap-center bg-white flex flex-col"
              >
                <CardContent className="flex flex-col items-center gap-4 p-4 flex-grow">
                  <div className="w-48 h-48 flex-shrink-0">
                    <img
                      src={item.image}
                      alt={item.name}
                      className="w-full h-full object-contain"
                    />
                  </div>
                  <div className="text-center flex flex-col justify-between flex-grow">
                    <div>
                      <h3 className="text-lg font-semibold">{item.name}</h3>
                      <p className="text-sm text-gray-600">{item.category}</p>
                    </div>
                    <div>
                      <p className="text-lg font-bold mt-2">{item.price}</p>
                      <Button className="w-full bg-black text-white hover:bg-gray-800 mt-2">
                        Shop Now
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>
            )
          ))}
        </div>
          <Button
            variant="outline"
            className="absolute right-0 z-10 bg-white rounded-full p-2 shadow-md"
            onClick={scrollRight}
          >
            <ChevronRight className="h-6 w-6" />
          </Button>
        </div>
      </motion.div>

      {/* Footer */}
      <footer className="bg-muted py-6 px-6 text-center">
        <p>© 2025 eCommerceStore. All rights reserved.</p>
      </footer>
    </div>
  )
}