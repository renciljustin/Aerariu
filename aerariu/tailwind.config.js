const defaultTheme = require('tailwindcss/defaultTheme');

module.exports = {
  mode: 'jit',
  purge: ['./pages/**/*.tsx', './components/**/*.tsx'],
  darkMode: 'class', // 'media' or 'class'
  theme: {
    fontFamily: {
      display: ['Fredoka', ...defaultTheme.fontFamily.sans],
      body: ['Inter', ...defaultTheme.fontFamily.sans],
    },
    borderRadius: {
      '4xl': '2rem',
      '5xl': '2.5rem',
      '6xl': '3rem',
    },
    extend: {},
  },
  variants: {
    extend: {},
  },
  plugins: [],
};
