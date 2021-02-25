import VisitorsTrackerService from './VisitorsTrackerService'

const baseService = new VisitorsTrackerService();

export default class AuthenticationService {

    auth = async (data) => {
        console.log("auth service method");
        console.log(data);
        const res = await baseService.setResource(`Authentication/verify/${data.userId}/${data.token}`);
        return !res.ok
            ? { error: await res.text() }
            : res.json();
    }

    setGoogleLogin = async (data) => {
        console.log("setGoogleLogin service method");
        console.log(data);
        const res = await baseService.setResource('Authentication/GoogleLogin', data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setRegister = async (data) => {
        const res = await baseService.setResource('Authentication/register', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}