import { AuthenticationService } from '../services';

export const authenticate = {
    PENDING: "SET_AUTHENTICATE_PENDING",
    SUCCESS: "SET_AUTHENTICATE_SUCCESS",
    ERROR: "SET_AUTHENTICATE_ERROR",
    SET_AUTHENTICATE: "SET_AUTHENTICATE",
}

const api_serv = new AuthenticationService();

export default function _authenticate(data) {
    return dispatch => {
        dispatch(setAuthenticatePending(true));

        const res = api_serv.auth(data);

        res.then(responce => {
            if (responce.error == null) {
                dispatch(setAuthenticate(responce));
            } else {
                dispatch(setAuthenticateError(responce.error));
            }
        })
    }
}

export function setAuthenticate(data) {
    return {
        type: authenticate.SET_AUTHENTICATE,
        payload: data
    }
}

export function setAuthenticatePending(isAuthenticatePending) {
    return {
        type: authenticate.PENDING,
        isAuthenticatePending
    }
}

export function setAuthenticateSuccess(isAuthenticateSuccess) {
    return {
        type: authenticate.SUCCESS,
        isAuthenticateSuccess
    }
}

export function setAuthenticateError(isAuthenticateError) {
    return {
        type: authenticate.ERROR,
        isAuthenticateError
    }
}
