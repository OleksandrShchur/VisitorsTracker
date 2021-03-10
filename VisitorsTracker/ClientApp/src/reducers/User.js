import initialState from '../store/InitialState';
import { SET_USER } from '../actions/Login';
import { SET_LOGOUT } from '../actions/Logout';
//import { editBirthday } from '../actions/EditProfile/editBirthday';
//import { editGender } from '../actions/EditProfile/EditGender';
//import { changeAvatar } from '../actions/EditProfile/change-avatar';
import { authenticate } from '../actions/Authentication';

export const reducer = (state = initialState.user, action) => {
    switch (action.type) {
        case SET_USER:
            return action.payload;

        case authenticate.SET_AUTHENTICATE:
            localStorage.setItem("token", action.payload.token);
            return action.payload;

        case SET_LOGOUT:
            return initialState.user;

        // case editBirthday.UPDATE:
        //     return {
        //         ...state,
        //         birthday: new Date(action.payload).toDateString()
        //     }

        // case editGender.UPDATE:
        //     return {
        //         ...state,
        //         gender: action.payload.Gender
        //     }
        // case changeAvatar.UPDATE:
        //     return {
        //         ...state,
        //         photoUrl: action.payload

        //     }
        default:
            return state;
    }
}