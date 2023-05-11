import { useEffect, useState  } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { useSelector, useDispatch } from 'react-redux'
import { setFavouriteList } from "../redux/features/favouriteSlice";
import { setBoards } from "../redux/features/boardSlice";
import { Box, IconButton, TextField, Typography, Button, Divider } from '@mui/material'
import StarOutlinedIcon from '@mui/icons-material/StarOutlined'
import StarBorderOutlinedIcon from '@mui/icons-material/StarBorderOutlined'
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined'
import boardApi from '../api/boardApi'
import Kanban from '../components/common/Kanban'

const Board = () => {
    const { boardId } = useParams()
    const dispatch = useDispatch()
    const navigate = useNavigate()
    const [name, setName] = useState('')
    const [description, setDescription] = useState('')
    const [sections, setSections] = useState([])
    const [isFavourite, setIsFavourite] = useState(false)

    const boards = useSelector( (state) => state.board.value )
    const favouriteList = useSelector( (state) => state.favourites.value )

    useEffect(() => {

        const getBoard = async () => {
            try {
                const res = await boardApi.getOne(boardId)
                setName(res.name)
                setDescription(res.description)
                setSections(res.sections)
                setIsFavourite(res.favourite)
            } catch(err) {
                alert(err.data.errors + "\n\nNavigating to existing board.")
                navigate(`/boards/${boards[0].id}`)
            }
        }

        getBoard()
    }, [boardId])

    const addFavourite = async () => {
        try {
            await boardApi.update(boardId, {favourite: !isFavourite})
            setIsFavourite(!isFavourite)
            const res = await boardApi.getAllFavouritesForUser();
            dispatch(setFavouriteList(res));
        } catch(err) {
            alert(err.data.errors)
        }
    }

    const removeBoard = async () => {
        try {
            await boardApi.remove(boardId)
            
            if(isFavourite) {
                const newFavouriteList = favouriteList.filter(e => e.id != boardId)
                dispatch(setFavouriteList(newFavouriteList))
            }

            const newList = boards.filter(e => e.id != boardId)

            if(newList.length == 0) {
                navigate('/')
            }
            else {
                navigate(`/boards/${newList[0].id}`)
            }

            dispatch(setBoards(newList))
            
        } catch(err) {
            alert(err.data.errors)
        }
    }

    return (
        <>
            <Box sx={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'space-between',
                width: '100%'
            }}>
                <IconButton variant='outlined' onClick={addFavourite}>
                {
                    isFavourite ? (
                        <StarOutlinedIcon color='warning'/>
                    ) : (
                        <StarBorderOutlinedIcon/>
                    )
                }
                </IconButton>
                <IconButton variant='outlined' color='error' onClick={removeBoard}>
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
                    <Kanban data={sections} boardId={boardId}/>
                </Box>
            </Box>
        </>
    )
}

export default Board