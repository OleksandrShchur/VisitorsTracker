import { routerReducer } from 'react-router-redux';
import { reducer as LoginReducer } from './login';
import { reducer as formReducer } from 'redux-form';
import * as User from './user';
import * as Register from './register';
import * as ChangeAvatar from './editReducers/change_avatar';
import * as Auth from './authenticationReducer';
import * as authReducer from './authReducer';
import * as Alert from './alert';
import * as ContactUs from './contact-us';

const rootReducers = {
    auth: authReducer.authReducer,
    user: User.reducer,
    routing: routerReducer,
    form: formReducer,
    login: LoginReducer,
    editBirthday: Birthday.reducer,
    register: Register.reducer,

    change_avatar: ChangeAvatar.reducer,
    profile: Profile.reducer,
    authenticate: Auth.reducer,
    alert: Alert.reducer,
    contactUs: ContactUs.reducer,
    notification: Notification.reducer
};

export default rootReducers;
