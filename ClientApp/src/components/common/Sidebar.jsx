import { Drawer, IconButton, Typography, List, ListItem, Box } from "@mui/material"
import { useSelector, useDispatch } from "react-redux"
import { useNavigate, useParams } from 'react-router-dom'
import LogoutOutlinedIcon from '@mui/icons-material/LogoutOutlined'
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined'
import assets from '../../assets/index'
import boardApi from "../../api/boardApi"
import { setBoards } from "../../redux/features/boardSlice"
import { useEffect } from "react"

const Sidebar = () => {
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
                alert(err)
            }
        }

        getBoards()
    }, [])

    useEffect(() => {
        console.log(boards)
    }, [boards])
    
    const logout = () => {
        localStorage.removeItem('token')
        navigate('/login')
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
                width: sidebarWidth,
                height: '100vh',
                backgroundColor: assets.colors.secondary
             }}
            >
                <ListItem>
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
                <Box sx={{
                    paddingTop: '10px'
                }}/>
                <ListItem>
                    <Box sx={{
                        width: '100%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'space-between'
                    }}>
                        <Typography variant='body2' fontWeight='700'>
                            Favourites
                        </Typography>
                    </Box>
                </ListItem>
                <Box sx={{
                    paddingTop: '10px'
                }}/>
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
                        <IconButton>
                            <AddBoxOutlinedIcon fontSize='small'/>
                        </IconButton>
                    </Box>
                </ListItem>
            </List>
        </Drawer>
    )
}

export default Sidebar