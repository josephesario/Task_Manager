﻿import { toast } from 'react-hot-toast';

async function AddTaskApiRcontrol(state) {
    try {
        const response = await fetch("/api/Task/CreateTask", {
            method: "post",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(state)
        });

        if (response.status === 409) {
            toast.error("Email Already Exist");
            return;
        }

        if (response.status === 400) {
            toast.error("All fields required");
            return;
        }

        const data = await response.text();
        toast.success(data);
    } catch (error) {
        console.log("error", error);
    }
}

export { AddTaskApiRcontrol };