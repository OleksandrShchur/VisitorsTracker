import AuthenticationService from '../services/authenticationService';

export const SET_LOGIN_PENDING = "SET_LOGIN_PENDING";
export const SET_LOGIN_SUCCESS = "SET_LOGIN_SUCCESS";
export const SET_LOGIN_ERROR = "SET_LOGIN_ERROR";
export const SET_USER = "SET_USER";

const api_serv = new AuthenticationService();

export default function loginGoogle(tokenId, email, name, imageUrl) {
    return dispatch => {
        dispatch(setLoginPending(true));
        
        const res = api_serv.setGoogleLogin({
        TokenId: tokenId,
        Email: email,
        Name: name,
        PhotoUrl: imageUrl
        });

        loginResponseHandler(res, dispatch);
    }
}

const loginResponseHandler = (res, dispatch) => {
    res.then(response => {
        if (!response.error) {  
            dispatch(setUser(response));
            dispatch(setLoginSuccess(true));
    
            localStorage.setItem('token', response.token);
            localStorage.setItem('id', response.id);
        } 
        else {
            localStorage.clear();
            dispatch(setLoginError(response.error));
        }
    });
};


export function setUser(data) {
    return {
        type: SET_USER,
        payload: data
    };
}

export function setLoginPending(isLoginPending) {
    return {
        type: SET_LOGIN_PENDING,
        isLoginPending
    };
}

export function setLoginSuccess(isLoginSuccess) {
    return {
        type: SET_LOGIN_SUCCESS,
        isLoginSuccess
    };
}

export function setLoginError(loginError) {
    return {
        type: SET_LOGIN_ERROR,
        loginError
    };
}