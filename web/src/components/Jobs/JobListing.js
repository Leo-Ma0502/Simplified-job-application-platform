import React, { useEffect, useState } from "react";
import JobItem from "./JobItem";
import JobDetail from "./JobDetail";
import "./JobListing.css";
import { fetchJobs } from "../../services/jobService";

function JobListing() {
  const [selectedJob, setSelectedJob] = useState(null);
  const [showDetail, setShowDetail] = useState(false);
  const [jobs, setJobs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);

  const getJobs = async () => {
    console.log("getting jobs");
    setLoading(true);
    try {
      const jobsData = await fetchJobs(page, 2);
      if (jobsData.length === 0) {
        setHasMore(false);
      } else {
        setJobs((prevJobs) => {
          const newJobs = jobsData.filter(
            (job) => !prevJobs.some((prevJob) => prevJob.jId === job.jId)
          );
          return [...prevJobs, ...newJobs];
        });
        setPage((prevPage) => prevPage + 1);
      }
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleScroll = () => {
    if (
      window.innerHeight + window.scrollY >= document.body.offsetHeight &&
      !loading &&
      hasMore
    ) {
      fetchJobs();
    }
  };

  useEffect(() => {
    console.log("loading");
    getJobs();
  }, [page]);

  useEffect(() => {
    window.addEventListener("scroll", handleScroll);
    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, [jobs, loading]);

  const handleJobClick = (job) => {
    setSelectedJob(job);
    setShowDetail(true);
  };

  const handleCloseDetail = () => {
    setShowDetail(false);
  };

  return (
    <div className="joblisting-container">
      <div className="joblisting-body">
        {loading ? (
          <div className="joblisting-list">
            <p></p>loading...
          </div>
        ) : jobs.length === 0 ? (
          <div className="joblisting-list">
            <p>No jobs available</p>
          </div>
        ) : (
          <div className="joblisting-list">
            {jobs.map((job) => (
              <div
                key={job.jId}
                className={`joblisting-item ${
                  selectedJob && selectedJob.jId === job.jId
                    ? "joblisting-item-selected"
                    : ""
                }`}
                onClick={() => handleJobClick(job)}
              >
                <JobItem
                  jobTitle={job.title}
                  jobLocation={job.location}
                  postDate={job.postdate}
                />
              </div>
            ))}
          </div>
        )}
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
