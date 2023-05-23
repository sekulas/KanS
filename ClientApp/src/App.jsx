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
            main: "rgb(16, 89, 96)",
            light: "rgb(26, 146, 157)"
        },
        list: {
            main: "rgb(0, 64, 128)",
            selected: "rgb(0, 93, 183)"
        },
        success: {
            main: "#388e3c"
        },
        error: {
            main: "#f44336"
        },
        warning: {
            main: "#f57c00",
            light: "#ffb74d"
        },
        share: {
            main: "#FF00FF",
            dark: "#9D009D"
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
