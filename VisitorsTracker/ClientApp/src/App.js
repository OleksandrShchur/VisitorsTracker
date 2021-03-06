import React, { Component } from 'react';
import { Route } from 'react-router';
import { BrowserRouter } from 'react-router-dom';

import { Layout } from './components/Layout/Layout';
import { Home } from './components/Home/Home';
import { AboutUs } from './components/AboutUs/AboutUs';
import Profile from './components/Profile/index';

import './custom.css'

export default class App extends Component {
  render() {
    return (
      <BrowserRouter basename={this.props.baseUrl}>
        <Layout store={this.props.store}>
          <Route exact path='/' component={Home} />
          <Route path='/aboutUs' component={AboutUs} />
          <Route path="/profile" component={Profile} />
        </Layout>
      </BrowserRouter>

    );
  }
}
