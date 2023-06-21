import React, { useState } from 'react';
import { AddTaskApiRcontrol }  from '../ApiRcontrol/AddTaskApiRcontrol';
import '../style/style.css';



function TaskForm() {
    const [formData, setFormData] = useState({});

    function handleChange(e) {
        setFormData((prev) => ({
            ...prev,
            [e.target.name]: e.target.value,
        }));

       
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await AddTaskApiRcontrol(formData);
            window.location.reload()
            // Reset the form after successful submission if needed
            setFormData({
                Name: '',
                Description: ''
            });
        } catch (error) {
            console.log("Error adding task:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="task-form card shadow">
            <div className="card-body">
                <h5 className="card-title">Task Form</h5>
                <div className="form-group">
                    <label htmlFor="title" className="form-label">Title</label>
                    <input type="text" className="form-control" id="Name" name="Name"  onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="description" className="form-label">Description</label>
                    <textarea className="form-control" id="Description" name="Description" onChange={handleChange} required />
                </div>

                <button type="submit" className="btn btn-primary">Submit</button>
            </div>
        </form>
    );
}

export default TaskForm;
