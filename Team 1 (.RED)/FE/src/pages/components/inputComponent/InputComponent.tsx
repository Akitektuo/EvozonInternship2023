import { Input } from "@mui/material";
import styles from "./InputComponent.module.scss";
import classNames from "classnames";

interface InputProps {
    pattern?: string;
    onChange: (value: string) => void;
    placeHolder?: string;
    value: string;
    type?: string;
    errorMessage?: string;
    required?: boolean;
}

export const InputComponent = ({ pattern, required, onChange, placeHolder, value, type, errorMessage }: InputProps) => <>
    <Input 
        inputProps={{pattern}} 
        onChange={event => onChange(event.target.value)}
        className={classNames(styles.inputControl, {
            [styles.invalidInput]: !!errorMessage
        })} 
        placeholder={placeHolder} 
        value={value} 
        type={type}
        required={required} />
        <div className={styles.errorBox}>{errorMessage}</div>
    </>;