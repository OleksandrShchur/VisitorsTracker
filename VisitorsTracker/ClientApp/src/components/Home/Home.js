import React, { Component } from 'react';
import logo from '../../Images/mainDepartment.jpg';

import './Home.css';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <img src={logo}/>
      </div>
    );
  }
}
