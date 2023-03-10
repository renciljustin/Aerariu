import Head from 'next/head';
import { Fragment } from 'react';
import BackgroundObjects from '../ui/BackgroundObjects';
import Footer from '../ui/Footer';
import Navbar from '../ui/Navbar';

const Layout = ({ children }: React.PropsWithChildren) => {
  return (
    <Fragment>
      <Head>
        <title>Aerariu Crafts</title>
        <meta name="description" content="Generated by create next app" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <header>
        <Navbar />
      </header>
      <main>{children}</main>
      <footer>
        <Footer />
      </footer>
      <BackgroundObjects />
    </Fragment>
  );
};

export default Layout;
