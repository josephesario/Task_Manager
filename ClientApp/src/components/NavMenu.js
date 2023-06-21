import React from 'react';
import { NavLink } from 'react-router-dom';
import {useNavigate } from "react-router-dom"


function Navbar() {

    const navigate = useNavigate()

    function logout() {
        localStorage.removeItem("token")

        navigate("/")
    }


    return (
        <nav className="py-2 bg-light topNav border-bottom">
            <div className="container d-flex flex-wrap">
                <ul className="nav me-auto">
                    <li className="nav-item">
                        <NavLink to="/Home" className="nav-link link-dark px-2" >
                            Home
                        </NavLink>
                    </li>
                    <li className="nav-item">
                        <NavLink to="/Home/todo" className="nav-link link-dark px-2" >
                            ToDo
                        </NavLink>
                    </li>
                    <li className="nav-item">
                        <NavLink to="/home/completed_task" className="nav-link link-dark px-2" >
                            Done
                        </NavLink>
                    </li>
                    <li className="nav-item">
                        <NavLink to="/home/add_task" className="nav-link link-dark px-2" >
                            Add
                        </NavLink>
                    </li>
                </ul>
                <ul className="nav">
                    <li className="nav-item" onClick={ logout }>
                        <a className="nav-link link-dark px-2">
                            LogOut
                        </a>
                    </li>
                   
                </ul>
            </div>
        </nav>
    );
}


function Header({ show=true }) {

    return (
        <header className={`${show ? "py-3 mb-4 border-bottom" : "d-hidden"}`}>
            <div className="container d-flex flex-wrap justify-content-center">
                <NavLink to="/" className="d-flex align-items-center mb-3 mb-lg-0 me-lg-auto text-dark text-decoration-none">
                    <svg className="bi me-2" width="40" height="32">
                        <use xlinkHref="#bootstrap" />
                    </svg>
                    <span className="fs-4 taskTitle">Task Manager</span>
                </NavLink>
        
            </div>
        </header>
    );
}

function NavMenu({show }) {
    return (
        <div>
            <Navbar />
            <Header show={ show } />
        </div>
    );
}

export default NavMenu;
