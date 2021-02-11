import React, { Component } from 'react';
import Login from '../components/Login';

class LoginWrapper extends Component {
  submit = values => {
    this.props.login(values.email, values.password);
  };

  render() {
    let { loginError } = this.props.loginStatus;

    return <>
      <div>
        <Login onSubmit={this.submit} loginError={loginError} />
      </div>
    </>
  }
}

export default LoginWrapper;
