export const fetchJobs = async (page, pageSize = 10) => {
  const endpoint = `${process.env.REACT_APP_API_BASE_URL}/job?page=${page}&pagesize=${pageSize}`;
  const options = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };
  try {
    const response = await fetch(endpoint, options);
    if (!response.ok) {
      throw new Error("Failed to fetch jobs");
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching jobs:", error);
    throw error;
  }
};

export const fetchJobById = async (id) => {
  const endpoint = `${process.env.REACT_APP_API_BASE_URL}/job/${id}`;
  const options = {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  };
  try {
    const response = await fetch(endpoint, options);
    if (!response.ok) {
      throw new Error("Failed to fetch job");
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching job:", error);
    throw error;
  }
};
