import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';

import config from '../../config';
import loginGoogle from '../../actions/Login';

import './googleLogin.css';

class LoginGoogle extends Component {
    render() {
        const responseGoogle = (response) => {
            if (typeof response.profileObj.email === 'undefined') {
                this.props.login.loginError = " Please add email to your google account!"
            }
            loginGoogle(
                response.tokenId,
                response.profileObj.email,
                response.profileObj.name,
                response.profileObj.imageUrl
            );
        }

        return (
            <div>
                <GoogleLogin
                    clientId={config.GOOGLE_CLIENT_ID}
                    render={renderProps => (
                        <button className="btnGoogle" onClick={renderProps.onClick} disabled={renderProps.disabled}>
                            <i className="fab fa-google fa-lg"></i>
                            <span>Вхід</span>
                        </button>
                    )}
                    onSuccess={responseGoogle}
                    version="3.2"
                />
            </div>
        );
    }
};

export default LoginGoogle;