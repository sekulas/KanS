import React from 'react'
import CssBaseline from '@mui/material/CssBaseline'
import { ThemeProvider, createTheme } from '@mui/material/styles'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import AppLayout from './components/layout/AppLayout'
import AuthLayout from './components/layout/AuthLayout'
import Home from './pages/Home'
import Board from './pages/Board'
import Register from './pages/Register'
import Login from './pages/Login'

function App() {

    const theme = createTheme({
        palette: {
            mode: 'dark'
        },  
        card: {
            main: "rgb(16, 89, 96)"
        },
        list: {
            main: "rgb(0, 64, 128)",
            selected: "rgb(0, 93, 183)"
        },
        success: {
            main: "#66bb6a"
        },
        error: {
            main: "#f44336"
        }
    })

    return (
        <ThemeProvider theme={theme}>
            <CssBaseline/>
            <BrowserRouter>
                <Routes>
                    <Route path='/' element={<AuthLayout/>}>
                        <Route path='login' element={<Login/>}/>
                        <Route path='register' element={<Register/>}/>
                    </Route>
                    <Route path='/' element={<AppLayout/>}>
                        <Route index element={<Home/>}/>
                        <Route path='boards/:boardId' element={<Board/>}/>
                    </Route>
                </Routes>
            </BrowserRouter>
        </ThemeProvider>
    )
}

export default App
