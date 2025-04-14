import { AnimatePresence, motion } from "framer-motion"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Link } from "react-router-dom"
import { useState } from "react"
import homepageImage1 from "@/assets/image/homepage1.webp"
import homepageImage3 from "@/assets/image/Homepage6.jpg"
import homepageImage4 from "@/assets/image/homepage4.jpg"
import homepageImage5 from "@/assets/image/homepage5.jpg"
import homepageImage7 from "@/assets/image/homepage7.jpg"

// 📸 Product Images
import iphoneImg from "@/assets/image/iphone16.png"
import samsungImg from "@/assets/image/samsung-galaxy S25+.webp"
import pixelImg from "@/assets/image/Pixel 9 Pro XL.webp"
import oneplusImg from "@/assets/image/phone-oneplus13R.webp"
import samsungImg1 from "@/assets/image/phone-samsungfold.webp"
import macbookImg from "@/assets/image/macbookM4.jpeg"
import dellImg from "@/assets/image/Dell - G16 Gaming.avif"
import hpImg from "@/assets/image/Hp Spectre x360 .png"
import lenovoImg from "@/assets/image/Laptop ThinkPad X1.png"
import asusImg from "@/assets/image/asus-rog zephyrus.webp"
import caseImg from "@/assets/image/Access-iphone14case.png"
import caseImg1 from "@/assets/image/Access-iphone16casepro.png"
import caseImg2 from "@/assets/image/Access-iphone16case.png"
import cableImg from "@/assets/image/Apple_Watch_Magnetic_Fast_Charger1.png"
import cableImg1 from "@/assets/image/Lightning_to_USB_Cable_2m.png"
import cableImg2 from "@/assets/image/USB-C_to_Lightning_Cable_2m.png"
import mouseImg from "@/assets/image/mouse1-lift.png"
import mouseImg1 from "@/assets/image/mouse-m720-triathlon.png"
import mouseImg2 from "@/assets/image/mouse-mx-ergo.png"
import powerbankImg from "@/assets/image/powerbank.png"
import ps5Img from "@/assets/image/ps5.png"
import xboxImg from "@/assets/image/xbox-controller.png"
import headsetImg from "@/assets/image/razer-headset.png"
import vrImg from "@/assets/image/metaquest.png"

const heroMedia = [
  { type: "image", src: homepageImage1 },
  { type: "image", src: homepageImage3 },
  { type: "image", src: homepageImage4 },
  { type: "image", src: homepageImage5 },
  { type: "image", src: homepageImage7 },
]

const sampleProducts = [
  { id: 1, category: "Phone", name: "iPhone 16", price: "$999.99", image: iphoneImg },
  { id: 2, category: "Phone", name: "Samsung Galaxy S25+", price: "$899.99", image: samsungImg },
  { id: 3, category: "Phone", name: "Google Pixel 8", price: "$799.99", image: pixelImg },
  { id: 4, category: "Phone", name: "OnePlus 13R 256GB", price: "$729.99", image: oneplusImg },
  { id: 5, category: "Phone", name: "Samsung Galaxy ZFold6", price: "$1,899.99", image: samsungImg1},
  { id: 6, category: "Laptop", name: "MacBook Air M4 Pro", price: "$1599.99", image: macbookImg },
  { id: 7, category: "Laptop", name: "Dell G16 Gaming", price: "$1099.99", image: dellImg },
  { id: 8, category: "Laptop", name: "HP Spectre x360", price: "$1249.99", image: hpImg },
  { id: 9, category: "Laptop", name: "Lenovo ThinkPad X1 Carbon", price: "$1299.99", image: lenovoImg },
  { id: 10, category: "Laptop", name: "Asus ROG Zephyrus", price: "$1399.99", image: asusImg },
  { id: 11, category: "Accessories", name: "iPhone 14 Pro Case", price: "$19.99", image: caseImg },
  { id: 12, category: "Accessories", name: "iPhone 16 Pro Clear Case with MagSafe", price: "$49.99", image: caseImg1 },
  { id: 13, category: "Accessories", name: "iPhone 16 Clear Case with MagSafe", price: "$39.99", image: caseImg2 },
  { id: 14, category: "Accessories", name: "Apple Watch Magnetic Fast Charger", price: "$9.99", image: cableImg },
  { id: 15, category: "Accessories", name: "Lightning to USB-C Cable (2m)", price: "$19.99", image: cableImg1 },
  { id: 16, category: "Accessories", name: "USB-C to Lightning Cable (2m)", price: "$19.99", image: cableImg2 },
  { id: 17, category: "Accessories", name: "LIFT", price: "$69.99", image: mouseImg },
  { id: 18, category: "Accessories", name: "MX Anywhere 3", price: "$79.99", image: mouseImg1 },
  { id: 19, category: "Accessories", name: "MX Master 3S", price: "$99.99", image: mouseImg2 },
  { id: 20, category: "Accessories", name: "Anker Power Bank 20,000mAh", price: "$39.99", image: powerbankImg },
  { id: 16, category: "Gaming", name: "PlayStation 5 Console", price: "$499.99", image: ps5Img },
  { id: 17, category: "Gaming", name: "Xbox Wireless Controller", price: "$59.99", image: xboxImg },
  { id: 18, category: "Gaming", name: "Razer Kraken Headset", price: "$79.99", image: headsetImg },
  { id: 20, category: "Gaming", name: "Meta Quest 3 VR Headset", price: "$499.99", image: vrImg },
]

export default function Home() {
  const [imageIndex, setImageIndex] = useState(0)
  const handleBackgroundClick = () => {
    setImageIndex((prevIndex) => (prevIndex + 1) % heroMedia.length)
  }

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
          { src: "/src/assets/image/category_phone.png", alt: "Phone", label: "Phone" },
          { src: "/src/assets/image/category_laptop.png", alt: "Laptop", label: "Laptop" },
          { src: "/src/assets/image/category_accessories.png", alt: "Accessories", label: "Accessories" },
          { src: "/src/assets/image/category_gaming.png", alt: "Gaming", label: "Gaming" },
        ].map((item, index) => (
          <div
            key={index}
            className="flex flex-col items-center justify-center gap-2 p-6 bg-muted/20 border border-border rounded-2xl shadow-sm transition-all duration-300 hover:shadow-lg hover:-translate-y-1 cursor-pointer"
          >
            <img src={item.src} alt={item.alt} className="w-16 h-16 object-contain" />
            <span className="text-base font-semibold text-center">{item.label}</span>
          </div>
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

      {/* Product Grid with Images */}
      <motion.main
        className="grid grid-cols-1 gap-6 px-6 pb-12 sm:grid-cols-2 lg:grid-cols-3"
        initial={{ opacity: 0, y: 30 }}
        whileInView={{ opacity: 1, y: 0 }}
        viewport={{ once: true }}
        transition={{ duration: 0.7 }}
      >
        {sampleProducts.map((product) => (
          <Card key={product.id} className="hover:shadow-lg transition-shadow">
            <CardHeader>
              <CardTitle>{product.name}</CardTitle>
              <CardDescription>{product.price}</CardDescription>
            </CardHeader>
            <CardContent className="flex flex-col items-center gap-4">
              <img src={product.image} alt={product.name} className="w-32 h-32 object-contain" />
              <Button className="w-full">Add to Cart</Button>
            </CardContent>
          </Card>
        ))}
      </motion.main>
    </div>
  )
}
