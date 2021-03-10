import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerMiddleware } from 'react-router-redux';
import rootReducers from '../reducers/Root';

const initState = {
  user: {
      id: null,
      name: null,
      email: null,
      phone: null,
      birthday: null,
      gender: null,
      role: null,
      photoUrl: null,
      token: null
  },

  login: {
      isLoginPending: false,
      isLoginSuccess: false,
      loginError: null
  },
  profile: {
      isPending: true,
      isError: false,
      data: null
  },
  authenticate: {
      isPending: false,
      isSucces: false,
      isError: null,
      data: []
  }
}

export default function configureStore(history, initialState) {

  const middleware = [
    thunk,
    routerMiddleware(history)
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.devToolsExtension());
    }

  const rootReducer = combineReducers({
    ...rootReducers
  });

  return createStore(
    rootReducer,
    initState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
