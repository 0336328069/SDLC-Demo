'use client'

import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/molecules/Card'
import { cn } from '@/lib/utils'
import { 
  Layers, 
  Zap, 
  RefreshCw, 
  Puzzle, 
  Shield, 
  Smartphone,
  ArrowRight
} from 'lucide-react'

interface Feature {
  title: string
  description: string
  icon: React.ComponentType<{ className?: string }>
  gradient: string
}

interface FeatureGridProps {
  className?: string
}

export function FeatureGrid({ className }: FeatureGridProps) {
  const features: Feature[] = [
    {
      title: 'Clean Architecture',
      description: 'Built with DDD principles and SOLID design patterns for maintainable code.',
      icon: Layers,
      gradient: 'from-blue-500 to-cyan-500'
    },
    {
      title: 'Modern Stack',
      description: '.NET 9 backend with Entity Framework Core and Next.js 15 frontend.',
      icon: Zap,
      gradient: 'from-purple-500 to-pink-500'
    },
    {
      title: 'CQRS Pattern',
      description: 'Command Query Responsibility Segregation with MediatR for scalable architecture.',
      icon: RefreshCw,
      gradient: 'from-green-500 to-emerald-500'
    },
    {
      title: 'Modular Design',
      description: 'Modular monolith approach for organized and scalable development.',
      icon: Puzzle,
      gradient: 'from-orange-500 to-red-500'
    },
    {
      title: 'Type Safety',
      description: 'Full TypeScript support with strict typing for better developer experience.',
      icon: Shield,
      gradient: 'from-indigo-500 to-purple-500'
    },
    {
      title: 'Responsive UI',
      description: 'Beautiful, responsive design with TailwindCSS and shadcn/ui components.',
      icon: Smartphone,
      gradient: 'from-teal-500 to-blue-500'
    }
  ]

  return (
    <section className={cn(
      'relative py-24 bg-gray-50 dark:bg-gray-900',
      className
    )}>
      {/* Background Pattern */}
      <div className="absolute inset-0 bg-grid-pattern opacity-5"></div>
      
      <div className="container relative w-full">
        <div className="text-center mb-20">
          <div className="inline-flex items-center gap-2 bg-blue-100 dark:bg-blue-900/30 rounded-full px-6 py-2 mb-6">
            <Zap className="w-4 h-4 text-blue-600" />
            <span className="text-sm font-medium text-blue-700 dark:text-blue-300">Powerful Features</span>
          </div>
          
          <h2 className="text-4xl md:text-6xl font-bold tracking-tight mb-6">
            <span className="bg-gradient-to-r from-gray-900 via-blue-900 to-gray-900 dark:from-white dark:via-blue-100 dark:to-white bg-clip-text text-transparent">
              Everything you need
            </span>
          </h2>
          
          <p className="mx-auto max-w-3xl text-xl text-gray-600 dark:text-gray-300">
            Manage your software development lifecycle effectively with our comprehensive suite of modern tools and architectures.
          </p>
        </div>

        <div className="grid gap-8 md:grid-cols-2 lg:grid-cols-3">
          {features.map((feature, index) => (
            <Card 
              key={index} 
              className="group relative overflow-hidden border-0 bg-white/60 dark:bg-gray-800/60 backdrop-blur-sm hover:bg-white/80 dark:hover:bg-gray-800/80 transition-all duration-500 hover:scale-105 hover:shadow-2xl cursor-pointer"
            >
              {/* Gradient Border */}
              <div className={`absolute inset-0 bg-gradient-to-r ${feature.gradient} opacity-0 group-hover:opacity-100 transition-opacity duration-500`}>
                <div className="absolute inset-[1px] bg-white dark:bg-gray-800 rounded-lg"></div>
              </div>
              
              <CardHeader className="relative z-10 pb-4">
                <div className={`w-16 h-16 rounded-2xl bg-gradient-to-r ${feature.gradient} p-4 mb-6 group-hover:scale-110 transition-transform duration-300`}>
                  <feature.icon className="w-8 h-8 text-white" />
                </div>
                
                <CardTitle className="text-2xl font-bold text-gray-900 dark:text-white group-hover:text-transparent group-hover:bg-gradient-to-r group-hover:bg-clip-text group-hover:from-blue-600 group-hover:to-purple-600 transition-all duration-300">
                  {feature.title}
                </CardTitle>
              </CardHeader>
              
              <CardContent className="relative z-10">
                <CardDescription className="text-lg text-gray-600 dark:text-gray-300 leading-relaxed mb-6">
                  {feature.description}
                </CardDescription>
                
                <div className="flex items-center text-blue-600 dark:text-blue-400 font-medium opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                  <span className="mr-2">Learn more</span>
                  <ArrowRight className="w-4 h-4 group-hover:translate-x-1 transition-transform duration-300" />
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      </div>

      {/* Custom Styles */}
      <style jsx>{`
        .bg-grid-pattern {
          background-image: url("data:image/svg+xml,%3Csvg width='60' height='60' viewBox='0 0 60 60' xmlns='http://www.w3.org/2000/svg'%3E%3Cg fill='none' fill-rule='evenodd'%3E%3Cg fill='%23000000' fill-opacity='0.1'%3E%3Ccircle cx='30' cy='30' r='1'/%3E%3C/g%3E%3C/g%3E%3C/svg%3E");
        }
      `}</style>
    </section>
  )
} 