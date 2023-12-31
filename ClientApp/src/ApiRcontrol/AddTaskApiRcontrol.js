﻿import { toast } from 'react-hot-toast';
import jwtDecode from 'jwt-decode'

async function AddTaskApiRcontrol(state) {
    try {


        const token = localStorage.getItem('token')

        // Assuming you have the JWT token stored in a variable called 'token'
        const decodedToken = jwtDecode(token);

        // Retrieve the email from the decoded token
        const email = decodedToken?.email

        state.Email = email;
        state.Status = true;


        const response = await fetch("/api/Task/CreateTask", {
            method: "post",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(state)
        });

        if (response.status === 409) {
            toast.error("Task Already Exist");
            return;
        }

        const data = await response.text();
        toast.success("task added successfully");

    } catch (error) {
        console.log("error", error);
    }
}

export { AddTaskApiRcontrol };



/*

*/
