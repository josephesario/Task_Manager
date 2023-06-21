import { toast } from 'react-hot-toast';

async function RegisterApiRcontrol(state) {
    try {
        const response = await fetch("/api/UserDetails/Register", {
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
        if (response.status === 500) {
            toast.error("Something Went Wrong");
            return;
        }

        const data = await response.text();
        toast.success(data);
    } catch (error) {
        console.log("error", error);
    }
}

export { RegisterApiRcontrol };
