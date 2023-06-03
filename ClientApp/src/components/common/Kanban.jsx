import { Box, Button, Typography, Divider, TextField, IconButton, Card } from '@mui/material'
import { useEffect, useState } from 'react'
import { DragDropContext, Draggable, Droppable } from 'react-beautiful-dnd'
import AddOutlinedIcon from '@mui/icons-material/AddOutlined'
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined'
import sectionApi from '../../api/sectionApi'
import taskApi from '../../api/taskApi'
import { useTheme } from '@mui/material/styles';

const Kanban = (props) => {
    const theme = useTheme();
    const boardId = props.boardId
    const [sections, setSections] = useState([])

    useEffect( () => {
        setSections(props.sections)
    }, [props.sections])

    const onDragEnd = async ({source, destination}) => {
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

        const didSectionChange = source.droppableId !== destination.droppableId ? true : false

        const newSections = [...sections]
            // changing section of task
        if(didSectionChange) {
            const [removed] = sourceTasks.splice(source.index, 1) // removing element from source column
            sourceTasks.forEach(t => t.position > removed.position ? t.position-- : t.position)
            removed.position = destination.index;
            destinationTasks.forEach(t => t.position >= destination.index ? t.position++ : t.position)
            destinationTasks.splice(destination.index, 0, removed) // inserting element to dest column
            newSections[sourceColIndex].tasks = sourceTasks
        }
        else { // changing position of task in same column
            const [removed] = destinationTasks.splice(source.index, 1)
            if( destination.index > source.index) {
                destinationTasks.forEach(t => t.position > source.index && t.position <= destination.index ? t.position-- : t.position)
            }
            else if ( destination.index < source.index) {
                destinationTasks.forEach(t => t.position < source.index && t.position >= destination.index ? t.position++ : t.position)
            }
            removed.position = destination.index;
            destinationTasks.splice(destination.index, 0, removed)  
        }

        newSections[destinationColIndex].tasks = destinationTasks

        try {
            await taskApi.update(boardId, taskId, {
                sectionId: destinationSectionId,
                position: destination.index
            })
            setSections(newSections)
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
            const newSectionIndex = newSections.findIndex(e => e.id === sectionId)
            newSections[newSectionIndex].tasks.forEach(taskInSection => taskInSection.position++);
            newSections[newSectionIndex].tasks.unshift(task)
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

    const removeTask = async (sectionId, taskId) => {
        try {
            await taskApi.remove(boardId, taskId)
            const newSections = [...sections]
            const newSectionIndex = newSections.findIndex(e => e.id === sectionId)
            const removedTaskIndex = newSections[newSectionIndex].tasks.findIndex(e => e.id === taskId)
            const removedTaskPosition = newSections[newSectionIndex].tasks[removedTaskIndex].position
            newSections[newSectionIndex].tasks.forEach(t => t.position > removedTaskPosition ? t.position-- : t.position)
            newSections[newSectionIndex].tasks = newSections[newSectionIndex].tasks.filter(e => e.id != taskId)
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

    const changeTaskName = async (e, sectionId, taskId) => {
        const newName = e.target.value
        const newSections = [...sections]
        const sectionIndex = newSections.findIndex(e => e.id == sectionId)
        const taskIndex = newSections[sectionIndex].tasks.findIndex(e => e.id == taskId)    
        newSections[sectionIndex].tasks[taskIndex].name = newName    
        setSections(newSections)
    }

    const updateTaskName = async (e, taskId) => {
        try {
            await taskApi.update(boardId, taskId, {name: e.target.value})
        }
        catch (err) {
            alert(err.data.errors)
        }
    }
    return (
        <>
          <Box
            sx={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'space-between'
            }}
          >
            <Button onClick={createSection}>Add section</Button>
            <Typography variant="body2" fontWeight="7">
              {sections.length} Sections
            </Typography>
          </Box>
          <Divider sx={{ margin: '10px 0' }} />
          <DragDropContext onDragEnd={onDragEnd}>
            <Box
              sx={{
                display: 'flex',
                alignItems: 'flex-start',
                width: 'calc(100vw - 400px)',
                overflowX: 'auto'
              }}
            >
              {sections.map((section, index) => (
                <div key={section.id} style={{ width: '300px' }}>
                  <Box
                    sx={{
                      width: '300px',
                      padding: '5px',

                    }}
                  >
                    <Box
                      sx={{
                        display: 'flex',
                        alignItems: 'start',
                        justifyContent: 'space-around',
                        marginBottom: '10px',
                        padding: '5px',
                      }}
                    >
                      <TextField
                        value={section.name}
                        onChange={(e) => changeSectionName(e, section.id)}
                        onBlur={(e) => updateSectionName(e, section.id)}
                        placeholder={`New Section #${index}`}
                        variant="outlined"
                        multiline
                        sx={{
                          flex: '1',
                          '& .MuiOutlinedInput-input': { padding: 0 },
                          '& .MuiOutlinedInput-notchedOutline': { border: 'unset' },
                          '& .MuiOutlinedInput-root': {
                            fontSize: '1rem',
                            fontWeight: '700'
                          }
                        }}
                      />
                      <div style={{display: 'flex'}}>
                        <IconButton
                          variant="outlined"
                          size="small"
                          onClick={() => createTask(section.id)}
                          sx={{
                            color: 'gray',
                            '&:hover': { color: theme.button.success }
                          }}
                        >
                          <AddOutlinedIcon />
                        </IconButton>
                        <IconButton
                          variant="outlined"
                          size="small"
                          sx={{
                            color: 'gray',
                            '&:hover': { color: theme.button.error }
                          }}
                          onClick={() => removeSection(section.id)}
                        >
                          <DeleteOutlinedIcon />
                        </IconButton>
                      </div>
                    </Box>
                  </Box>
                  <Droppable key={section.id} droppableId={section.id.toString()}>
                    {(provided) => (
                      <div ref={provided.innerRef} {...provided.droppableProps}>
                        {section.tasks
                          .sort((a, b) => a.position - b.position)
                          .map((task, index) => (
                            <Draggable
                              key={task.id}
                              draggableId={task.id.toString()}
                              index={index}
                            >
                              {(provided, snapshot) => (
                                <Card
                                  ref={provided.innerRef}
                                  {...provided.draggableProps}
                                  {...provided.dragHandleProps}
                                  sx={{
                                    backgroundColor: theme.card.main,
                                    padding: '10px',
                                    marginBottom: '10px',
                                    marginRight: '10px',
                                    cursor: snapshot.isDragging ? 'grab' : 'pointer!important',
                                    display: 'flex',
                                    justifyContent: 'space-between'
                                  }}
                                >
                                  <TextField
                                    value={task.name}
                                    onChange={(e) => changeTaskName(e, section.id, task.id)}
                                    onBlur={(e) => updateTaskName(e, task.id)}
                                    placeholder={`New Task #${index}`}
                                    variant="outlined"
                                    multiline
                                    sx={{
                                      flex: '1',
                                      '& .MuiOutlinedInput-input': { padding: 0 },
                                      '& .MuiOutlinedInput-notchedOutline': { border: 'unset' },
                                      '& .MuiOutlinedInput-root': {
                                        fontSize: '1rem',
                                        fontWeight: '700',
                                      }
                                    }}
                                  />
                                  <div>
                                    <IconButton
                                      variant="outlined"
                                      size="small"
                                      sx={{
                                        color: theme.placeholder.main,
                                        '&:hover': { color: theme.button.error}
                                      }}
                                      onClick={() => removeTask(section.id, task.id)}
                                    >
                                      <DeleteOutlinedIcon />
                                    </IconButton>
                                  </div>
                                </Card>
                              )}
                            </Draggable>
                          ))}
                        {provided.placeholder}
                        {section.tasks.length === 0 && (
                            <Typography 
                              variant="body2" 
                              fontWeight="700" 
                              sx={{
                                color: theme.placeholder.dark,
                                textAlign: 'center',
                              }}
                            >
                              Empty Section
                            </Typography>
                        )}
                      </div>
                    )}
                  </Droppable>
                </div>
              ))}
            </Box>
          </DragDropContext>
        </>
      )
}

export default Kanban;