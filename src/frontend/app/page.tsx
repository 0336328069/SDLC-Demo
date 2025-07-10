import { Navbar } from '@/components/organisms/Navbar'
import { HeroSection } from '@/components/organisms/HeroSection'
import { FeatureGrid } from '@/components/organisms/FeatureGrid'

export default function HomePage() {
  return (
    <div className="min-h-screen bg-background">
      <Navbar />
      <main className="pt-16">
        <HeroSection />
        <FeatureGrid />
      </main>
    </div>
  )
} 