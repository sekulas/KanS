import { Box, Button, Typography, Divider, TextField, IconButton } from '@mui/material'
import { useEffect, useState } from 'react'
import { DragDropContext, Draggable, Droppable } from 'react-beautiful-dnd'
import AddOutlinedIcon from '@mui/icons-material/AddOutlined'
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined'
import assets from '../../assets/index'

const Kanban = (props) => {
    const boardId = props.boardId
    const [data, setData] = useState([])

    useEffect( () => {
        setData(props.data)
    }, [props.data])

    const onDragEnd = () => {
        
    }

    return (
        <>
            <Box sx={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'space-between'
            }}>
                <Button>
                    Add section
                </Button>
                <Typography variant='body2' fontWeight='7'>
                    {/* {sections.length} Sections */}
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
                        data.map((section, index) => (
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
                                                        placeholder= {`New Section #${index}`}
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