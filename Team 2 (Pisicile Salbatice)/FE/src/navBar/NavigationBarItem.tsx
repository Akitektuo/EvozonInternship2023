import { NavLink } from "react-router-dom";
import classNames from "classnames";
import styles from "./NavigationBarItem.module.scss";

interface Props {
    text: string;
    url: string;
    onClick?: () => void;
}

export const NavigationBarItem = ({ text, url, onClick }: Props) => (
    <NavLink 
        to={url} 
        className={({ isActive }) => classNames(styles.navigationBarItem, {
            [styles.active]: isActive
        })}
        onClick={onClick}>
        {text}
    </NavLink>
);

export const NavigationBarItemLogout = ({ text, url, onClick }: Props) => (
    <NavLink 
        to={url} 
        className={styles.navigationBarItem}
        onClick={onClick}>
        {text}
    </NavLink>
);