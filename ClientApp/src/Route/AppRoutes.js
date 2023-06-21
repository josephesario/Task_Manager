import Completed_task from "../Pages/Completed_task"
import AddTask from "../Pages/Add_task"
import To_do from "../Pages/To_do"
import Home from "../Pages/Home"
import Login from "../Pages/Login"
import isAuthenticated from "../ApiRcontrol/Authentication"
import { useNavigate, Outlet } from "react-router-dom"


const AppRoutes = [
    {
        index: true,
        element: <Login />
    },
    {
        path: '/Home/Add_task',
        element: <AddTask />
    },
    {
        path: '/Home/To_do',
        element: <To_do/>
    },
    {
        path: '/Home/Complete_Task',
        element: <Completed_task />
    },
    {
        path: '/home',
        element: <Home />
    }
];


function ProtectedRoute() {

    const navigate = useNavigate()

    if (isAuthenticated()) {

        return (
            <Outlet/>
        )
    }

    else {
        return (
            <Login/>
            )
    }
}

export default  ProtectedRoute  ;
