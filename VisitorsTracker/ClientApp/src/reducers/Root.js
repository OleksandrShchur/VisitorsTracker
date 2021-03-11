import { reducer as LoginReducer } from './Login';
import * as User from './User';
import { routerReducer } from 'react-router-redux';
import { reducer as formReducer } from 'redux-form';

const rootReducers = {
    login: LoginReducer,
    user: User.reducer,
    form: formReducer,
    routing: routerReducer
};

export default rootReducers;