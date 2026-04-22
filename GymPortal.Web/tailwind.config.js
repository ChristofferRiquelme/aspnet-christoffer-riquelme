/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Views/**/*.cshtml",
    "./Areas/**/*.cshtml",
    "./Pages/**/*.cshtml"
  ],
  theme: {
    extend: {
      fontFamily: {
        dm: [' "DM Sans"', 'sans-serif'],
      }
    },
  },
  plugins: [],
}

