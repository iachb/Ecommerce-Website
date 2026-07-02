export const httpParams = (obj) => {
    Object.keys(obj).forEach((k) => obj[k] == null && delete obj[k]);
    return obj;
};