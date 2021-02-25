import React, { Component } from 'react';
import { Container } from 'reactstrap';
import LeftSidebarWrapper from '../left-sidebar/index';
import { NavMenu } from '../NavMenu/NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <LeftSidebarWrapper />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
