import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Login() {
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [rememberme, setRememberme] = useState<boolean>(false);

    const [error, setError] = useState<string>("");
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (name === "email") setEmail(value);
        if (name === "password") setPassword(value);
        if (name === "rememberme") setRememberme(e.target.checked);
    };

    const handleRegisterClick = () => {
        navigate("/register");
    }

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        //validate email and password
        if (!email || !password) {
            setError("Please fill in all fields");
        } else {
            // clear error message
            setError("");
            // post data to the /register api
            var loginurl = "";
            if (rememberme == true) {
                loginurl = "/login?useCookies=true";
            } else {
                loginurl = "/login?useSessionCookies=false";
            }
            fetch(loginurl, {
                method: "POST",
                headers: {
                    "content-Type": "application/json",
                },
                body: JSON.stringify({
                    email: email,
                    password: password,
                })
            })

                .then((data) => {
                    //handle success or error from the server
                    console.log(data);
                    if (data.ok) {
                        setError("Succesfull login.");
                        window.location.href = '/';
                    } else {
                        setError("Error login in.");
                    }
                })
                .catch((error) => {
                    //handle network error
                    console.error(error);
                    setError("Error login in.");
                });
        }


    };
    return (
        <div className="containerbox">

            <h3>Login</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label className="forminput" htmlFor="email">Email:</label>
                </div>
                <div>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        value={email}
                        onChange={handleChange}
                        />
                </div>
                <div>
                    <label className="forminput" htmlFor="password">Password: </label>
                </div>

            </form>

        </div>
    )
}

export default Login();
