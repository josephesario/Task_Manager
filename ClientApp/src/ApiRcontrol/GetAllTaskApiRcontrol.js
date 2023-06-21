async function GetAllTaskApiRcontrol(state) {
    const tok = localStorage.getItem("token");
    console.log(tok);

    try {
        const response = await fetch("/api/Task/GetAllTask", {
            headers: {
                Authorization: `Bearer ${tok}`,
            },
        });

        if (!response.ok) {
            throw new Error('Request failed');
        }

        const data = await response.json(); // Update to response.json() to parse the response as JSON
        return data;
    } catch (error) {
        console.log("error", error);
        // You may want to handle the error here or propagate it to the caller
    }
}

export default GetAllTaskApiRcontrol;
