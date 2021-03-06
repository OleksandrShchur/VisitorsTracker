import React from 'react';
import LeftSidebarWrapper from '../left-sidebar/index';
import { NavMenu } from '../NavMenu/NavMenu';

const Layout = ({ children }) => {
  return (
      <>
          <NavMenu />
          <LeftSidebarWrapper />
          <div id="main" className="container-fluid pl-5">
              {children}
          </div>
      </>
  );
}

export default Layout;