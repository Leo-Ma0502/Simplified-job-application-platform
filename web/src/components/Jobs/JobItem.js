import React from "react";
import { formatDistanceToNow, parseISO } from "date-fns";

const JobItem = ({ jobTitle, jobLocation, postDate }) => {
  const date = parseISO(postDate);

  const formattedDate = formatDistanceToNow(date, { addSuffix: true });

  return (
    <div
      style={{
        padding: "10px",
        margin: "10px",
        border: "1px solid #ccc",
        borderRadius: "5px",
      }}
    >
      <h2>{jobTitle}</h2>
      <p>{jobLocation}</p>
      <p>Posted {formattedDate}</p>
    </div>
  );
};

export default JobItem;
