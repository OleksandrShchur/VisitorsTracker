import React, { Component } from "react";
import Avatar from '@material-ui/core/Avatar';
import { connect } from 'react-redux';

class CustomAvatar extends Component {
    render() {
        const  { photoUrl, name }  = this.props;
        
        let size = `${this.props.size}Avatar`;
        
        let firstLetterSize = (this.props.size === 'big') 
            ? 'display-1' 
            : (this.props.size === 'little') 
                ? 'display-4' 
                : '';

        return (
            <>  
                {photoUrl
                    ? <Avatar
                        src={photoUrl}
                        className={size}
                    />
                    : <Avatar className={size}>
                        <div className={`${firstLetterSize} text-light`}>
                            {name.charAt(0).toUpperCase()}
                        </div>
                    </Avatar>}
            </>
        );
    }
}

const mapStateToProps = (state) => ({
    user: state.user
});

const mapDispatchToProps = (dispatch) => {
}

export default connect(mapStateToProps, mapDispatchToProps)(CustomAvatar);