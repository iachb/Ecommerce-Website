export const httpParams = (obj) => {
    Object.keys(obj).forEach((k) => obj[k] === undefined && delete obj[k]);
    return obj;
};