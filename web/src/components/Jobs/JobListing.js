import Logout from "../Auth/Logout";

function JobListing() {
  return (
    <div>
      <h1>Welcome, {localStorage.getItem("name")}</h1>
      <Logout />
    </div>
  );
}

export default JobListing;
