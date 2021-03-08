const initialState = {
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
        categories: []
    },

    login: {
        isLoginPending: false,
        isLoginSuccess: false,
        loginError: null
    }
}

export default initialState;