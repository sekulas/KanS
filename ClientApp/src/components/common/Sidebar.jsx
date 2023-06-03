import { Drawer, IconButton, Typography, List, ListItem, Box } from "@mui/material"
import { useSelector, useDispatch } from "react-redux"
import { useNavigate, useParams, Link } from 'react-router-dom'
import LogoutOutlinedIcon from '@mui/icons-material/LogoutOutlined'
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined'
import ListItemButton from '@mui/material/ListItemButton';
import boardApi from "../../api/boardApi"
import { setBoards } from "../../redux/features/boardSlice"
import { useEffect } from "react"
import FavouriteList from "./FavouriteList"
import RequestedList from "./RequestList"
import { useTheme } from "@emotion/react"

const Sidebar = () => {
    const theme = useTheme()
    const user = useSelector( (state) => state.user.value )
    const boards = useSelector( (state) => state.board.value )
    const navigate = useNavigate()
    const dispatch = useDispatch()
    const { boardId } = useParams()
    const sidebarWidth = 250

    useEffect(() => {
        const getBoards = async () => {
            try {
                const res = await boardApi.getAllForUser()
                dispatch(setBoards(res))
                if(res.length > 0 && boardId === undefined) {
                    navigate(`/boards/${res[0].id}`)
                }
            } catch(err) {
                alert(err.data.errors)
            }
        }

        getBoards()
    }, [])

    const logout = () => {
        localStorage.removeItem('token')
        navigate('/login')
    }

    const addBoard = async () => {
        try {
            const res = await boardApi.create()
            const newList = [...boards, res]
            dispatch(setBoards(newList))
            navigate(`/boards/${res.id}`)
        } catch (err) {
            alert(err.data.errors)
        }
    }

    return (
        <Drawer
         container={window.document.body}
         variant='permanent'
         open={true}
         sx={{
            width: sidebarWidth,
            height: '100%',
            '& > div': { borderRight: 'none' }
         }}
        >
            <List
             disablePadding
             sx={{
                backgroundColor: theme.list.main,
                width: sidebarWidth,
                height: '100vh',
             }}
            >
                <ListItem sx={{backgroundColor: theme.card.light}}>
                    <Box sx={{
                        width: '100%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'space-between'
                    }}>
                        <Typography variant='body2' fontWeight='700'>
                            {user.name}
                        </Typography>
                        <IconButton onClick={logout}>
                            <LogoutOutlinedIcon fontSize='small'/>
                        </IconButton>
                    </Box>
                </ListItem>
                <RequestedList/>
                <FavouriteList/>
                <ListItem>
                    <Box sx={{
                        width: '100%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'space-between'
                    }}>
                        <Typography variant='body2' fontWeight='700'>
                            All Boards
                        </Typography>
                        <IconButton onClick={addBoard}>
                            <AddBoxOutlinedIcon fontSize='small'/>
                        </IconButton>
                    </Box>
                </ListItem>
                {boards.map((item) => (
                    <ListItemButton
                        key={item.id}
                        selected={item.id == boardId}
                        component={Link}
                        to={`/boards/${item.id}`}
                        sx={{
                            pl: '20px',
                            cursor: 'pointer!important',
                            '&.Mui-selected': {
                                backgroundColor: theme.list.selected,
                            },
                        }}
                    >
                        <Typography
                            variant='body2'
                            fontWeight='700'
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
            </List>
        </Drawer>
    )
}

export default Sidebar