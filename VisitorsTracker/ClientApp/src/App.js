import React, { Component } from 'react';
import { Route } from 'react-router';

import { Layout } from './components/Layout/Layout';
import { Home } from './components/Home/Home';
import { AboutUs } from './components/AboutUs/AboutUs';
import Profile from './components/Profile/index';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/aboutUs' component={AboutUs} />
        <Route path="/profile/" component={Profile} />
      </Layout>
    );
  }
}
