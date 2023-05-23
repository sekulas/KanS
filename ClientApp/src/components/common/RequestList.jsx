import { Typography, ListItem, IconButton, Box } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import boardApi from "../../api/boardApi"
import { useEffect } from "react";
import { setRequestedList } from '../../redux/features/requestSlice';
import { setBoards } from "../../redux/features/boardSlice"
import LibraryAddOutlinedIcon from '@mui/icons-material/LibraryAddOutlined';
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined'
import { useTheme } from '@emotion/react';

const RequestedList = () => {
  const theme = useTheme()
  const dispatch = useDispatch();
  const requestedList = useSelector((state) => state.requested.value);
  const boardList = useSelector((state) => state.board.value)

  
  useEffect(() => {
    const getRequestedBoards = async () => {
      try {
        const res = await boardApi.getAllRequestedForUser();
        dispatch(setRequestedList(res));
      } catch (err) {
        if(err.data === undefined) {
            alert(err)
        } else {
            const errors = err.data.errors

            let errorMessage = ''
            
            for (const key in errors) {
                const errorArray = errors[key]
                errorMessage += errorArray
                errorMessage += '\n---\n'
            }
            
            alert(errorMessage)
        }
      }
    };

    getRequestedBoards();
  }, []);

  const acceptParticipation = async (boardId, decision) => {
    try {
        await boardApi.respondToRequest(boardId, {participatingAccepted: decision})
        const index = requestedList.findIndex(e => e.id == boardId)
        const acceptedBoard = requestedList[index]
        const newRequestedList = requestedList.filter(e => e.id != boardId);
        dispatch(setRequestedList(newRequestedList))
        dispatch(setBoards([...boardList, acceptedBoard]))
    }
    catch (err) {
        console.log(err)
        if(err.data === undefined) {
            alert(err)
        } else {
            const errors = err.data.errors

            let errorMessage = ''
            
            for (const key in errors) {
                const errorArray = errors[key]
                errorMessage += errorArray
                errorMessage += '\n---\n'
            }
            
            alert(errorMessage)
        }
    }
  }

  const denyParticipation = async (boardId, decision) => {
    try {
        await boardApi.respondToRequest(boardId, {participatingAccepted: decision})
        const newRequestedList = requestedList.filter(e => e.id != boardId);
        dispatch(setRequestedList(newRequestedList))
    }
    catch (err) {
        if(err.data === undefined) {
            alert(err)
        } else {
            const errors = err.data.errors

            let errorMessage = ''
            
            for (const key in errors) {
                const errorArray = errors[key]
                errorMessage += errorArray
                errorMessage += '\n---\n'
            }
            
            alert(errorMessage)
        }
    }
  }

  return (
    <>
        {requestedList.length > 0 && (
            <>
                <Box sx={{
                    paddingTop: '5px'
                }}/>
                <ListItem sx={{backgroundColor: theme.share.dark}}>
                    <Box
                    sx={{
                        width: "100%",
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "space-between",
                    }}
                    >
                    <Typography variant="body2" fontWeight="700">
                        Shared
                    </Typography>
                    </Box>
                </ListItem>
            </>
        )}
        {requestedList.length > 0 && requestedList.map((item) => (
            <ListItem
                key={item.id}
                sx={{
                    pl: "20px",
                    cursor: "pointer!important",
                    display: 'flex',
                    justifyContent: 'space-between',
                    borderLeft: `2px solid ${theme.share.dark}`,
                    borderRight: `2px solid ${theme.share.dark}`,
                    borderBottom: `2px solid ${theme.share.dark}`,
                }}
            >
            <Typography
                variant="body2"
                fontWeight="700"
                sx={{
                    whiteSpace: "nowrap",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                }}
            >
                {item.name}
            </Typography>
            <Box>
                <IconButton onClick={() => acceptParticipation(item.id, "true")}>
                    <LibraryAddOutlinedIcon fontSize='small'/>
                </IconButton>
                <IconButton onClick={() => denyParticipation(item.id, "false")}>
                    <DeleteOutlinedIcon fontSize='small'/>
                </IconButton>
            </Box>
            </ListItem>
        ))}
    </>
  )
}

export default RequestedList