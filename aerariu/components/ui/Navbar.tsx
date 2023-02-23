import Link from 'next/link';
import React from 'react';
import Container from '../layout/Container';
import { FaToriiGate, FaShoppingCart } from 'react-icons/fa';
import { RxDividerVertical } from 'react-icons/rx';

const Navbar = () => {
  return (
    <nav>
      <Container>
        <div className="font-display grid grid-cols-2 text-xl lg:grid-cols-3">
          <span className="font-semibold self-center">
            <FaToriiGate className="inline-block mr-2 text-3xl" />
            <span className="hidden md:inline-block">
              Aerariu Artisan Keycaps
            </span>
          </span>
          <ul className="text-right lg:order-last">
            <li className="inline-block py-3 transition-colors hover:text-pink-400">
              <Link href="/account/cart">
                <FaShoppingCart className="inline-block text-3xl" />
              </Link>
            </li>
            <li className="inline-block py-3">
              <RxDividerVertical className="inline-block text-3xl" />
            </li>
            <li className="inline-block py-3 transition-colors hover:text-pink-400">
              <Link href="/account">Login</Link>
            </li>
          </ul>
          <ul className="col-span-2 lg:col-span-1 text-center">
            <li className="inline-block px-4 py-3 transition-colors hover:text-pink-400">
              <Link href="/">Home</Link>
            </li>
            <li className="inline-block px-4 py-3 transition-colors hover:text-pink-400">
              <Link href="/shop">Shop</Link>
            </li>
            <li className="inline-block px-4 py-3 transition-colors hover:text-pink-400">
              <Link href="/about">About</Link>
            </li>
          </ul>
        </div>
      </Container>
    </nav>
  );
};

export default Navbar;
