import initialState from '../store/initialState';
import { SET_USER } from '../actions/login';
import { SET_LOGOUT } from '../actions/logout';
import { addUserCategory } from '../actions/EditProfile/addUserCategory';
import { editBirthday } from '../actions/EditProfile/editBirthday';
import { changeAvatar } from '../actions/EditProfile/change-avatar';
import { authenticate } from '../actions/authentication';

export const reducer = (state = initialState.user, action) => {
    switch (action.type) {
        case SET_USER:
            return action.payload;

        case authenticate.SET_AUTHENTICATE:
            localStorage.setItem("token", action.payload.token);
            return action.payload;

        case SET_LOGOUT:
            return initialState.user;

        case editBirthday.UPDATE:
            return {
                ...state,
                birthday: new Date(action.payload).toDateString()
            }

        case changeAvatar.UPDATE:
            return {
                ...state,
                photoUrl: action.payload

            }
        default:
            return state;
    }
}