import styles from "./AuthenticationButton.module.scss";

interface Props {
    text: string;
    onClick?: () => void;
}

export const AuthenticationButton = ({ text, onClick }: Props) => (
    <button
        onClick={onClick}
        className={styles.submitButton}>
        {text}
    </button>
);