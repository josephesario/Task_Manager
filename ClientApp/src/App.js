import React  from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './Route/AppRoutes';
import Layout from './Share/Layout';


import Login from './Pages/Login';
import Register from './Pages/Register';
import Home from './Pages/Home';
import To_do from './Pages/To_do';
import Dashboard from './Pages/Dashboard';
import Add_task from './Pages/Add_task';
import Completed_task from './Pages/Completed_task';
import { Toaster } from 'react-hot-toast';

function App() {

    return (

        <>

                <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/register" element={<Register />} />


                <Route path="home" element={<Home />} >
                    <Route path='/home' element={<Dashboard />} />
                    <Route path='todo' element={<To_do />} />
                    <Route path='add_task' element={<Add_task />} />
                    <Route path='completed_task' element={<Completed_task />} />
                </Route>

            </Routes>

            <Toaster />

        </>
    )
}

export default App



