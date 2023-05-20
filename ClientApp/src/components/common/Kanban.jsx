import { Box, Button, Typography, Divider, TextField, IconButton, Card } from '@mui/material'
import { useEffect, useState } from 'react'
import { DragDropContext, Draggable, Droppable } from 'react-beautiful-dnd'
import AddOutlinedIcon from '@mui/icons-material/AddOutlined'
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined'
import assets from '../../assets/index'
import sectionApi from '../../api/sectionApi'
import taskApi from '../../api/taskApi'

const Kanban = (props) => {
    const boardId = props.boardId
    const [sections, setSections] = useState([])

    useEffect( () => {
        setSections(props.sections)
    }, [props.sections])

    const onDragEnd = async ({source, destination}) => {
        console.log(source)
        console.log(destination)
        if(destination == null){
            return
        }
        const sourceColIndex = sections.findIndex(e => e.id == source.droppableId)
        const destinationColIndex = sections.findIndex(e => e.id == destination.droppableId)
        const sourceCol = sections[sourceColIndex]
        const destinationCol = sections[destinationColIndex]

        const taskId = sourceCol.tasks[source.index].id;

        const destinationSectionId = destinationCol.id

        const sourceTasks = [...sourceCol.tasks]
        const destinationTasks = [...destinationCol.tasks]

            // changing section of task
        if(source.droppableId !== destination.droppableId) {
            const [removed] = sourceTasks.splice(source.index, 1) // removing element from source column
            destinationTasks.splice(destination.index, 0, removed) // inserting element to dest column
            sections[sourceColIndex].tasks = sourceTasks
            sections[destinationColIndex].tasks = destinationTasks
        }
        else { // changing position of task in same column
            const [removed] = destinationTasks.splice(source.index, 1) 
            destinationTasks.splice(destination.index, 0, removed)  
            sections[destinationColIndex].tasks = destinationTasks
        }

        try {
            await taskApi.update(boardId, taskId, {
                sectionId: destinationSectionId,
                position: destination.index
            })
            setSections(sections)
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

    const createSection = async () => {
        try {
            const section = await sectionApi.create(boardId)
            setSections([...sections, section])
        }
        catch (err) {
            if(err.data === undefined) {
                alert(err)
            } else {
                alert(err.data.errors)
            }
        }
    }

    const removeSection = async (sectionId) => {
        try {
            await sectionApi.remove(boardId, sectionId)
            const newSections = [...sections].filter(e => e.id != sectionId)
            setSections(newSections)
        }
        catch (err) {
            if(err.data === undefined) {
                alert(err)
            } else {
                alert(err.data.errors)
            }
        }
    }

    const changeSectionName = async (e, sectionId) => {
        const newName = e.target.value
        const newSections = [...sections]
        const index = newSections.findIndex(e => e.id == sectionId)
        newSections[index].name = newName
        setSections(newSections)
    }

    const updateSectionName = async (e, sectionId) => {
        try {
            await sectionApi.update(boardId, sectionId, {boardId: boardId, name: e.target.value})
        }
        catch (err) {
            alert(err.data.errors)
        }
    }

    const createTask = async (sectionId) => {
        try {
            const task = await taskApi.create(boardId, {sectionId: sectionId})
            const newSections = [...sections]
            const index = newSections.findIndex(e => e.id === sectionId)
            newSections[index].tasks.unshift(task)
            setSections(newSections)
        }
        catch (err) {
            if(err.data === undefined) {
                alert(err)
            } else {
                alert(err.data.errors)
            }
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
                    {sections.length} Sections 
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
                        sections.map((section, index) => (
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
                                                {provided.placeholder}
                                                <Box sx={{
                                                    display: 'flex',
                                                    alignItems: 'center',
                                                    justifyContent: 'space-between',
                                                    marginBottom: '10px'
                                                }}>
                                                    <TextField
                                                        value={section.name}
                                                        onChange={(e) => changeSectionName(e, section.id)}
                                                        onBlur={(e) => updateSectionName(e, section.id)}
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
                                                        onClick={() => createTask(section.id)}
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
                                                {
                                                section.tasks.map((task, index) => (
                                                    <Draggable key={task.id} draggableId={task.id.toString()} index={index}>
                                                    {(provided, snapshot) => (
                                                        <Card
                                                        ref={provided.innerRef}
                                                        {...provided.draggableProps}
                                                        {...provided.dragHandleProps}
                                                        sx={{
                                                            padding: '10px',
                                                            marginBottom: '10px',
                                                            cursor: snapshot.isDragging ? 'grab' : 'pointer!important'
                                                        }}
                                                        >
                                                        <Typography>
                                                            {task.name === '' ? `New Task #${task.id}` : task.name}
                                                        </Typography>
                                                        </Card>
                                                    )}
                                                    </Draggable>
                                                ))                    
                                                }
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

export default Kanban;