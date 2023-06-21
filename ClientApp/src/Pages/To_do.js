import React, { useState, useEffect } from 'react';
import '../style/style.css';
import GetAllTaskApiRcontrol from '../ApiRcontrol/GetAllTaskApiRcontrol';
import deleteTask from '../ApiRcontrol/DeleteTaskApiRcontrol';
import UpdateTask from '../ApiRcontrol/UpdateTaskApiRcontrol';
import { useNavigate } from 'react-router-dom';
import { toast } from "react-hot-toast"


function TaskCard() {
    const [tasks, setTasks] = useState([]);

    const navigate = useNavigate()


    const onDelete = (name) => {

        deleteTask(name).then((res) => {

            window.location.reload()
            setTimeout(() => {
                toast.success("task deleted successfully")
            }, 500)
        }).catch((err) => console.log(err))
    }



    const onUpdate = (name) => {

        UpdateTask(name).then((res) => {

            window.location.reload()
            setTimeout(() => {
                toast.success("task Updated successfully")
            }, 500)
        }).catch((err) => console.log(err))
    }


    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await GetAllTaskApiRcontrol();
                console.log(response);
                if (response && response.length > 0) {
                    setTasks(response);
                }
            } catch (error) {
                console.log("Error fetching tasks:", error);
            }
        };

        fetchData();
    }, []);





    return (
        <div className="container card-container33">
            <div className="card-container">
                <h1 className="title">Pending</h1>
                <div className="task-grid">
                    {tasks &&
                        tasks
                            .filter((task) => task.status) // Filter tasks with status = true
                            .map((task) => (
                              <div className="card shadow-sm" key={task.name}>
                                    <div className="card-body">
                                        <h5 className="card-title">{task.name}</h5>
                                        <p className="card-text">{task.description}</p>
                                        <p className="card-text">Added By: {task.email}</p>
                                        <button className="btn4 btn-danger" onClick={() => onDelete(task.name)}>Delete</button>
                                        <button className="btn2 btn-danger" onClick={() => onUpdate(task.name)}>Completed</button>
                                    </div>
                                </div>

                            ))}
                </div>
            </div>
        </div>
    );
}

export default TaskCard;
