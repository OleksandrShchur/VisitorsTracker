import React, { Component } from 'react';

export default class AboutUs extends Component {
  render() {
    return (
      <div>
        <h1>Про сайт</h1>

        <p>
          Цей сайт створено для моніторингу відвідуваності студентів та покращення ефективності
          ведення журналу
        </p>
        <p>
          Розробка та підтримка здійснюється студентом 3 курсу. Зі всіма питаннями звертатися за поштою <link href="shchur.oleksandr@chnu.edu.ua" />.
        </p>
      </div>
    );
  }
}
