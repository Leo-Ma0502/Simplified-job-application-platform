const Apply = ({ job }) => {
  return (
    <a
      href={`mailto:${job.email}?subject=Application for ${job.title}&body=Hi, I am interested in the position advertised.`}
      style={{
        display: "inline-block",
        padding: "10px 20px",
        margin: "10px 0",
        backgroundColor: "#4CAF50",
        color: "white",
        borderRadius: "5px",
        textDecoration: "none",
      }}
    >
      Apply by Email
    </a>
  );
};

export default Apply;
