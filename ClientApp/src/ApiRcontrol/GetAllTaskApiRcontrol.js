import jwtDecode from 'jwt-decode'

async function GetAllTaskApiRcontrol() {


    const token = localStorage.getItem('token')

    // Assuming you have the JWT token stored in a variable called 'token'
    const decodedToken = jwtDecode(token);

    // Retrieve the email from the decoded token
    const Email = decodedToken?.email


    try {

        const response = await fetch(`/api/Task/GetAllTask/${Email}`);

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
