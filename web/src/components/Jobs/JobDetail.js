import React from "react";
import ApplyWithAuth from "./Apply";

const JobDetail = ({ job }) => {
  if (!job) return null;
  const handleApply = () => {
    if (job) {
      const mailtoLink = `mailto:${
        job.email || "change@email_address.com"
      }?subject=Application for ${
        job.title
      }&body=Hi, I am interested in the position advertised.`;
      window.location.href = mailtoLink;
    } else {
      alert("Job details are not available.");
    }
  };

  return (
    <div style={{ padding: "20px", borderRadius: "8px" }}>
      <h3>{job.title}</h3>
      <p>Location: {job.location}</p>
      <p>Posted: {job.postDate}</p>
      <p>Description: {job.description}</p>
      <ApplyWithAuth onClick={handleApply} />
    </div>
  );
};

export default JobDetail;
