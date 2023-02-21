import Link from 'next/link';
import React from 'react';
import Container from '../layout/Container';

const Navbar = () => {
  return (
    <nav>
      <Container>
        <div className="grid grid-cols-3">
          <span className="self-center text-xl">Aerariu Artisan Keycaps</span>
          <ul className="text-center">
            <li className="inline-block px-4 py-3">
              <Link href="/">Home</Link>
            </li>
            <li className="inline-block px-4 py-3">
              <Link href="/shop">Shop</Link>
            </li>
            <li className="inline-block px-4 py-3">
              <Link href="/about">About</Link>
            </li>
          </ul>
          <ul className="text-right">
            <li className="inline-block px-4 py-3">
              <Link href="/account/wishlists">Wishlist</Link>
            </li>
            <li className="inline-block px-4 py-3">
              <Link href="/account/cart">Cart</Link>
            </li>
            <li className="inline-block px-4 py-3">
              <Link href="/account">Account</Link>
            </li>
          </ul>
        </div>
      </Container>
    </nav>
  );
};

export default Navbar;
