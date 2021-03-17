import React, { Component } from 'react';
import { Redirect } from "react-router-dom";

class Logout extends Component {
    componentWillMount(){
        this.props.logout();
    }

    componentDidMount() {
        // this.props.logout();
    }

    render(){
        return(
            <div><Redirect to={{
                pathname: '/'
            }} /></div>
        );
    }
};

const mapStateToProps = (state) => {
    return {
      login: state.login
    };
  };
  
  const mapDispatchToProps = (dispatch) => {
    return {
      logout: () => {
        dispatch(logout());
      }
    }
  };
  
  export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Logout));