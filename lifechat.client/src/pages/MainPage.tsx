import { Box, Theme, useTheme } from "@mui/material";
import { useDispatch, useSelector,  } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { setConnector } from "../store/slices/ConnectorSlice";
import { SignalConnector } from "../API/SignalConnector";
import { Topbar } from "../components/global/Tobpar";
import { ContactsDrawer } from "../components/global/ContactsDrawer";
import Interceptors from "../API/Interceptors";
import { setLoggedIn, setToken, setUsername } from "../store/slices/UserSlice";
import HttpClient from "../API/HttpClient";
import { MessageChannel } from "../components/MessageChannel";
import { MainDefault } from "../components/MainDefault";
import Channel from "../model/Channel";


function MainPage() {

    const theme: Theme = useTheme()
    const dispatch = useDispatch()
    const isLogged = useSelector(state => state.user.loggedIn)

    const reroute = useNavigate()
    
    useEffect(() => {
        const remember = localStorage.getItem("remember")
        const signalConnector = new SignalConnector()
        dispatch(setConnector(signalConnector))
        
        const token = localStorage.getItem("token")
        if((remember === null || remember === undefined) && !isLogged) {
            localStorage.removeItem("token")
            localStorage.removeItem("remember")
            reroute("/")
            return
        }
        else if(token && !isLogged) {
            dispatch(setToken(token))
            Interceptors.addAuthInterceptor("token")
            const subscription = HttpClient.get("https://localhost:7078/api/Users")
                .subscribe({
                    next(response) {

                        dispatch(setUsername(response))
                        dispatch(setToken(token))
                        dispatch(setLoggedIn(true))
                        localStorage.setItem("token", token)
                    },
                    error(err) {
                        console.error(err.message)
                    },
                    complete() {
                        subscription.unsubscribe()
                        dispatch(setLoggedIn(true))
                    }
                })
        }
    },[])

    const [drawerOpen, setDrawerOpen] = useState<boolean>(false)
    const [contentOffset, setContentOffset] = useState<number>(0)
    const drawerWidth: number = 20
    const drawerTransitionTime: number = 400
    const defaultContent = <MainDefault/>
    const [mainContent, setMainContent] = useState(defaultContent)


    useEffect(() => {
        drawerOpen === true ? setContentOffset(drawerWidth) :
            setContentOffset(0)
    }, [drawerOpen])

    const toggleDrawer = () => {
        setDrawerOpen(!drawerOpen)
    }
    
    const updateDrawer = (newState: boolean) => {
        setDrawerOpen(newState)
    }

    const selectChannel = (channel: Channel) => {
        setMainContent(<MessageChannel drawerTransitionTime={drawerTransitionTime} contentOffset={contentOffset} channel={channel}/>)
    }

    return (
        <Box     
            display="flex"
            flexDirection="column"
            sx={{
                height: '100%',
                marginLeft: `${contentOffset}%`,
                transition: `margin-left ${drawerTransitionTime}ms ease-in-out`,
                bgcolor: theme.palette.background.default
            }}>
            <ContactsDrawer 
                height={5}
                 open={drawerOpen}
                 width={drawerWidth}
                 transitionTime={drawerTransitionTime}
                 handleClose={updateDrawer}
                 handleChannelSelect={selectChannel}>
            </ContactsDrawer>
            <Topbar height={5} onMenuOpen={toggleDrawer} drawerWidth={ drawerOpen === true ? drawerWidth : 0} drawerTransitionTime={drawerTransitionTime}></Topbar>
            <Box width="100%" height="100%" display="flex" alignItems="center" justifyContent="center" bgcolor={theme.palette.background.light}>
                {mainContent}
            </Box>

        </Box>
    )
}

export default MainPage;