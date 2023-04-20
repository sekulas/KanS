import { Box, Button, TextField } from '@mui/material'
import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import LoadingButton from '@mui/lab/LoadingButton'

const Register = () => {

    const navigate = useNavigate()

    const [loading, setLoading] = useState(false)
    const [nameErrText, setNameErrText] = useState('')
    const [emailErrText, setEmailErrText] = useState('')
    const [passwordErrText, setPasswordErrText] = useState('')
    const [confirmPasswordErrText, setConfirmPasswordErrText] = useState('')

    const handleSubmit = (e) => {
        e.preventDefault()
        setNameErrText('')
        setEmailErrText('')
        setPasswordErrText('')
        setConfirmPasswordErrText('')
    
        const data = new FormData(e.target)
        const name = data.get('name').trim()
        const email = data.get('email').trim()
        const password = data.get('password').trim()
        const confirmPassword = data.get('confirmPassword').trim()

        let err = false
        
        if( name === '' ) {
            err = true
            setNameErrText('Please fill this field')
        }
        if( email === '' ) {
            err = true
            setEmailErrText('Please fill this field')
        }
        if( password === '' ) {
            err = true
            setPasswordErrText('Please fill this field')
        }
        if( confirmPassword === '' ) {
            err = true
            setConfirmPasswordErrText('Please fill this field')
        }
        if( password !== confirmPassword ) {
            err = true
            setPasswordErrText('Confirm password do not match Password')
        }

        if( err ) {
            return
        }
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
                id='name'
                label='Name'
                name='name'
                type='name'
                disabled={loading}
                error={nameErrText !== ''}
                helperText={nameErrText}
            />
            <TextField
                margin='normal'
                required
                fullWidth
                id='email'
                label='Email'
                name='email'
                type='email'
                disabled={loading}
                error={emailErrText !== ''}
                helperText={emailErrText}
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
                error={passwordErrText !== ''}
                helperText={passwordErrText}
            />
            <TextField
                margin='normal'
                required
                fullWidth
                id='confirmPassword'
                label='Confirm Password'
                name='confirmPassword'
                type='password'
                disabled={loading}
                error={confirmPasswordErrText !== ''}
                helperText={confirmPasswordErrText}
            />
            <LoadingButton
                sx={{mt:3, mb:2}}
                variant='outlined'
                fullWidth
                color='success'
                type='submit'
                loading={loading}
            >
                Register
            </LoadingButton>
        </Box>
        <Button
            component={Link}
            to='/login'
            sx={{textTransform: 'none'}}
        >
            Already registered? Login!
        </Button>
    </>
    )
}

export default Register