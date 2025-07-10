'use client'

import { Button } from '@/components/atoms/Button'
import { cn } from '@/lib/utils'
import { Code, Zap, Shield, Users } from 'lucide-react'

interface HeroSectionProps {
  className?: string
}

export function HeroSection({ className }: HeroSectionProps) {
  return (
    <section className={cn(
      'relative overflow-hidden',
      className
    )}>
      {/* Animated Background */}
      <div className="absolute inset-0 bg-gradient-to-br from-blue-50 via-indigo-50 to-purple-50 dark:from-gray-900 dark:via-blue-900 dark:to-purple-900">
        <div className="absolute inset-0 bg-[url('data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNjAiIGhlaWdodD0iNjAiIHZpZXdCb3g9IjAgMCA2MCA2MCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZyBmaWxsPSJub25lIiBmaWxsLXJ1bGU9ImV2ZW5vZGQiPjxnIGZpbGw9IiMwMDAwMDAiIGZpbGwtb3BhY2l0eT0iMC4wNSI+PGNpcmNsZSBjeD0iMzAiIGN5PSIzMCIgcj0iMiIvPjwvZz48L2c+PC9zdmc+')] opacity-40"></div>
      </div>

      {/* Floating Elements */}
      <div className="absolute top-20 left-10 w-20 h-20 bg-blue-200 rounded-full mix-blend-multiply filter blur-xl opacity-70 animate-blob"></div>
      <div className="absolute top-40 right-10 w-20 h-20 bg-purple-200 rounded-full mix-blend-multiply filter blur-xl opacity-70 animate-blob animation-delay-2000"></div>
      <div className="absolute bottom-20 left-20 w-20 h-20 bg-pink-200 rounded-full mix-blend-multiply filter blur-xl opacity-70 animate-blob animation-delay-4000"></div>

      <div className="container relative flex flex-col items-center justify-center min-h-[90vh] text-center space-y-8 md:space-y-12 py-16 md:py-20">
        {/* Main Hero Content */}
        <div className="space-y-6 md:space-y-8 max-w-4xl mx-auto">
          <div className="inline-flex items-center gap-2 bg-white/20 dark:bg-gray-800/20 backdrop-blur-sm rounded-full px-4 md:px-6 py-2 border border-white/20">
            <Zap className="w-4 h-4 text-blue-600" />
            <span className="text-sm font-medium text-gray-700 dark:text-gray-300">Powered by .NET 9 & Next.js 15</span>
          </div>

          <h1 className="text-4xl md:text-6xl lg:text-7xl xl:text-8xl font-bold tracking-tight leading-tight">
            <span className="bg-gradient-to-r from-blue-600 via-purple-600 to-blue-800 bg-clip-text text-transparent">
              Modern SDLC
            </span>
            <br />
            <span className="text-gray-900 dark:text-white">Management</span>
          </h1>
          
          <p className="mx-auto max-w-3xl text-lg md:text-xl lg:text-2xl text-gray-600 dark:text-gray-300 leading-relaxed">
            Streamline your software development lifecycle with our comprehensive 
            project management platform featuring modular architecture and real-time collaboration.
          </p>
        </div>
        
        {/* CTA Buttons */}
        <div className="flex flex-col sm:flex-row gap-4 md:gap-6 items-center w-full max-w-lg">
          <Button size="lg" className="w-full sm:min-w-[220px] h-12 md:h-14 text-base md:text-lg bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-700 hover:to-purple-700 shadow-lg hover:shadow-xl transition-all duration-300 transform hover:scale-105">
            <Code className="w-5 h-5 mr-2" />
            Get Started Free
          </Button>
          <Button variant="outline" size="lg" className="w-full sm:min-w-[220px] h-12 md:h-14 text-base md:text-lg border-2 backdrop-blur-sm bg-white/10 hover:bg-white/20 transition-all duration-300">
            View Live Demo
          </Button>
        </div>

        {/* Stats Grid */}
        <div className="grid grid-cols-2 md:grid-cols-4 gap-4 md:gap-8 w-full max-w-4xl">
          {[
            { icon: Users, number: "500+", label: "Active Projects", color: "text-blue-600" },
            { icon: Shield, number: "99.9%", label: "Uptime SLA", color: "text-green-600" },
            { icon: Zap, number: "50ms", label: "Response Time", color: "text-purple-600" },
            { icon: Code, number: "24/7", label: "Support", color: "text-orange-600" }
          ].map((stat, index) => (
            <div key={index} className="group">
              <div className="bg-white/20 dark:bg-gray-800/20 backdrop-blur-sm rounded-xl md:rounded-2xl p-4 md:p-6 border border-white/20 hover:bg-white/30 hover:scale-105 transition-all duration-300">
                <stat.icon className={`w-6 md:w-8 h-6 md:h-8 mx-auto mb-2 md:mb-3 ${stat.color}`} />
                <div className="text-2xl md:text-3xl font-bold text-gray-900 dark:text-white mb-1">{stat.number}</div>
                <div className="text-xs md:text-sm text-gray-600 dark:text-gray-400">{stat.label}</div>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Custom CSS for animations */}
      <style jsx>{`
        @keyframes blob {
          0% { transform: translate(0px, 0px) scale(1); }
          33% { transform: translate(30px, -50px) scale(1.1); }
          66% { transform: translate(-20px, 20px) scale(0.9); }
          100% { transform: translate(0px, 0px) scale(1); }
        }
        .animate-blob {
          animation: blob 7s infinite;
        }
        .animation-delay-2000 {
          animation-delay: 2s;
        }
        .animation-delay-4000 {
          animation-delay: 4s;
        }
      `}</style>
    </section>
  )
} 