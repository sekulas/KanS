import { useState, useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import authUtils from '../../utils/authUtils'
import Loading from '../common/Loading'
import { Container, Box } from '@mui/material'
import assets from '../../assets'

const AuthLayout = () => {

  const navigate = useNavigate()
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const checkAuth = async () => {
      const isAuth = authUtils.isAuthenticated()

      if(!isAuth) {
        setLoading(false)
      } else {
        navigate('/')
      }
    }

    checkAuth()

  }, [navigate])

  return (
      loading ? (
        <Loading fullHeight/>
      ) : (
        <Container component='main' maxWidth='xs'>
          <Box sx={{
            display: 'flex',
            alignItems: 'center',
            flexDirection: 'column',
            justifyContent: 'center',
            height: '100vh',
          }}>
            <img src={assets.images.logoDark} style={{width: '100px'}} alt='KanS logo'/>
            <Outlet/>
          </Box>
        </Container>
      )
  )
}

export default AuthLayout