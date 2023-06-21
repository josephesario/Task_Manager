import React, { useState, useEffect } from 'react';
import '../style/style.css';
import GetAllTaskApiRcontrol from '../ApiRcontrol/GetAllTaskApiRcontrol';

function CompletedTask() {
    const [tasks, setTasks] = useState([]);

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
                <h1 className="title">Completed</h1>
                <div className="task-grid">
                    {tasks &&
                        tasks
                            .filter((task) => !task.status) // Filter tasks with status = false
                            .map((task) => (
                                <div className="card shadow-sm" key={task.name}>
                                    <div className="card-body">
                                        <h5 className="card-title">{task.name}</h5>
                                        <p className="card-text">{task.description}</p>
                                        <p className="card-text">Added By: {task.email}</p>
                                        <button className="btn4 btn-danger">Delete</button>
                                    </div>
                                </div>
                            ))}
                </div>
            </div>
        </div>
    );
}

export default CompletedTask;
