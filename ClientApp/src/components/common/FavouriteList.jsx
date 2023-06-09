import { Typography, ListItem, ListItemButton, Box } from "@mui/material";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { setFavouriteList } from "../../redux/features/favouriteSlice";
import { Link, useParams } from "react-router-dom";
import boardApi from "../../api/boardApi"
import { useTheme } from "@emotion/react";

const FavouriteList = () => {
  const theme = useTheme()
  const dispatch = useDispatch();
  const list = useSelector((state) => state.favourites.value);
  const { boardId } = useParams();

  useEffect(() => {
    const getFavouriteBoards = async () => {
      try {
        const res = await boardApi.getAllFavouritesForUser();
        dispatch(setFavouriteList(res));
      } catch (err) {
        alert(err.data.errors);
      }
    };

    getFavouriteBoards();
  }, [boardId]);

  return (
    <>
      <Box sx={{
        paddingTop: '5px'
      }}/>
      <ListItem sx={{backgroundColor: theme.list.favourite}}>
        <Box
          sx={{
            width: "100%",
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
          }}
        >
          <Typography variant="body2" fontWeight="700">
            Favourites
          </Typography>
        </Box>
      </ListItem>
      {list.map((item) => (
        <ListItemButton
          key={item.id}
          selected={item.id == boardId}
          component={Link}
          to={`/boards/${item.id}`}
          sx={{
            pl: "20px",
            cursor: "pointer!important",
            '&.Mui-selected': {
              backgroundColor: theme.list.selected, // Set your desired selected color
            },
            borderLeft: `2px solid ${theme.list.favourite}`,
            borderRight: `2px solid ${theme.list.favourite}`,
            borderBottom: `2px solid ${theme.list.favourite}`,
          }}
        >
          <Typography
            variant="body2"
            fontWeight="700"
            sx={{
              whiteSpace: "pre-wrap",
              overflowWrap: "break-word",
              overflow: "scroll",
            }}
          >
            {item.name}
          </Typography>
        </ListItemButton>
      ))}
    </>
  );
};

export default FavouriteList;
