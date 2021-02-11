import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import config from '../config';

class LoginGoogle extends Component {
    render() {
        const responseGoogle = (response) => {
            if (typeof response.profileObj.email === 'undefined') {
                this.props.login.loginError = " Please add email to your google account!"
            }
            this.props.loginGoogle(
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
                            <span>Log in</span>
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