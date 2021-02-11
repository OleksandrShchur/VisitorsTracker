import React, { Component } from 'react';
import _authenticate from '../../actions/authentication';

class Authentication extends Component {
    componentWillMount = () => {
        const { id, token } = this.props.match.params;
        this.props.auth({ userId: id, token: token })
    }

    render() {
        return (
            <div className="mt-5 b-inline-block">
                <div className='h3 text-center alert alert-success'>
                    Our congratulation, Your registration was successful!
                </div>
            </div>
        )
    }
}

export default Authentication;
