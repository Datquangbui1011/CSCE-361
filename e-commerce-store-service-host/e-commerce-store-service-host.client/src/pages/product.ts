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

export const sampleProducts = [
  {
    id: 1,
    category: "Phone",
    name: "iPhone 16",
    price: "$999.99",
    image: iphoneImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "The iPhone 16 features a stunning design and cutting-edge technology.",
      specifications: {
        Brand: "Apple",
        "Operating System": "iOS 17",
        "RAM Memory Installed Size": "8 GB",
        "Memory Storage Capacity": "128 GB",
        "Screen Size": "6.1 Inches",
        "Model Name": "iPhone 16",
        "Wireless Carrier": "T-Mobile",
        "Cellular Technology": "5G",
        Color: "Ultramarine",
        "Connector Type": "USB Type C"
      }
    }
  },
  {
    id: 2,
    category: "Phone",
    name: "Samsung Galaxy S25+",
    price: "$899.99",
    image: samsungImg,
    quantity: 5,
    rating: 4.7,
    description: {
      summary: "Experience the power of the Samsung Galaxy S25+ with its advanced features.",
      specifications: {
        Brand: "Samsung",
        "Operating System": "Android 14",
        "RAM Memory Installed Size": "12 GB",
        "Memory Storage Capacity": "256 GB",
        "Screen Size": "6.7 Inches",
        "Model Name": "Galaxy S25+",
        "Wireless Carrier": "Verizon",
        "Cellular Technology": "5G",
        Color: "Phantom Black",
        "Connector Type": "USB Type C"
      }
    }
  },
  {
    id: 3,
    category: "Phone",
    name: "Google Pixel 9",
    price: "$799.99",
    image: pixelImg,
    quantity: 8,
    rating: 4.6,
    description: {
      summary: "Capture every moment with the Google Pixel 9's exceptional camera.",
      specifications: {
        Brand: "Google",
        "Operating System": "Android 14",
        "RAM Memory Installed Size": "8 GB",
        "Memory Storage Capacity": "128 GB",
        "Screen Size": "6.3 Inches",
        "Model Name": "Pixel 9",
        "Wireless Carrier": "Unlocked",
        "Cellular Technology": "5G",
        Color: "Stormy Sky",
        "Connector Type": "USB Type C"
      }
    }
  },
  {
    id: 4,
    category: "Phone",
    name: "OnePlus 13R 256GB",
    price: "$729.99",
    image: oneplusImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "The OnePlus 13R offers a seamless experience with its powerful performance.",
      specifications: {
        Brand: "OnePlus",
        "Operating System": "Android 14",
        "RAM Memory Installed Size": "8 GB",
        "Memory Storage Capacity": "256 GB",
        "Screen Size": "6.8 Inches",
        "Model Name": "OnePlus 13R",
        "Wireless Carrier": "Unlocked",
        "Cellular Technology": "5G",
        Color: "Midnight Black",
        "Connector Type": "USB Type C"
      }
    }
  },
  {
    id: 5,
    category: "Phone",
    name: "Samsung Galaxy ZFold6",
    price: "$1,899.99",
    image: samsungImg1,
    quantity: 5,
    rating: 4.8,
    description: {
      summary: "Unfold the future with the Samsung Galaxy ZFold6's innovative design.",
      specifications: {
        Brand: "Samsung",
        "Operating System": "Android 14",
        "RAM Memory Installed Size": "12 GB",
        "Memory Storage Capacity": "512 GB",
        "Screen Size": "7.6 Inches (Main), 6.3 Inches (Cover)",
        "Model Name": "Galaxy ZFold6",
        "Wireless Carrier": "AT&T",
        "Cellular Technology": "5G",
        Color: "Pink Gold",
        "Connector Type": "USB Type C"
      }
    }
  },
  {
    id: 6,
    category: "Laptop",
    name: "MacBook Air M4 Pro",
    price: "$1,599.99",
    image: macbookImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "The MacBook Air M4 Pro is lightweight, powerful, and perfect for professionals.",
      specifications: {
        Brand: "Apple",
        "Operating System": "macOS Sequoia",
        "RAM Memory Installed Size": "16 GB",
        "Memory Storage Capacity": "512 GB SSD",
        "Screen Size": "13.6 Inches",
        "Model Name": "MacBook Air M4 Pro",
        Processor: "Apple M4 (10-core CPU, 10-core GPU)",
        Color: "Space Gray",
        "Connector Type": "Thunderbolt 4 (USB-C)",
        "Display Resolution": "2560 x 1664 (Liquid Retina)"
      }
    }
  },
  {
    id: 7,
    category: "Laptop",
    name: "Dell G16 Gaming",
    price: "$1,099.99",
    image: dellImg,
    quantity: 5,
    rating: 4.6,
    description: {
      summary: "Level up your gaming experience with the Dell G16 Gaming laptop.",
      specifications: {
        Brand: "Dell",
        "Operating System": "Windows 11 Home",
        "RAM Memory Installed Size": "16 GB DDR5",
        "Memory Storage Capacity": "1 TB SSD",
        "Screen Size": "16 Inches",
        "Model Name": "G16 Gaming",
        Processor: "Intel Core i7-13700H",
        Color: "Metallic Nightshade",
        "Connector Type": "USB 3.2, HDMI 2.1, Thunderbolt 4",
        "Graphics Card": "NVIDIA GeForce RTX 4060"
      }
    }
  },
  {
    id: 8,
    category: "Laptop",
    name: "HP Spectre x360",
    price: "$1,249.99",
    image: hpImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "The HP Spectre x360 combines style and performance in a sleek package.",
      specifications: {
        Brand: "HP",
        "Operating System": "Windows 11 Pro",
        "RAM Memory Installed Size": "16 GB LPDDR4x",
        "Memory Storage Capacity": "512 GB SSD",
        "Screen Size": "13.5 Inches",
        "Model Name": "Spectre x360",
        Processor: "Intel Core i7-1355U",
        Color: "Nightfall Black",
        "Connector Type": "USB-C, Thunderbolt 4, HDMI",
        "Display Resolution": "3000 x 2000 (OLED)"
      }
    }
  },
  {
    id: 9,
    category: "Laptop",
    name: "Lenovo ThinkPad X1 Carbon",
    price: "$1,299.99",
    image: lenovoImg,
    quantity: 5,
    rating: 4.6,
    description: {
      summary: "The Lenovo ThinkPad X1 Carbon is built for productivity and durability.",
      specifications: {
        Brand: "Lenovo",
        "Operating System": "Windows 11 Pro",
        "RAM Memory Installed Size": "32 GB LPDDR5",
        "Memory Storage Capacity": "1 TB SSD",
        "Screen Size": "14 Inches",
        "Model Name": "ThinkPad X1 Carbon Gen 11",
        Processor: "Intel Core i7-1365U",
        Color: "Black",
        "Connector Type": "USB-C, USB-A, HDMI 2.1",
        "Display Resolution": "1920 x 1200 (WUXGA)"
      }
    }
  },
  {
    id: 10,
    category: "Laptop",
    name: "Asus ROG Zephyrus",
    price: "$1,399.99",
    image: asusImg,
    quantity: 5,
    rating: 4.7,
    description: {
      summary: "The Asus ROG Zephyrus delivers top-tier gaming performance in a compact design.",
      specifications: {
        Brand: "Asus",
        "Operating System": "Windows 11 Home",
        "RAM Memory Installed Size": "16 GB DDR5",
        "Memory Storage Capacity": "1 TB SSD",
        "Screen Size": "14 Inches",
        "Model Name": "ROG Zephyrus G14",
        Processor: "AMD Ryzen 9 7940HS",
        Color: "Moonlight White",
        "Connector Type": "USB-C, USB-A, HDMI 2.0",
        "Graphics Card": "NVIDIA GeForce RTX 4060"
      }
    }
  },
  {
    id: 11,
    category: "Accessories",
    name: "iPhone 14 Pro Case",
    price: "$19.99",
    image: caseImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "A durable and stylish case to protect your iPhone 14 Pro.",
      specifications: {
        Brand: "Generic",
        Material: "Silicone",
        Color: "Midnight Blue",
        Compatibility: "iPhone 14 Pro",
        Weight: "1.2 oz",
        Features: "Shock-absorbent, Precise cutouts"
      }
    }
  },
  {
    id: 12,
    category: "Accessories",
    name: "iPhone 16 Pro Clear Case with MagSafe",
    price: "$49.99",
    image: caseImg1,
    quantity: 10,
    rating: 4.6,
    description: {
      summary: "Keep your iPhone 16 Pro safe with this clear case featuring MagSafe compatibility.",
      specifications: {
        Brand: "Apple",
        Material: "Polycarbonate, TPU",
        Color: "Transparent",
        Compatibility: "iPhone 16 Pro",
        Weight: "1.4 oz",
        Features: "MagSafe support, Anti-yellowing coating"
      }
    }
  },
  {
    id: 13,
    category: "Accessories",
    name: "iPhone 16 Clear Case with MagSafe",
    price: "$39.99",
    image: caseImg2,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "A sleek and transparent case for your iPhone 16 with MagSafe support.",
      specifications: {
        Brand: "Apple",
        Material: "Polycarbonate, TPU",
        Color: "Clear",
        Compatibility: "iPhone 16",
        Weight: "1.3 oz",
        Features: "MagSafe support, Anti-slip grip"
      }
    }
  },
  {
    id: 14,
    category: "Accessories",
    name: "Apple Watch Magnetic Fast Charger",
    price: "$9.99",
    image: cableImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "Quickly charge your Apple Watch with this magnetic fast charger.",
      specifications: {
        Brand: "Apple",
        "Connector Type": "USB-C",
        Color: "White",
        Compatibility: "Apple Watch Series 1-10, Ultra, Ultra 2",
        "Charging Speed": "Up to 7.5W",
        "Cable Length": "1 meter"
      }
    }
  },
  {
    id: 15,
    category: "Accessories",
    name: "Lightning to USB-C Cable (2m)",
    price: "$19.99",
    image: cableImg1,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "A reliable 2-meter Lightning to USB-C cable for your Apple devices.",
      specifications: {
        Brand: "Apple",
        "Connector Type": "Lightning to USB-C",
        Color: "White",
        Compatibility: "iPhone, iPad, iPod with Lightning connector",
        Length: "2 meters",
        "Charging Speed": "Up to 20W"
      }
    }
  },
  {
    id: 16,
    category: "Accessories",
    name: "USB-C to Lightning Cable (2m)",
    price: "$19.99",
    image: cableImg2,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "A high-quality USB-C to Lightning cable for fast and efficient charging.",
      specifications: {
        Brand: "Apple",
        "Connector Type": "USB-C to Lightning",
        Color: "White",
        Compatibility: "iPhone, iPad, iPod with Lightning connector",
        Length: "2 meters",
        "Charging Speed": "Up to 20W"
      }
    }
  },
  {
    id: 17,
    category: "Accessories",
    name: "LIFT",
    price: "$69.99",
    image: mouseImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "The LIFT ergonomic mouse ensures comfort and precision for long hours of use.",
      specifications: {
        Brand: "Logitech",
        "Connector Type": "USB-A (Logi Bolt Receiver), Bluetooth",
        Color: "Graphite",
        Compatibility: "Windows, macOS, Linux, ChromeOS",
        DPI: "400-4000",
        "Battery Life": "Up to 24 months (AA battery)"
      }
    }
  },
  {
    id: 18,
    category: "Accessories",
    name: "MX Anywhere 3",
    price: "$79.99",
    image: mouseImg1,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "The MX Anywhere 3 mouse offers portability and performance on the go.",
      specifications: {
        Brand: "Logitech",
        "Connector Type": "USB-C (Charging), Bluetooth, Logi Bolt",
        Color: "Pale Grey",
        Compatibility: "Windows, macOS, iPadOS, ChromeOS",
        DPI: "200-4000",
        "Battery Life": "Up to 70 days (rechargeable)"
      }
    }
  },
  {
    id: 19,
    category: "Accessories",
    name: "MX Master 3S",
    price: "$99 Empresariales.99",
    image: mouseImg2,
    quantity: 10,
    rating: 4.7,
    description: {
      summary: "The MX Master 3S mouse delivers advanced features for productivity enthusiasts.",
      specifications: {
        Brand: "Logitech",
        "Connector Type": "USB-C (Charging), Bluetooth, Logi Bolt",
        Color: "Black",
        Compatibility: "Windows, macOS, Linux, ChromeOS",
        DPI: "200-8000",
        "Battery Life": "Up to 70 days (rechargeable)"
      }
    }
  },
  {
    id: 20,
    category: "Accessories",
    name: "Anker Power Bank 20,000mAh",
    price: "$39.99",
    image: powerbankImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "Stay powered on the go with the Anker 20,000mAh power bank.",
      specifications: {
        Brand: "Anker",
        "Connector Type": "USB-C, USB-A",
        Color: "Black",
        Capacity: "20,000mAh",
        Ports: "2 USB-A, 1 USB-C",
        "Charging Speed": "Up to 20W (PowerIQ 3.0)"
      }
    }
  },
  {
    id: 21,
    category: "Gaming",
    name: "PlayStation 5 Console",
    price: "$499.99",
    image: ps5Img,
    quantity: 5,
    rating: 4.8,
    description: {
      summary: "Experience next-gen gaming with the PlayStation 5 console.",
      specifications: {
        Brand: "Sony",
        "Storage Capacity": "825 GB SSD",
        Color: "White",
        Connectivity: "HDMI, USB-C, USB-A",
        Processor: "AMD Ryzen Zen 2, 8-core",
        "Graphics Card": "AMD RDNA 2",
        "Max Resolution": "8K"
      }
    }
  },
  {
    id: 22,
    category: "Gaming",
    name: "Xbox Wireless Controller",
    price: "$59.99",
    image: xboxImg,
    quantity: 10,
    rating: 4.5,
    description: {
      summary: "Enhance your gaming experience with the Xbox Wireless Controller.",
      specifications: {
        Brand: "Microsoft",
        Connectivity: "Bluetooth, USB-C, Xbox Wireless",
        Color: "Carbon Black",
        Compatibility: "Xbox Series X|S, Xbox One, Windows, Android, iOS",
        "Battery Life": "Up to 40 hours (AA or rechargeable)",
        Features: "Textured grip, Custom button mapping"
      }
    }
  },
  {
    id: 23,
    category: "Gaming",
    name: "Razer Kraken Headset",
    price: "$79.99",
    image: headsetImg,
    quantity: 5,
    rating: 4.6,
    description: {
      summary: "Immerse yourself in gaming with the Razer Kraken headset's superior sound quality.",
      specifications: {
        Brand: "Razer",
        "Connector Type": "3.5mm Audio Jack, USB",
        Color: "Black",
        Compatibility: "PC, PS5, Xbox, Nintendo Switch",
        Audio: "7.1 Surround Sound",
        Microphone: "Retractable, Noise-cancelling"
      }
    }
  },
  {
    id: 24,
    category: "Gaming",
    name: "Meta Quest 3 VR Headset",
    price: "$499.99",
    image: vrImg,
    quantity: 5,
    rating: 4.5,
    description: {
      summary: "Step into virtual reality with the Meta Quest 3 VR headset.",
      specifications: {
        Brand: "Meta",
        "Storage Capacity": "128 GB",
        Color: "White",
        Connectivity: "Wi-Fi 6E, Bluetooth 5.2",
        Processor: "Qualcomm Snapdragon XR2 Gen 2",
        Display: "4K+ Infinite Display, 120Hz",
        "Battery Life": "Up to 2.2 hours"
      }
    }
  }
]