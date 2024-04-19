import React, { useState } from "react";
import { useAuth } from "../../contexts/AuthContext";
import Logout from "../Auth/Logout";
import JobItem from "./JobItem";
import JobDetail from "./JobDetail";
import "./JobListing.css";

function JobListing() {
  const [selectedJob, setSelectedJob] = useState(null);
  const [showDetail, setShowDetail] = useState(false);
  const { loggedIn } = useAuth();

  const handleJobClick = (job) => {
    setSelectedJob(job);
    setShowDetail(true);
  };

  const handleCloseDetail = () => {
    setShowDetail(false);
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
    <div className="joblisting-container">
      <div className="joblisting-header">
        {loggedIn ? (
          <div>
            <h1>Welcome, {localStorage.getItem("name")}</h1>
            <Logout />
          </div>
        ) : (
          <div>
            <button className="joblisting-button">Login</button>
            <button className="joblisting-button">Register</button>
          </div>
        )}
      </div>
      <div className="joblisting-body">
        <div className="joblisting-list">
          {jobs.map((job) => (
            <div
              key={job.id}
              className={`joblisting-item ${
                selectedJob && selectedJob.id === job.id
                  ? "joblisting-item-selected"
                  : ""
              }`}
              onClick={() => handleJobClick(job)}
            >
              <JobItem
                jobTitle={job.title}
                jobLocation={job.location}
                postDate={job.postDate}
              />
            </div>
          ))}
        </div>

        <div
          className={`joblisting-detail ${
            showDetail ? "joblisting-detail-active" : ""
          }`}
        >
          {showDetail ? (
            <div>
              <JobDetail job={selectedJob} />
              <button onClick={handleCloseDetail}>Close</button>
            </div>
          ) : (
            <p>Click job card to view details</p>
          )}
        </div>
      </div>
    </div>
  );
}

export default JobListing;
