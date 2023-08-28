import { useUserService } from "./userService/UseUserService";

export const useAppServices = () => {
    useUserService(true);
}