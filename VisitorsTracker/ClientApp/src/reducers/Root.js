import { reducer as LoginReducer } from './Login';
import * as User from './User';
import { routerReducer } from 'react-router-redux';


const rootReducers = {
    login: LoginReducer,
    user: User.reducer,
    routing: routerReducer
};

export default rootReducers;