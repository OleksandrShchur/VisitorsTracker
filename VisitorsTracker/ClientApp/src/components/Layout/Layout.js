import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from '../NavMenu/NavMenu';
import LeftSidebar from '../Left-sidebar/index';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <LeftSidebar />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
