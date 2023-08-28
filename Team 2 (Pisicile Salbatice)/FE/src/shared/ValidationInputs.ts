export const hasSpacesBeforeAndAfter = (field: string) => {
    const spacesBeforeAndAfterRegex = new RegExp(/^(?!\s)(?!.*\s$).+$/, "gm");
    return !spacesBeforeAndAfterRegex.test(field);
}

export const hasConsecutiveSpaces = (field: string) => {
    const consecutiveSpacesRegex = new RegExp(/^(?!.*\s\s).+$/, "gm");
    return !consecutiveSpacesRegex.test(field);
}


export const nameValidation = (name: string) => {
    const characterRegex = new RegExp(/^(?!.*\s{2,})[A-Za-z]+(?:[-'\s]?[A-Za-z]+)*$/, "gm");
    const isValidCharacterRegex = characterRegex.test(name);

    if (!name.length) {
        return "This field is required!";
    }

    if (hasSpacesBeforeAndAfter(name)) {
        return "Name should not start or end with whitespace characters!";
    }

    if (hasConsecutiveSpaces(name)) {
        return "Name should not contain consecutive whitespace characters!";
    }

    if (!isValidCharacterRegex) {
        return "Name should not contain non-alphabetic characters, digits or symbols!";
    }

    if (name.length >= 50) {
        return "Name must be shorter than 50 characters!";
    }

    return "";
};

export const emailValidation = (email: string) => {
    const emailRegex = new RegExp(/^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/, "gm");
    const isEmailValid = emailRegex.test(email);

    if (!email.length) {
        return "This field is required!";
    }

    if (email.length < 7) {
        return "Email must be at least 7 characters long!";
    }

    if (!isEmailValid) {
        return "Email does not fit the format!";
    }

    if (email.length >= 50) {
        return "Email must be shorter than 50 characters!";
    }

    return "";
};

export const passwordValidation = (password: string) => {
    if (!password.length) {
        return "This field is required!";
    }

    if (hasSpacesBeforeAndAfter(password)) {
        return "Password should not start or end with whitespace characters!";
    }

    if (hasConsecutiveSpaces(password)) {
        return "Password should not contain consecutive whitespace characters!";
    }

    if (password.length < 5) {
        return "Password must be at least 5 characters long!";
    }

    if (password.length >= 20) {
        return "Password must be shorter than 20 characters!";
    }

    return "";
};

export const confirmedPasswordValidation = (password: string, confirmedPassword: string) => {
    if (!confirmedPassword.length) {
        return "This field is required!";
    }

    if (confirmedPassword !== password) {
        return "The two passwords must be the same!";
    }

    return "";
}