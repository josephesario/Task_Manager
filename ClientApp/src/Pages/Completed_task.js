import React, { useState, useEffect } from 'react';
import '../style/style.css';
import GetAllTaskApiRcontrol from '../ApiRcontrol/GetAllTaskApiRcontrol';

function Completed_task() {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await GetAllTaskApiRcontrol();
                console.log(response);
                if (response && response.tasks) {
                    setTasks(response.tasks);
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
                    {tasks && tasks.map((task) => {
                        if (!task.status.status) {
                            return (
                                <div className="card shadow-sm" key={task.id}>
                                    <div className="card-body">
                                        <h5 className="card-title">{task.name}</h5>
                                        <p className="card-text">{task.description}</p>
                                        <p className="card-text">Added By: {task.email.email}</p>
                                        <button className="btn4 btn-danger">Delete</button>
                                   
                                    </div>
                                </div>
                            );
                        }
                        return null; // If task.status.status is false, don't render the card
                    })}
                </div>
            </div>
        </div>
    );
}

export default Completed_task;
