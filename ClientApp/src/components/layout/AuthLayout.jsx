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
        navigate('/account/login')
      }
    }

    checkAuth()

  }, [navigate])

  return (
    <div>
      loading ? (
        <Loading fullHeight/>
      ) : (
        <Container component='main' maxWidth='xs'>
          <Box sx={{
            marginTop: 8,
            display: 'flex',
            alignItems: 'center',
            flexDirection: 'column'
          }}>
            <img src={assets.images.logoDark} style={{width: '100px'}} alt='KanS logo'/>
            <Outlet/>
          </Box>
        </Container>
      )
    </div>
  )
}

export default AuthLayout