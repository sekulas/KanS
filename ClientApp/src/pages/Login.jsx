import { Box, TextField } from '@mui/material'
import { useState } from 'react'
import LoadingButton from '@mui/lab/LoadingButton'

const Login = () => {

    const [loading, setLoading] = useState(false)

    const handleSubmit = () => {

    }

    return (
        <>
            <Box
                component='form'
                sx={{ mt:1 }}
                onSubmit={handleSubmit}
                noValidate
            >
                <TextField
                    margin='normal'
                    required
                    fullWidth
                    id='email'
                    label='Email'
                    name='email'
                    type='email'
                    disabled={loading}
                />
                <TextField
                    margin='normal'
                    required
                    fullWidth
                    id='password'
                    label='Password'
                    name='password'
                    type='password'
                    disabled={loading}
                />
                <LoadingButton
                    sx={{mt:3, mb:2}}
                    variant='outlined'
                    fullWidth
                    color='success'
                    type='submit'
                    loading={loading}
                >
                    Login
                </LoadingButton>
            </Box>
        </>
    )
}

export default Login