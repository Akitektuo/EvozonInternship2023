declare global {
    interface Array<T> {
        remove: (element: T) => void;
        sumBy: (selector: (element: T) => number) => number;
    }
}

if (!Array.prototype.remove) {
    Array.prototype.remove = function (element) {
        const elementIndex = this.indexOf(element);
        this.splice(elementIndex, 1);
    }
}

if (!Array.prototype.sumBy) {
    Array.prototype.sumBy = function (selector) {
        let sum = 0;
        for (const element of this) {
            sum += selector(element);
        }
        return sum;
    }
}

export {};