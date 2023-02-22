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
    extend: {},
  },
  variants: {
    extend: {},
  },
  plugins: [],
};
