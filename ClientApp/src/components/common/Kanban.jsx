import { Box, Button, Typography, Divider, TextField, IconButton } from '@mui/material'
import { useEffect, useState } from 'react'
import { DragDropContext, Draggable, Droppable } from 'react-beautiful-dnd'
import AddOutlinedIcon from '@mui/icons-material/AddOutlined'
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined'
import assets from '../../assets/index'
import sectionApi from '../../api/sectionApi'

const Kanban = (props) => {
    const boardId = props.boardId
    const [data, setData] = useState([])

    useEffect( () => {
        setData(props.data)
    }, [props.data])

    const onDragEnd = () => {
        
    }

    const createSection = async () => {
        try {
            const section = await sectionApi.create(boardId)
            setData([...data, section])
        }
        catch (err) {
            alert(err.data.errors)
        }
    }

    const removeSection = async (sectionId) => {
        try {
            await sectionApi.remove(boardId, sectionId)
            const newData = [...data].filter(e => e.id != sectionId)
            setData(newData)
        }
        catch (err) {
            alert(err.data.errors)
        }
    }

    return (
        <>
            <Box sx={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'space-between'
            }}>
                <Button onClick={createSection}>
                    Add section
                </Button>
                <Typography variant='body2' fontWeight='7'>
                    {data.length} Sections 
                </Typography>
            </Box>
            <Divider sx={{ margin: '10px 0' }} />
            <DragDropContext onDragEnd={onDragEnd}>
                <Box sx={{
                    display: 'flex',
                    alignItems: 'flext-start',
                    width: 'calc(100vw - 400px)',
                    overflowX: 'auto'
                }}>
                    {
                        data.map((section) => (
                            <div key={section.id} style={{width: '300px'}}>
                                <Droppable key={section.id} droppableId={section.id.toString()}>
                                    {
                                        (provided) => (
                                            <Box
                                                ref={provided.innerRef}
                                                {...provided.droppableProps}
                                                sx={{
                                                    width: '300px',
                                                    padding: '10px',
                                                    marginRight: '10px'
                                                }}
                                            >
                                                <Box sx={{
                                                    display: 'flex',
                                                    alignItems: 'center',
                                                    justifyContent: 'space-between',
                                                    marginBottom: '10px'
                                                }}>
                                                    <TextField
                                                        value={section.name}
                                                        placeholder= {'Untitled'}
                                                        variant='outlined'
                                                        sx={{
                                                            flexGrow: 1,
                                                            '& .MuiOutlinedInput-input': {padding: 0},
                                                            '& .MuiOutlinedInput-notchedOutline': {border: 'unset'},
                                                            '& .MuiOutlinedInput-root': {fontSize: '1rem', fontWeight: '700'},
                                                        }}
                                                    />
                                                    <IconButton
                                                        variant='outlined'
                                                        size='small'
                                                        sx={{
                                                            color: 'gray',
                                                            '&:hover': {color: assets.colors.success}
                                                        }}
                                                    >
                                                        <AddOutlinedIcon/>
                                                    </IconButton>
                                                    <IconButton
                                                        variant='outlined'
                                                        size='small'
                                                        sx={{
                                                            color: 'gray',
                                                            '&:hover': {color: assets.colors.error}
                                                        }}
                                                        onClick={() => removeSection(section.id)}
                                                    >
                                                        <DeleteOutlinedIcon/>
                                                    </IconButton>
                                                </Box>
                                            </Box>
                                        )
                                    }
                                </Droppable>
                            </div>
                        ))
                    }
                </Box>
            </DragDropContext>
        </>
    )
}

export default Kanban