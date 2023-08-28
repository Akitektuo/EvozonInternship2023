import { PropsWithChildren } from "react";
import { AuthenticationHeader } from "../authenticationHeader/AuthenticationHeader";
import styles from "./AuthenticationContainer.module.scss";

interface Props {
    title: string;
}

export const AuthenticationContainer = ({ title, children }: PropsWithChildren<Props>) => (
    <div className={styles.pageContainer}>
        <div className={styles.authenticationContainer}>
            <AuthenticationHeader title={title} />
            {children}
        </div>
    </div>
);