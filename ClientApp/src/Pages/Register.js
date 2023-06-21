import React, { useState } from 'react';
import '../style/signin.css';
import { RegisterApiRcontrol } from '../ApiRcontrol/RegisterApiRcontrol';

function Register() {
    const [state, setState] = useState({});

    async function submit(e) {
        e.preventDefault();

        await RegisterApiRcontrol(state);
    }

    function handleChange(e) {
        setState((prev) => ({
            ...prev,
            [e.target.name]: e.target.value,
        }));
    }

    console.log(state);

    return (
        <div className="text-center">
            <main className="form-signin">
                <form onSubmit={submit}>
                    <img
                        className="mb-4 rounded-image"
                        src="https://www.bing.com/th/id/OGC.fe0187c9dc3ef7cf9e24a52ccb0aa96f?pid=1.7&rurl=https%3a%2f%2fcdn.dribbble.com%2fusers%2f1210339%2fscreenshots%2f2767019%2favatar18.gif&ehk=tWWLaDBl5zLniCm6a53IGJTH0Iv%2bk6TE2qys069Gwqk%3d"
                        alt=""
                        width="72"
                        height="57"
                    />
                    <h1 className="h3 mb-3 fw-normal">Sign Up</h1>
                    <div className="form-floating">
                        <input
                            type="email"
                            className="form-control focus-ring custom-input"
                            id="email"
                            placeholder="name@example.com"
                            autoComplete="off"
                            name="Email"
                            onChange={handleChange}
                        />
                        <label htmlFor="email">Email address</label>
                    </div>
                    <div className="form-floating">
                        <input
                            type="password"
                            className="form-control focus-ring custom-input"
                            id="password"
                            placeholder="Password"
                            autoComplete="off"
                            name="Password"
                            onChange={handleChange}
                        />
                        <label htmlFor="password">Password</label>
                    </div>

        
                    <button className="w-100 btn btn-lg btn-primary" type="submit">
                        Register
                    </button>
                    <div className="text-center mt-3">
                        Already Subscribed? <a href="/">Login</a>
                    </div>
                </form>
            </main>
        </div>
    );
}

export default Register;
