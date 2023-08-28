import { useContext } from "react";
import { Dialog, DialogContent, DialogContentText, DialogTitle, IconButton } from "@mui/material";
import { popUpNotificationContext } from "./PopUpNotifications.store";
import { observer } from "mobx-react";
import classNames from "classnames";
import styles from "./PopUpNotifications.module.scss";
import { CheckCircle, ErrorOutline, Close } from "@mui/icons-material";
import { MessageType } from "./PopUpNotifications.types";

export const PopUpNotification = observer(() => {
    const { messageInfo, resetMessage } = useContext(popUpNotificationContext);

    return (
        <Dialog className={classNames(styles.dialogBox, {
            [styles.isError]: messageInfo?.type === MessageType.Error,
        })}
            open={!!messageInfo?.message}
            onClose={resetMessage}>
            <DialogTitle className={styles.dialogTitle}>
                {messageInfo && (messageInfo.type === MessageType.Error ? (
                    <ErrorOutline className={styles.icon} />
                ) : (
                    <CheckCircle className={styles.icon} /> 
                ))}
                {messageInfo?.title}
                <IconButton onClick={resetMessage}><Close /></IconButton>
            </DialogTitle>
            <DialogContent className={styles.dialogContent}>
                <DialogContentText className={styles.dialogContentText}>
                    {messageInfo?.message}
                </DialogContentText>
                {Object.keys(messageInfo?.errorObject || {}).map((keyName, index) => (
                    <div key={index}>
                        <h3>{keyName}</h3>
                        <ul>
                            {messageInfo?.errorObject[keyName].map((errorString, index) => (
                                <li key={index}>{errorString}</li>
                            ))}
                        </ul>
                    </div>
                ))}
            </DialogContent>
        </Dialog> 
   );
});