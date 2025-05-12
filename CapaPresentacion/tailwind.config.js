const path = require('path');

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    path.resolve(__dirname, 'CapaPresentacion/**/*.{aspx,ascx,ashx,asmx,html,htm}'),
    path.resolve(__dirname, 'CapaPresentacion/*.{aspx,ascx,ashx,asmx,html,htm}'),
    path.resolve(__dirname, 'CapaPresentacion/Site.Master'),
    path.resolve(__dirname, 'CapaPresentacion/Site.Mobile.Master'),
    path.resolve(__dirname, 'CapaPresentacion/src/**/*.css')
  ],
  theme: {
    extend: {},
  },
  plugins: [],
  safelist: [
    'bg-gray-100',
    'bg-gray-800',
    'text-white',
    'max-w-7xl',
    'mx-auto',
    'px-4',
    'sm:px-6',
    'lg:px-8',
    'flex',
    'items-center',
    'justify-between',
    'h-16',
    'text-xl',
    'font-bold',
    'hidden',
    'md:block',
    'ml-10',
    'items-baseline',
    'space-x-4',
    'px-3',
    'py-2',
    'rounded-md',
    'text-sm',
    'font-medium',
    'hover:bg-gray-700',
    'bg-blue-500',
    'hover:bg-blue-700',
    'text-white',
    'font-bold',
    'py-2',
    'px-4',
    'rounded',
    'py-8',
    'bg-red-500',
    'test-class'
  ]
} 