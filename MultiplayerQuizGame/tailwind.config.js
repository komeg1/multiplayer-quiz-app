/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html}",
        "./node_modules/flowbite/**/*.js"],
    darkMode: 'class',
  theme: {
    extend: {},
  },
    plugins: [
        require('flowbite/plugin')
    ],
}

