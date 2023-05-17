import { useContext } from "react";
import { UserContext } from "../../App";
import { Link } from "react-router-dom";

const LoginStatus = () => {
    const { user, setUser } = useContext(UserContext);

    const postLogout = async (e) => {
        e.preventDefault();
        /* setLoading(true);
        setMessage("");
        setError(""); */

        const response = await fetch("/logout", {
            method: "GET"
        })
        if (!response.ok) {
            /* setLoading(false);
            setMessage(''); */
        } else {
            /* setLoading(false);
            setMessage('Successful logout'); */
            setUser(null);
        }
    }

    return (
        <div className="nav-item">
            {user === null && <Link to="/login"
                className="nav-link active h5"
                aria-current="page">
                Login
            </Link>}
            {user !== null && <><span>{`${user.firstName} ${user.lastName}`}</span>
                <button type="logout" className="btn btn-primary w-auto m-1" disabled={false/* loading */} onClick={postLogout}>
                    Logout
                </button>
            </>}
        </div>
    );
}

export default LoginStatus;