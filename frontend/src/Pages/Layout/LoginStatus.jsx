import { useContext } from "react";
import { UserContext } from "../../App";
import { Link } from "react-router-dom";

const LoginStatus = () => {
    const { user, setUser } = useContext(UserContext);

    const postLogout = async (e) => {
        e.preventDefault();

        const response = await fetch("https://localhost:44353/logout", {
            method: "GET",
            credentials: "include"
        })
        if (!response.ok) {
        } else {
            setTimeout(() => setUser(null), 300);
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