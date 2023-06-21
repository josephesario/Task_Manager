import { toast } from 'react-hot-toast';


async function UpdateTask(Name) {

    try {

        const response = await fetch(`/api/Task/UpdateTask/${Name}`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            }

        })

        if (response.status === 404) {
            toast.error("Task not found");
            return
        }

        if (response.status === 204) {
            return response
        }
    }

    catch (error) {
        toast.error("An error occurred while deleting the task");
        console.log("error", error);
    }
    
}

export default UpdateTask;
