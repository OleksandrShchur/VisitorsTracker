import React, { Component } from "react";
import Avatar from '@material-ui/core/Avatar';

export default class CustomAvatar extends Component {
    render() {

        //const  { photoUrl, name }  = this.props;
        let photoUrl = "qwerty";
        let name = "Visitors Tracker";
        
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
                        //src={icon}
                        className={size}
                    />
                    : <Avatar className={size}>
                        <div className={`text-light`}>
                            {name.charAt(0).toUpperCase()}
                        </div>
                    </Avatar>}
            </>
        );
    }
}