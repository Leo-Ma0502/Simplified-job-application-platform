import React from "react";

const JobDetail = ({ job }) => {
  if (!job) return null;

  return (
    <div
      style={{ padding: "20px", backgroundColor: "white", borderRadius: "8px" }}
    >
      <h3>{job.title}</h3>
      <p>Location: {job.location}</p>
      <p>Posted: {job.postDate}</p>
      <p>Description: {job.description}</p>
    </div>
  );
};

export default JobDetail;
