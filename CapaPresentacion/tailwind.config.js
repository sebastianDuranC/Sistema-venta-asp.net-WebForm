/** @type {import('tailwindcss').config} */
module.exports = {
    content: ["./**/*.aspx", "./**/*.Master"], // Adjust paths as needed
    theme: {
        extend: {
            colors: {
                primary: '#080708', // Para texto, botones y títulos
                secondary: '#DF2935', // Para el fondo del panel izquierdo
                contentbg: '#E6E8E6', // Para el fondo del contenido derecho
            },
        },
    },
    plugins: [],
}