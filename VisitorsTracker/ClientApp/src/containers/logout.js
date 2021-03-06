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

  
  export default Logout;