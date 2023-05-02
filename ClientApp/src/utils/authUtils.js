import jwt_decode from "jwt-decode";

const baseURL = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";

const authUtils = {
  isAuthenticated: () => {
    const token = localStorage.getItem("token");

    if (token) {
      const decodedToken = jwt_decode(token);

      const user = {
        name: decodedToken[baseURL + "name"],
        email: decodedToken[baseURL + "emailaddress"],
      };
      return user;
    }

    return false;
  },
};

export default authUtils;
