import React, { Component } from "react";
import GoogleLogin from '../../containers/GoogleLogin/googleLogin';

class Login extends Component {

  render() {
    const { loginError } = this.props;

    return (
      <div className="auth">
        <div className="d-flex justify-content-around mb-3">
          <GoogleLogin />
        </div>
        <div className="text-center">
          {loginError &&
            <p className="text-danger text-center">{loginError}</p>
          }
        </div>
      </div>
    );
  }
}

export default Login;
