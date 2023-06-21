import { toast } from 'react-hot-toast';


async function LoginApiRcontrol(state) {

    try {
        const response = await fetch("/api/UserDetails/Authenticate", {
            method: "post",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(state)
        });

        if (response.ok) {

            const data = await response.json();
            toast.success("Login successful");
            console.log(data)
            localStorage.setItem("token", data.token);
            return true;

        } else if (response.status === 401) {

            toast.error("Authentication failed");

        } else {
            const errorMessage = await response.text();
            toast.error(`Something went wrong: ${errorMessage}`);
        }
    } catch (error) {
        console.log("error", error);
        toast.error("Something went wrong");
    }
}

export { LoginApiRcontrol }


