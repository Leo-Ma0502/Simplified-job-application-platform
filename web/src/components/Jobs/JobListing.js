import React, { useCallback, useEffect, useState } from "react";
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
  const [industry, setIndustry] = useState("");
  const [title, setTitle] = useState("");
  const [search, setSearch] = useState(false);

  const getJobs = useCallback(async () => {
    console.log("hiiiiii");
    if (search) {
      console.log("search ....");
      setLoading(true);
      try {
        const jobsData = await fetchJobs(page, 5, null, industry, title);
        if (jobsData.length === 0) {
          setHasMore(false);
        } else {
          setJobs((prevJobs) => {
            const newJobs = jobsData.filter(
              (job) => !prevJobs.some((prevJob) => prevJob.jId === job.jId)
            );
            return [...prevJobs, ...newJobs];
          });
        }
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
        setSearch(false);
      }
    }
  }, [page, industry, title, search]);

  useEffect(() => {
    getJobs();
  }, [getJobs]);

  useEffect(() => {
    setSearch(true);
  }, []);

  const handleScroll = () => {
    if (
      window.innerHeight + window.scrollY >= document.body.offsetHeight &&
      !loading &&
      hasMore
    ) {
      setPage((prevPage) => prevPage + 1);
      setSearch(true);
    }
  };

  const handleJobClick = (job) => {
    setSelectedJob(job);
    setShowDetail(true);
  };

  const handleCloseDetail = () => {
    setShowDetail(false);
  };

  const handleSearch = async (e) => {
    e.preventDefault();
    setJobs([]);
    // setPage(page);
    setSearch(true);
  };

  return (
    <div className="joblisting-container">
      <div className="joblisting-body">
        <div className="joblisting-left">
          <form className="joblisting-search" onSubmit={handleSearch}>
            <input
              type="search"
              placeholder="Search by job title"
              onChange={(e) => setTitle(e.target.value.trim())}
              value={title}
            />
            <input
              type="search"
              placeholder="Search by industry"
              onChange={(e) => setIndustry(e.target.value.trim())}
              value={industry}
            />
            <button type="Submit">Search</button>
          </form>
          <div className="joblisting-list" onScroll={handleScroll}>
            {jobs.length !== 0 &&
              jobs.map((job) => (
                <div
                  key={job.jId}
                  className={`joblisting-item ${
                    selectedJob && selectedJob.jId === job.jId
                      ? "joblisting-item-selected"
                      : ""
                  }`}
                  onClick={() => handleJobClick(job)}
                >
                  {job && (
                    <JobItem
                      jobTitle={job.title}
                      jobLocation={job.location}
                      postDate={job.postdate}
                    />
                  )}
                </div>
              ))}
            <p>No more jobs available</p>
          </div>
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
