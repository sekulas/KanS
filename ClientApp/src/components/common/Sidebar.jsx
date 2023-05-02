import { Drawer, IconButton, Typography, List, ListItemButton, Box } from "@mui/material"
import { useSelector, useDispatch } from "react-redux"
import LogoutOutlinedIcon from '@mui/icons-material/LogoutOutlined'
import assets from '../../assets/index'

const Sidebar = () => {
    const user = useSelector( (state) => state.user.value )
    const sidebarWidth = 250

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
                <ListItemButton>
                    <Box sx={{
                        width: '100%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'space-between'
                    }}>
                        <Typography variant='body2' fontWeight='700'>
                            {user.name}
                        </Typography>
                        <IconButton>
                            <LogoutOutlinedIcon fontSize='small'/>
                        </IconButton>
                    </Box>
                </ListItemButton>
            </List>
        </Drawer>
    )
}

export default Sidebar