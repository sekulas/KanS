import { useState, useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom'
import { useDispatch } from 'react-redux'
import authUtils from '../../utils/authUtils'
import Loading from '../common/Loading'
import { Box } from '@mui/material'
import Sidebar from '../common/Sidebar'
import { setUser } from '../../redux/features/userSlice'
const AppLayout = () => {
  const navigate = useNavigate()
  const dispatch = useDispatch()
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const checkAuth = async () => {
      const user = authUtils.isAuthenticated()

      if(!user) {
        navigate('/login')
      } else {
        dispatch(setUser(user))
        setLoading(false)
      }
    }

    checkAuth()

  }, [navigate])
  
  return (
      loading ? (
        <Loading fullHeight/>
      ) : (
        <Box sx={{
          display: 'flex'
        }}>
          <Sidebar/>
          <Box sx={{
            flexGrow: 1,
            p: 1,
            width: '100%',
            height: '100vh'
          }}>
            <Outlet/>
          </Box>
        </Box>
    )
  )
}

export default AppLayout