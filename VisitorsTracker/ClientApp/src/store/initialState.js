'use strict';

import eventHelper from '../components/helpers/eventHelper';

const initialState = {
    modalWind: {
        isOpen: false
    },
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
        photoUrl: null
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
    editUsername: {
        isEditUsernamePending: false,
        isEditUsernameSuccess: false,
        EditUsernameError: {}
    },
    countries: {
        isPending: false,
        isError: false,
        data: []
    },
    cities: {
        isPending: false,
        isError: false,
        data: []
    },
    users: {
        isPending: true,
        isError: false,
        editedUser: null,
        userSearchFilter: null,
        data: {
            items: [],
            pageViewModel: {}
        }
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
    notification:
    {
        messages: [],
        seen_messages: [],
        events: []
    }
};

export default initialState;
