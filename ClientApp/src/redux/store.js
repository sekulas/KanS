import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./features/userSlice";
import boardReducer from "./features/boardSlice";
import favouriteReducer from "./features/favouriteSlice";
import requestedReducer from "./features/requestSlice";

export const store = configureStore({
  reducer: {
    user: userReducer,
    board: boardReducer,
    favourites: favouriteReducer,
    requested: requestedReducer,
  },
});
