'use strict';

const initialState = {
    resetError: {
        isError: false,
    },
    user: {
        id: null,
        name: null,
        email: null,
        phone: null,
        birthday: null,
        gender: null,
        role: null,
        photoUrl: null,
        token: null,
    },
    roles: {
        isPending: false,
        isError: false,
        data: []
    },
    login: {
        isLoginPending: false,
        isLoginSuccess: false,
        loginError: null
    },
    register: {
        isRegisterPending: false,
        isRegisterSuccess: false,
        registerError: null
    },
    change_avatar: {
        isPending: false,
        isSuccess: false,
        Error: {}
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
    },
    alert: {
        variant: null,
        message: null,
        autoHideDuration: null,
        open: false
    },
    contactUs: {
        isPending: false,
        isSuccess: false,
        isError: null
    },
};

export default initialState;
