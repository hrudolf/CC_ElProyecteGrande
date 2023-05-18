import { useContext, useState } from 'react';
import './HomePage.css'
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { UserContext } from "../App";

const LoginPage = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [message, setMessage] = useState("");
    const [loginname, setLoginname] = useState('');
    const [password, setPassword] = useState('');
    const {setUser} = useContext(UserContext);

    const postRequest = async (e) => {
        e.preventDefault();
        setLoading(true);
        setMessage("");
        setError("");

        const headers = { "Content-Type": "application/json" };
        const body = {
            loginName: loginname,
            password: password
        };
        const response = await fetch("/login", {
            method: "POST",
            headers: headers,
            body: JSON.stringify(body),
        })
        const json = await response.json();
        console.log(json)
        if (!response.ok) {
            setError(json.message);
            setLoading(false);
            setMessage('');
        } else {
            setError('');
            setLoading(false);
            setMessage('Successful login');
            setUser(json);
            console.log(json);
        }
    }

    return (
        <div>
            <div className="container bg-light w-50 p-2">
                <h1 className="text-center">Log in</h1>
                <form className="UserForm" onSubmit={postRequest}>
                    <div className="row no-gutters w-100">
                        <Row>
                            <Col>
                                <label htmlFor="loginName">Login name:</label>
                            </Col>
                            <Col>
                                <input
                                    className="w-100"
                                    type="int"
                                    name="loginname"
                                    id="loginname"
                                    value={loginname}
                                    onChange={(e) => setLoginname(e.target.value)}
                                    required
                                />
                            </Col>
                        </Row>
                    </div>

                    <div className="row no-gutters w-100">
                        <Row>
                            <Col>
                                <label htmlFor="startDate">Password:</label>
                            </Col>
                            <Col>
                                <input
                                    className="w-100"
                                    type="text"
                                    name="password"
                                    id="password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    required
                                />
                            </Col>
                        </Row>
                    </div>

                    <div className="buttons">
                        <button
                            type="submit"
                            className="btn btn-primary w-auto m-1"
                            disabled={loading}
                        >
                            Login
                        </button>
                    </div>
                </form>
                <div>Message: {message}</div>
                <div>Error: {error}</div>
            </div>
        </div>
    )
};

export default LoginPage;
