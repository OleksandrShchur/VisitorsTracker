'use strict';

const initialState = {
    user: {
        id: null,
        name: 'Visitors Tracker',
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

export default initialState;