import VisitorsTrackerService from './VisitorsTrackerService'

const baseService = new VisitorsTrackerService();

export default class UserService {

    getUserById = async (id) => {
        const res = await baseService.getResource(`users/GetUserProfileById?id=${id}`);
        return res;
    }

    setContactUs = async (data) => {
        const res = await baseService.setResource('users/ContactAdmins', data);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setAvatar = async (data) => {
        let file = new FormData();
        file.append('newAva', data.image.file);
        const res = await baseService.setResourceWithData('users/changeAvatar', file);
        return !res.ok
            ? { error: await res.text() }
            : await res.text();
    }

    setBirthday = async (data) => {
        const res = await baseService.setResource('Users/EditBirthday', {
            birthday: new Date(data.Birthday)
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setGender = async (data) => {
        const res = await baseService.setResource('Users/EditGender', {
            gender: Number(data.Gender)
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}