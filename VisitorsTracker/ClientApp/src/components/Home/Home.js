import React, { Component } from 'react';
import Login from '../Login/index';
import logo from '../../Images/chnuUkr.png';
import './Home.css';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Привіт</h1>
        <p>Вітаємо тебе на нашому сайті.</p>
        <ul>
          <li>Якщо ти вперше на нашому сайті, тобі слід зареєструватися та зв'язатися зі старостою своєї групи або куратором </li>
          <li>Якщо ти вже маєш створений обліковий запис - тоді ти знаєш, що робити</li>
        </ul>
        <img src={logo} className="center"/>
        <Login />
      </div>
    );
  }
}
