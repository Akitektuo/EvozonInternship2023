import { Box, CircularProgress } from "@mui/material";
import styles from "./LoadingPage.module.scss";

export const LoadingPage = () => (
    <Box className={styles.container}>
        <CircularProgress />
        <p>Loading ... </p>
    </Box>
);