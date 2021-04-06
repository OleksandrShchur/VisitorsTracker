import React, { Component } from "react";
import { Link } from 'react-router-dom';
import IconButton from "@material-ui/core/IconButton";
import Tooltip from '@material-ui/core/Tooltip';
import Zoom from '@material-ui/core/Zoom';
import CustomAvatar from '../Avatar/Avatar';
import './Header-profile.css';
import { connect } from 'react-redux';
import webRootPath from '../../constants/projectConstants';

class HeaderProfile extends Component {
    render() {
        const { id, name, photoUrl } = this.props.user;

        return (
            <div className='header-profile-root'>
                <div className='d-inline-block'>
                    {id && (
                        <div className="d-flex flex-column align-items-center">
                            <CustomAvatar size="big" photoUrl={webRootPath + photoUrl} name={name} />
                            <h4>{name}</h4>
                            <div>
                                <Link to={'/profile'}>
                                    <Tooltip title="Edit your profile" placement="bottom" TransitionComponent={Zoom}>
                                        <IconButton>
                                            <i className="fa fa-cog" aria-hidden="true"></i>
                                        </IconButton>
                                    </Tooltip>
                                </Link>
                            </div>
                        </div>
                    )}
                </div>
            </div >
        );
    }
}

const mapStateToProps = (state) => ({
    user: state.user
});

const mapDispatchToProps = (dispatch) => {
}

export default connect(mapStateToProps, mapDispatchToProps)(HeaderProfile);
