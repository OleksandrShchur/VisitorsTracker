import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { createBrowserHistory } from 'history';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import configureStore from './store/ConfigureStore';
import { setUser } from './actions/Login';
import 'bootstrap/dist/css/bootstrap.css';
import initState from './store/InitialState';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });

const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

async function AuthUser(token) {
  if (!token)
    return;

  const res = await fetch('api/Authentication/login_token', {
    method: 'post',
    headers: new Headers({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    }),
  });

  if (res.ok) {
    const user = await res.json();

    store.dispatch(setUser(user));
  } else {
    localStorage.clear();
  }
}

const token = localStorage.getItem('token');

if (token) {
  AuthUser(token);
}

const rootElement = document.getElementById('root');

ReactDOM.render(
  <Provider store={store}>
    <App
      baseUrl={baseUrl}
      store={initState} />
  </ Provider>,
  rootElement);

registerServiceWorker();

