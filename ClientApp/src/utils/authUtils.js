const authUtils = {
  isAuthenticated: () => {
    const token = localStorage.getItem("token");
    return !!token;
  },
};

export default authUtils;
