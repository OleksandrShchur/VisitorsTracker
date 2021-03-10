import React, { Component } from 'react';
import { Container } from 'reactstrap';
import LeftSidebar from '../Left-sidebar/index';

import './Layout.css';
import './ColorLib.css';

export class Layout extends Component {
  render () {
    return (
      <div>
        <LeftSidebar store={this.props.store}/>
        <Container store={this.props.store}>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
