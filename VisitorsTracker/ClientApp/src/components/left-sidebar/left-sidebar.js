import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import HeaderProfileWrapper from '../header-profile/index';
import './left-sidebar.css';

const NavItem = ({ to, icon, text, my_icon }) => {
    return (
        <li className="sidebar-header">
            <Link to={to} className="active">
                <span className="link">
                    <i className={icon + ' nav-item-icon'}></i>
                    {my_icon}
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
                    <HeaderProfileWrapper />
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
                                        to={'/user/' + this.props.user.id}
                                        icon={'fa fa-user'}
                                        text={"Мій профіль"}
                                    />
                                    <NavItem
                                        to={'/search/users?page=1'}
                                        icon={'fa fa-users'}
                                        text={"Пошук користувача"}
                                    />
                                </>
                            }
                            {this.props.user.role === "Admin" &&
                                <>
                                    <NavItem
                                        to={'/admin/administration/'}
                                        icon={'fa fa-lock'}
                                        text={"Адміністрування"}
                                    />
                                    <NavItem
                                        to={'/admin/users?page=1'}
                                        icon={'fa fa-users'}
                                        text={"Users"}
                                    />
                                </>
                            }
                            {this.props.user.role === "User" &&
                                <>
                                    <NavItem
                                        to={'/contactUs'}
                                        icon={'fa fa-exclamation-circle'}
                                        text={'Зв\'яжіться з нами'}
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

export default LeftSidebar;
