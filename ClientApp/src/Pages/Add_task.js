import React, { useState } from 'react';
import { AddTaskApiRcontrol } from '../ApiRcontrol/AddTaskApiRcontrol';
import '../style/style.css';

function TaskForm() {
    const [formData, setFormData] = useState({
        title: '',
        description: '',
        dueDate: ''
    });

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await AddTaskApiRcontrol(formData);
            // Reset the form after successful submission if needed
            setFormData({
                title: '',
                description: '',
                dueDate: ''
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
                    <input type="text" className="form-control" id="title" name="title" value={formData.title} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="description" className="form-label">Description</label>
                    <textarea className="form-control" id="description" name="description" value={formData.description} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="dueDate" className="form-label">Due Date</label>
                    <input type="date" className="form-control" id="dueDate" name="dueDate" value={formData.dueDate} onChange={handleChange} required />
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </div>
        </form>
    );
}

export default TaskForm;
