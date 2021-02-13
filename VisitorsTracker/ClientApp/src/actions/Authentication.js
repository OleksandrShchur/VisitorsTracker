import AuthenticationService from '../services/authenticationService';

const api_serv = new AuthenticationService();

export default function loginGoogle(tokenId, email, name, imageUrl) {
        api_serv.setGoogleLogin({
        TokenId: tokenId,
        Email: email,
        Name: name,
        PhotoUrl: imageUrl
    });
}