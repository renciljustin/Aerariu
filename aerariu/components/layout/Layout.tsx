import Head from 'next/head';
import { Fragment } from 'react';
import Footer from '../ui/Footer';
import Navbar from '../ui/Navbar';

const Layout = ({ children }: React.PropsWithChildren) => {
  return (
    <Fragment>
      <Head>
        <title>Aerariu Crafts</title>
      </Head>
      <header>
        <Navbar />
      </header>
      <main>{children}</main>
      <footer>
        <Footer />
      </footer>
    </Fragment>
  );
};

export default Layout;
