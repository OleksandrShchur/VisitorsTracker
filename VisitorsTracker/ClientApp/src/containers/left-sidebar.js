import React, { Component } from 'react';
import { connect } from 'react-redux';
import LeftSidebar from '../components/left-sidebar';

class LeftSidebarWrapper extends Component {
    render() {
        return <LeftSidebar
            user={this.props.user}
        />;
    }
}

const mapStateToProps = state => ({
    user: state.user,
});

export default connect(
    mapStateToProps
)(LeftSidebarWrapper);
