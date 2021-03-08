export const SET_LOGOUT = "SET_LOGOUT";

export default function logout() {
    revokeToken();
    localStorage.clear();
    return dispatch => {
        dispatch(setLogout());
    }
}

function setLogout() {
    return {
        type: SET_LOGOUT
    };
}

function revokeToken() {
    fetch('api/token/revoke-token', {
        method: "POST"
    });
}
