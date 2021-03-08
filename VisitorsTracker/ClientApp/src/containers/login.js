import React, { Component } from 'react';
import Login from '../components/Login';

class LoginWrapper extends Component {
  submit = values => {
    this.props.login(values.email);
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

const mapStateToProps = state => {
  return {
    loginStatus: state.login
  }
};

const mapDispatchToProps = dispatch => {
  return {
    login: (email, password) => dispatch(login(email, password))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(LoginWrapper);
