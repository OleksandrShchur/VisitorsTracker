import React, { Component } from 'react';
import 'moment-timezone';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import genders from '../../constants/genderConstants';
import CustomAvatar from '../Avatar/Avatar';
import './UserProfile.css';

export default class UserItemView extends Component {
    state = {
        value: 0
    };

    getAge = birthday => {
        let today = new Date();
        let birthDate = new Date(birthday);
        let age = today.getFullYear() - birthDate.getFullYear();
        let month = today.getMonth() - birthDate.getMonth();
        if (month < 0 || (month === 0 && today.getDate() < birthDate.getDate())) {
            age = age - 1;
        }
        if (age > 100) return '---';
        return age;
    }

    render() {
        const {
            userPhoto,
            name,
            email,
            birthday,
            gender,
            id
        } = this.props.data;

        const render_prop = (propName, value) => (
            <div className='row mb-3 font-weight-bold'>
                <div className='col-4'>{propName + ':'}</div>
                <div className='col-8'>
                    {value ? value : ''}
                </div>
            </div>
        )

        return <>
            <div className="info">
                {(id !== this.props.current_user)
                    ?
                    <div className="col-4 user">
                        <div className='d-flex flex-column justify-content-center align-items-center'>
                            <div className="user-profile-avatar">
                                <CustomAvatar size="big" name={name} photoUrl={userPhoto} />
                            </div>
                        </div>
                    </div>
                    : <div className="col-4"></div>
                }
                <div className='col-sm-12  col-md-6'>
                    {render_prop('User Name', name)}
                    {render_prop('Age', this.getAge(birthday))}
                    {render_prop('Gender', genders[gender])}
                    {render_prop('Email', email)}
                </div>
            </div>
        </>
    }
}