import { userDetailsStore } from "../services/UserDetails.store";

export const getAuthorizationHeaders = () => {
    const userToken = userDetailsStore.userDetails?.token;
    const authorizationHeaders = { headers: {"Authorization": `Bearer ${userToken}`}};
    return authorizationHeaders;
}