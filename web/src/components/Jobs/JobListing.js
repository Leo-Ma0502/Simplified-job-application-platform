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
  const [pageRender, setPageRender] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [industry, setIndustry] = useState("");
  const [title, setTitle] = useState("");
  const [search, setSearch] = useState(false);
  const [triggerRequest, setTrigger] = useState(false);

  const getJobs = useCallback(async () => {
    if (!triggerRequest) {
      return;
    }
    console.log("page: ", pageRender);
    console.log("searching: ", search);
    setLoading(true);
    try {
      const jobsData = await fetchJobs(pageRender, 5, null, industry, title);
      search && pageRender === 1 && setJobsSearch([]);
      if (jobsData.length === 0) {
        setHasMore(false);
      } else {
        search &&
          setJobsSearch((prevJobs) => {
            const newJobs = jobsData.filter(
              (job) => !prevJobs.some((prevJob) => prevJob.jId === job.jId)
            );
            return [...prevJobs, ...newJobs];
          });

        !search &&
          setJobsNormal((prevJobs) => {
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
      setHasMore(true);
    }
  }, [pageRender, industry, title, search, triggerRequest]);

  useEffect(() => {
    getJobs();
  }, [getJobs]);

  useEffect(() => {
    setTrigger(true);
  }, []);

  useEffect(() => {
    setJobsRender(search ? jobsSearch : jobsNormal);
    setPageRender(search ? pageSearch : pageNormal);
  }, [search, jobsNormal, jobsSearch, pageSearch, pageNormal]);

  const handleScroll = () => {
    if (
      window.innerHeight + window.scrollY >= document.body.offsetHeight &&
      !loading &&
      hasMore
    ) {
      console.log("scroll");
      search
        ? setPageSearch((prevPage) => prevPage + 1)
        : setPageNormal((prevPage) => prevPage + 1);
      setTrigger(true);
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
    if (title || industry) {
      setSearch(!search);
      setTrigger(true);
      setPageNormal(1);
      setPageSearch(1);
    }
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
            <button type="Submit">{search ? "Back" : "Search"}</button>
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
