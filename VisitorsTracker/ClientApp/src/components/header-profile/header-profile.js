import React, { Component } from "react";
import CustomAvatar from '../avatar/index';
import './header-profile.css';

export default class HeaderProfile extends Component {
    render() {
        const { id, name, photoUrl } = this.props.user;

        return (
            <div className='header-profile-root'>
                <div className='d-inline-block'>
                    {id && (
                        <div className="d-flex flex-column align-items-center">
                            <CustomAvatar size="big" photoUrl={photoUrl} name={this.props.user.name} />
                            <h4>{name}</h4>
                        </div>
                    )}
                </div>
            </div >
        );
    }
}
