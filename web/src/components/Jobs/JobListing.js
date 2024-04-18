import Logout from "../Auth/Logout";
import JobItem from "./JobItem";
import JobDetail from "./JobDetail";
import { useState } from "react";

function JobListing() {
  const [selectedJob, setSelectedJob] = useState(null);

  const handleJobClick = (job) => {
    setSelectedJob(job);
  };

  const jobs = [
    {
      id: 1,
      title: "Software Engineer",
      location: "New York, NY",
      postDate: "2023-04-16T14:00:00Z",
      description:
        "This is a software engineer position focusing on backend development.",
    },
    {
      id: 2,
      title: "Software Engineer 2",
      location: "New York, NY",
      postDate: "2023-04-16T14:00:00Z",
      description:
        "This is another software engineer position focusing on backend development.",
    },
  ];

  return (
    <div style={{ fontFamily: "Arial, sans-serif" }}>
      {/* Header with Welcome and Logout */}
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          padding: "20px",
          backgroundColor: "#f5f5f5",
          borderBottom: "1px solid #ccc",
        }}
      >
        <h1>Welcome, {localStorage.getItem("name")}</h1>
        <Logout />
      </div>

      {/* Main content area with job list and details */}
      <div style={{ display: "flex", padding: "20px" }}>
        {/* Job list */}
        <div style={{ flex: 1, marginRight: "20px" }}>
          {jobs.map((job) => (
            <div
              key={job.id}
              onClick={() => handleJobClick(job)}
              style={{
                cursor: "pointer",
                marginBottom: "10px",
                padding: "10px",
                border: "1px solid #ccc",
                borderRadius: "5px",
                backgroundColor:
                  selectedJob && selectedJob.id === job.id
                    ? "#e6e6e6"
                    : "white",
              }}
            >
              <JobItem
                jobTitle={job.title}
                jobLocation={job.location}
                postDate={job.postDate}
              />
            </div>
          ))}
        </div>
        {/* Job details */}
        <div style={{ flex: 2 }}>
          <JobDetail job={selectedJob} />
        </div>
      </div>
    </div>
  );
}

export default JobListing;
