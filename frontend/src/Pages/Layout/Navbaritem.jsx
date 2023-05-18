import { Link } from "react-router-dom";

const Navbaritem = ({item}) => {

    return (
        <li className="nav-item">
            <Link
                to={item.path}
                className="nav-link active h5"
                aria-current="page">
                {item.title}
            </Link>
        </li>
    );
}

export default Navbaritem;