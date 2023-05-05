import { useEffect, useState  } from 'react'
import { useParams } from 'react-router-dom'
import { Box, IconButton, TextField, Typography, Button, Divider } from '@mui/material'
import StarOutlinedIcon from '@mui/icons-material/StarOutlined'
import StarBorderOutlinedIcon from '@mui/icons-material/StarBorderOutlined'
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined'
import boardApi from '../api/boardApi'

const Board = () => {
    const { boardId } = useParams()
    const [name, setName] = useState('')
    const [description, setDescription] = useState('')
    const [sections, setSections] = useState([])
    const [isFavourite, setIsFavourite] = useState(false)

    useEffect(() => {

        const getBoard = async () => {
            try {
                const res = await boardApi.getOne(boardId)
                setName(res.name)
                setDescription(res.description)
                setSections(res.sections)
                setIsFavourite(res.favourite)
            } catch(err) {
                alert(err)
            }
        }

        getBoard()
    }, [boardId])

    return (
        <>
            <Box sx={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'space-between',
                width: '100%'
            }}>
                <IconButton variant='outlined'>
                {
                    isFavourite ? (
                        <StarOutlinedIcon color='warning'/>
                    ) : (
                        <StarBorderOutlinedIcon/>
                    )
                }
                </IconButton>
                <IconButton variant='outlined' color='error'>
                    <DeleteOutlinedIcon/>
                </IconButton>
            </Box>
            <Box sx={{ padding: '10px 50px' }}>
                <Box>
                    <TextField
                        value={name}
                        placeholder='New Board #?'
                        variant='outlined'
                        fullWidth
                        sx={{
                            '& .MuiOutlinedInput-input': {padding: 0},
                            '& .MuiOutlinedInput-notchedOutlined': {border: 'unset'},
                            '& .MuiOutlinedInput-root': {fontSize: '2rem', fontWeight: '700'},
                        }}
                    />
                    <TextField
                        value={description}
                        placeholder='Description goes here...'
                        variant='outlined'
                        multiline
                        fullWidth
                        sx={{
                            '& .MuiOutlinedInput-input': {padding: 0},
                            '& .MuiOutlinedInput-notchedOutlined': {border: 'unset'},
                            '& .MuiOutlinedInput-root': {fontSize: '0.8rem'},
                        }}
                    />
                </Box>
                <Box>
                    <Box sx={{
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'space-between'
                    }}>
                        <Button>
                            Add section
                        </Button>
                        <Typography variant='body2' fontWeight='7'>
                            {sections.length} Sections
                        </Typography>
                    </Box>
                    <Divider sx={{ margin: '10px 0'}}/>
                </Box>
            </Box>
        </>
    )
}

export default Board