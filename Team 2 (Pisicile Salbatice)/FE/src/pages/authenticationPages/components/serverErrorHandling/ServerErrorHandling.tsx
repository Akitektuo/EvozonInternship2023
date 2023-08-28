import { useContext } from "react";
import { observer } from "mobx-react";
import { ServerErrorHandlingContext } from "./ServerErrorHandling.store";
import { List, Dialog, Typography, ListItem } from "@mui/material";
import styles from "./ServerErrorHandling.module.scss";

export const ServerErrorHandling = observer(() => {
    const { 
        serverErrors, 
        setPopUpIsOpened, 
        isPopUpOpened, 
        serverErrorMessage,
        resetErrors
    } = useContext(ServerErrorHandlingContext);

    const handleDialog = () => {
        setPopUpIsOpened(false);
        resetErrors();
    }

    return (
        <Dialog 
            onClose={handleDialog} 
            open={isPopUpOpened}>  
            <div className={styles.popUpContainer}>
                <Typography className={styles.genericErrorMessage}>{serverErrorMessage}</Typography>
                {Object.entries(serverErrors).map(([errorTitle, errorMessages], index) => (
                    <List key={index}>
                        <Typography className={styles.titleError}>{errorTitle}:</Typography>
                        <List className={styles.containerMessage}>
                            {errorMessages.map((error, errorIndex) => (
                                <ListItem key={errorIndex} className={styles.textMessage}>{error}</ListItem>
                            ))}
                        </List>
                    </List>
                ))}
            </div>
        </Dialog>
    );
});