import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import HeaderProfileWrapper from '../Header-profile/index';
import Login from '../Login/index';

import './Left-sidebar.css';

const NavItem = ({ to, icon, text }) => {
    return (
        <li className="sidebar-header">
            <Link to={to} className="active">
                <span className="link">
                    <i className={icon + ' nav-item-icon'}></i>
                    <span className="nav-item-text">&nbsp;{text}</span>
                    <strong></strong>
                </span>
            </Link>
        </li>
    );
}

class LeftSidebar extends Component {
    constructor(props) {
        super(props);
        this.state = {
            _class: "left-sidebar-closed"
        };
    }

    render() {
        return (
            <>
                <div id='open-close-zone'
                    className={this.state._class + ' d-flex justify-content-start'}
                    onClick={() => {
                        this.state._class === "left-sidebar-opened"
                            ? this.setState({ _class: "left-sidebar-closed" })
                            : this.setState({ _class: "left-sidebar-opened" })
                    }}
                >
                    <button className="open-close-btn">
                        {this.state._class === "left-sidebar-opened" ? '×' : '☰'}
                    </button>
                </div>
                <div className={this.state._class + ' left-sidebar'}>
                    <div className="text-uppercase">
                        Visitors Tracker
                    </div>
                    <HeaderProfileWrapper />
                    {!this.props.user.id &&
                        <Login />}
                    <nav>
                        <hr />
                        <ul className="list-unstyled">
                            <NavItem
                                to={'/'}
                                icon={'fa fa-home'}
                                text={"Головна"}
                            />
                            {this.props.user.id &&
                                <>
                                    <NavItem
                                        to={'/profile'}
                                        icon={'fa fa-user'}
                                        text={"Профіль"}
                                    />
                                    <NavItem
                                        to={'/'}
                                        icon={'fa fa-users'}
                                        text={"Пошук користувачів"}
                                    />
                                    <NavItem
                                        to={'/'}
                                        icon={'fa fa-users'}
                                        text={"Розклад"}
                                    />
                                </>
                            }
                            {this.props.user.role === "Admin" &&
                                <>
                                    <NavItem
                                        to={'/'}
                                        icon={'fa fa-users'}
                                        text={"Адміністрування"}
                                    />
                                </>
                            }
                        </ul>
                    </nav>
                </div>
            </>
        );
    }
}

const mapStateToProps = (state) => ({
    user: state.user
});

const mapDispatchToProps = (dispatch) => {
}

export default connect(mapStateToProps, mapDispatchToProps)(LeftSidebar);
