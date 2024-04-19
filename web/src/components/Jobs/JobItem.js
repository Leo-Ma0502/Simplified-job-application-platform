import React from "react";
import { formatDistanceToNow, parseISO } from "date-fns";

const JobItem = ({ jobTitle, jobLocation, postDate }) => {
  const date = parseISO(postDate);

  const formattedDate = formatDistanceToNow(date, { addSuffix: true });

  return (
    <div
      style={{
        alignContent: "center",
        color: "#fdfdfd",
      }}
    >
      <h2>{jobTitle}</h2>
      <p>{jobLocation}</p>
      <p>Posted {formattedDate}</p>
    </div>
  );
};

export default JobItem;
