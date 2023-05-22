import { createSlice } from "@reduxjs/toolkit";

const initialState = { value: [] };

export const requestedSlice = createSlice({
  name: "requested",
  initialState,
  reducers: {
    setRequestedList: (state, action) => {
      state.value = action.payload;
    },
  },
});

export const { setRequestedList } = requestedSlice.actions;

export default requestedSlice.reducer;
