import React, { Component } from 'react';
import {
  BrowserRouter,
  Route
} from 'react-router-dom';

import Layout from '../Layout/Layout';
import { Home } from '../Home/Home';
import { Authentication } from '../Authentication/authentication';
import { AboutUs } from '../AboutUs/AboutUs';

import './App.css';

export default class App extends Component {
  
  render () {
    return (
      <BrowserRouter>
        <Layout>
          <Route exact path="/" component={Home} />
          <Route path='/aboutUs' component={AboutUs} />
          <Route path="/authentication/:id/:token" component={Authentication} />
        </Layout>
      </BrowserRouter>
      
    );
  }
}
