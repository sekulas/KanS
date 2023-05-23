import { useEffect, useState  } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { useSelector, useDispatch } from 'react-redux'
import { setFavouriteList } from "../redux/features/favouriteSlice";
import { setBoards } from "../redux/features/boardSlice";
import { Box, IconButton, TextField, Modal, Button} from '@mui/material'
import StarOutlinedIcon from '@mui/icons-material/StarOutlined'
import StarBorderOutlinedIcon from '@mui/icons-material/StarBorderOutlined'
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined'
import ShareIcon from '@mui/icons-material/Share';
import boardApi from '../api/boardApi'
import Kanban from '../components/common/Kanban'
import { useTheme } from '@emotion/react';

const Board = () => {
    const theme = useTheme()
    const { boardId } = useParams()
    const dispatch = useDispatch()
    const navigate = useNavigate()
    const [shareModalOpen, setShareModalOpen] = useState(false)
    const [emailErrText, setEmailErrText] = useState('')
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

    const changeBoardName = async (e) => {
        const newName = e.target.value
        setName(newName)
        /* Live Board Name Change on Sidebar TODO
        let newBoards = [...boards];
        const index = newBoards.findIndex((e) => e.id === boardId);
        newBoards[index] = { ...newBoards[index], name: newName };
        dispatch(setBoards(newBoards)); // Update the boards array in Redux
        */
    }

    const updateBoardName = async (e) => {
        try {
            await boardApi.update(boardId, {name: e.target.value})
        }
        catch (err) {
            alert(err.data.errors)
        }
    }

    const changeBoardDesc = async (e) => {
        const newDesc = e.target.value
        setDescription(newDesc)
    }

    const updateBoardDesc = async (e) => {
        try {
            await boardApi.update(boardId, {description: e.target.value})
        }
        catch (err) {
            alert(err.data.errors)
        }
    }

    const handleOpen = () => {
        setShareModalOpen(true);
    };
    const handleClose = () => {
        setShareModalOpen(false);
    };

    const handleSubmit = async (e) => {
        e.preventDefault()
        setEmailErrText('')

        const data = new FormData(e.target)
        const email = data.get('email').trim()

        let err = false
        
        if( email === '' ) {
            err = true
            setEmailErrText('Please fill this field')
        }

        if( err ) {
            return
        }

        try {
            await boardApi.requestForParticipation(boardId, {email: email})
            handleClose()
        }
        catch (err) {
            if(err.data === undefined) {
                setEmailErrText(err)
            } else {
                const errors = err.data.errors

                let errorMessage = ''
                
                for (const key in errors) {
                    const errorArray = errors[key]
                    errorMessage += errorArray
                    errorMessage += '\n'
                }
                
                setEmailErrText(errorMessage)
            }
        }

    }

    const style = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        bgcolor: 'background.paper',
        border: `2px solid ${theme.button.share}`,
        pt: 2,
        px: 4,
        pb: 3,
    };

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
                <Box>
                <IconButton variant='outlined' onClick={handleOpen} sx={{color: theme.button.share}}>
                    <ShareIcon/>
                </IconButton>
                <Modal
                    open={shareModalOpen}
                    onClose={handleClose}
                    aria-labelledby="modal-title"
                    aria-describedby="modal-description"
                >
                    <Box 
                        component='form'
                        onSubmit={handleSubmit}
                        noValidate
                        sx={{ ...style, width: '25%' }}
                    >
                        <h2>Sharing</h2>
                        <p>
                            Fill empty field with the email of person you want to share board with.
                        </p>
                        <TextField
                            margin='normal'
                            required
                            fullWidth
                            id='email'
                            label='Email'
                            name='email'
                            type='email'
                            error={emailErrText !== ''}
                            helperText={emailErrText}
                        />
                        <Button
                            sx={{mt:3, mb:2}}
                            variant='outlined'
                            fullWidth
                            type='submit'
                        >
                            Send Request
                        </Button>
                    </Box>
                </Modal>
                <IconButton variant='outlined' color='error' onClick={removeBoard}>
                    <DeleteOutlinedIcon/>
                </IconButton>
                </Box>
            </Box>
            <Box sx={{ padding: '10px 50px' }}>
                <Box>
                    <TextField
                        value={name}
                        onChange={(e) => changeBoardName(e)}
                        onBlur={(e) => updateBoardName(e)}
                        placeholder='Untitled'
                        variant='outlined'
                        fullWidth
                        sx={{
                            '& .MuiOutlinedInput-input': {padding: 0},
                            '& .MuiOutlinedInput-notchedOutline': {border: 'unset'},
                            '& .MuiOutlinedInput-root': {fontSize: '2rem', fontWeight: '700'},
                        }}
                    />
                    <TextField
                        value={description}
                        onChange={(e) => changeBoardDesc(e)}
                        onBlur={(e) => updateBoardDesc(e)}
                        placeholder='Description goes here...'
                        variant='outlined'
                        multiline
                        fullWidth
                        sx={{
                            '& .MuiOutlinedInput-input': {padding: 0},
                            '& .MuiOutlinedInput-notchedOutline': {border: 'unset'},
                            '& .MuiOutlinedInput-root': {fontSize: '0.8rem'},
                        }}
                    />
                </Box>
                <Box>
                    <Kanban sections={sections} boardId={boardId}/>
                </Box>
            </Box>
        </>
    )
}

export default Board