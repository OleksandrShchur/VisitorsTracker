import React, { Component } from "react";
//import Module from '../helpers';
//import Modalwind2 from '../recoverPassword/modalwind2';
import GoogleLogin from '../../containers/googleLogin';

//const { validate, renderTextField, asyncValidate } = Module;

class Login extends Component {
  //openModal = () => (<Modalwind2 />)

  render() {
    const { pristine, reset, submitting, loginError } = this.props;

    return (
      <div className="auth">
        <div>
          <p>Будь ласка, використайте свою корпоративну пошту для входу в систему.</p>
        </div>
        <div className="d-flex justify-content-around mb-3">
          <GoogleLogin />
        </div>
        <div className="text-center">
          {loginError &&
            <p className="text-danger text-center">{loginError}</p>
          }
          {/* <Modalwind2 /> */}
        </div>
      </div>
    );
  }
}

export default Login;
