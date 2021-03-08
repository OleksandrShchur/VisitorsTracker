import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import configureStore from './store/ConfigureStore';
import { BrowserRouter } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.css';


const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const history = createBrowserHistory({ basename: baseUrl });
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

ReactDOM.render(
  <Provider store={store}>
    <BrowserRouter basename={baseUrl}>
      <App />
    </BrowserRouter>
  </ Provider>,
  rootElement);

registerServiceWorker();

