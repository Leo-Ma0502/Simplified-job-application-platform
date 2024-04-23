import React, { useCallback, useEffect, useState } from "react";
import JobItem from "./JobItem";
import JobDetail from "./JobDetail";
import "./JobListing.css";
import { fetchJobs } from "../../services/jobService";

function JobListing() {
  const [selectedJob, setSelectedJob] = useState(null);
  const [showDetail, setShowDetail] = useState(false);
  const [jobsNormal, setJobsNormal] = useState([]);
  const [jobsSearch, setJobsSearch] = useState([]);
  const [jobsRender, setJobsRender] = useState([]);
  const [loading, setLoading] = useState(true);
  const [pageNormal, setPageNormal] = useState(1);
  const [pageSearch, setPageSearch] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [industry, setIndustry] = useState("");
  const [title, setTitle] = useState("");
  const [search, setSearch] = useState(false);
  const [triggerRequest, setTrigger] = useState(false);

  const getJobs = useCallback(async () => {
    if (!triggerRequest) {
      return;
    }
    setLoading(true);
    try {
      const jobsData = await fetchJobs(
        search ? pageSearch : pageNormal,
        5,
        null,
        industry,
        title
      );
      if (jobsData.length === 0) {
        setHasMore(false);
      } else {
        const updateFunction = search ? setJobsSearch : setJobsNormal;
        updateFunction((prevJobs) => {
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
      setTrigger(false);
    }
  }, [pageNormal, pageSearch, industry, title, search, triggerRequest]);

  useEffect(() => {
    getJobs();
  }, [getJobs]);

  useEffect(() => {
    setTrigger(true);
  }, []);

  const handleScroll = useCallback(() => {
    if (
      window.innerHeight + window.scrollY >= document.body.offsetHeight - 100 &&
      !loading &&
      hasMore
    ) {
      const nextPage = search ? pageSearch + 1 : pageNormal + 1;
      search ? setPageSearch(nextPage) : setPageNormal(nextPage);
      setTrigger(true);
    }
  }, [loading, hasMore, pageSearch, pageNormal, search]);

  useEffect(() => {
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, [handleScroll]);

  useEffect(() => {
    setJobsRender(search ? jobsSearch : jobsNormal);
  }, [search, jobsNormal, jobsSearch]);

  const handleJobClick = (job) => {
    setSelectedJob(job);
    setShowDetail(true);
  };

  const handleCloseDetail = () => {
    setShowDetail(false);
  };

  const handleSearch = (e) => {
    e.preventDefault();
    if (industry || title) {
      setSearch(!search);
      setTrigger(true);
    }
    if (!search) {
      setPageSearch(1);
      setJobsSearch([]);
    } else {
      setPageNormal(1);
    }
    setHasMore(true);
  };

  return (
    <div className="joblisting-container">
      <div className="joblisting-body">
        <div className="joblisting-left">
          <form className="joblisting-search" onSubmit={handleSearch}>
            <input
              type="search"
              placeholder="Search by job title"
              onChange={(e) => {
                e.preventDefault();
                setTitle(e.target.value.trim());
                setSearch(false);
              }}
              value={title}
            />
            <input
              type="search"
              placeholder="Search by industry"
              onChange={(e) => {
                e.preventDefault();
                setIndustry(e.target.value.trim());
                setSearch(false);
              }}
              value={industry}
            />
            <button type="submit">{search ? "Back" : "Search"}</button>
          </form>
          <div className="joblisting-list" onScroll={handleScroll}>
            {jobsRender.length !== 0 &&
              jobsRender.map((job) => (
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
            {!hasMore && <p>No more jobs available</p>}
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
