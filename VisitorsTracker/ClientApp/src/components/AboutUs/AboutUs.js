import React, { Component } from 'react';
import Paper from '@material-ui/core/Paper';
import './AboutUs.css';

export class AboutUs extends Component {
  render() {
    return (
      <Paper
        elevation={3}
      >
        <div className="aboutUs">
          <h1 className="center">Про сайт</h1>

          <p>
            Цей сайт створено для моніторингу відвідуваності студентів та покращення ефективності
            ведення журналу.
        </p>
          <p>
            Розробка та підтримка здійснюється студентом 3 курсу. Зі всіма питаннями звертатися за поштою
          <a href="shchur.oleksandr@chnu.edu.ua"> shchur.oleksandr@chnu.edu.ua</a>.
        </p>
          <p>
            У розробці сайту використано такі технології:
          <li>ASP.NET CORE</li>
            <li>REACT.JS</li>
            <li>MS SQL SERVER</li>
            <li>MATERIAL UI</li>
            <li>HTML</li>
            <li>CSS</li>
            <li>ENTITY FRAMEWORK CORE</li>
            <li>МОДЕЛЬ MVC</li>
          </p>
          <p>
            Даний проект розміщено на публічному репозиторії на <a href="https://github.com/OleksandrShchur/VisitorsTracker">GitHub</a>.
        </p>
        <br />
        </div>
      </Paper>
    );
  }
}
