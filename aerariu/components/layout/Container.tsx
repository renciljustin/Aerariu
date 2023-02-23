import React from 'react';

const Container = ({ children }: React.PropsWithChildren) => {
  return (
    <div className="max-w-screen-xl mx-auto px-2 md:px-4 xl:px-0">
      {children}
    </div>
  );
};

export default Container;
