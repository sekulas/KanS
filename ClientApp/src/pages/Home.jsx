import { Box } from '@mui/material'
import LoadingButton from '@mui/lab/LoadingButton'

const Home = () => {

    const createBoard = () => {

    }

    return (
        <Box sx={{
            height: '100vh',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center'
        }}>
            <LoadingButton
                variant='outlined'
                color='success'
                onClick={createBoard}
            >
                Create your first board!
            </LoadingButton>
        </Box>
    )
}

export default Home