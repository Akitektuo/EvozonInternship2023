import styles from "./UnauthorizedPage.module.scss";
import { Link } from "react-router-dom";

export const UnauthorizedPage = () => (
    <div className={styles.container}>
        <h1>Whoa there :)</h1>
        <p>You must be logged in to access this page. </p>
        <Link to={"/login-page"}>Go to login page</Link>
    </div>
);