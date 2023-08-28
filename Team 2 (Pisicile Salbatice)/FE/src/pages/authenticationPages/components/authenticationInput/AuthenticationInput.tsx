import classNames from "classnames";
import styles from "./AuthenticationInput.module.scss";

interface Props {
    labelText: string;
    type: string;
    value: string;
    onChange: (value: string) => void;
    onFocus: () => boolean;
    errorMessage: string;
    isValid: boolean;
}

export const AuthenticationInput = ({ 
    labelText, 
    type,
    value,
    onChange, 
    onFocus, 
    errorMessage,
    isValid
}: Props) => (
    <div className={styles.inputContainer}>
        <input
            type={type}
            value={value}
            onChange={event => onChange(event.target.value)}
            onFocus={onFocus}
            className={classNames(styles.input, {
                [styles.inputValid]: isValid,
                [styles.inputError]: !isValid
            })}
            placeholder={labelText} />
        <p className={styles.errorMessage}>{errorMessage}</p>
    </div>
);