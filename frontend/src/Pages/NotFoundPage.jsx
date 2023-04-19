import { Link } from "react-router-dom";

const NotFoundPage = () => {
  return (
    <div id="error-page">
      <h1>Error 404! Oops!</h1>
      <p>The requested site was not found on the server.</p>
      <Link to={"/"}>Back to homepage</Link>
    </div>
  );
};

export default NotFoundPage;