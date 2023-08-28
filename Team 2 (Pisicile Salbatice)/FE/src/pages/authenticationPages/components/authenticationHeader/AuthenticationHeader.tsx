import styles from "./AuthenticationHeader.module.scss";

interface Props {
    title: string;
}

export const AuthenticationHeader = ({ title }: Props) => (
    <h1 className={styles.authenticationTitle}>{title}</h1>
);