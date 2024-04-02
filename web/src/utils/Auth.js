export const registerUser = async (email, password, fname, lname) => {
  const endpoint = `${process.env.REACT_APP_API_BASE_URL}/user/register`;
  const options = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password, fname, lname }),
  };
  try {
    const response = await fetch(endpoint, options);
    if (!response.ok) {
      var errorJson = await response.json();
      throw new Error(
        `Oops, registration failed, \nresponse status: ${
          response.status
        }, \n reason: ${JSON.stringify(errorJson.errors) || "Unknown"}`
      );
    }

    const data = await response.json();
    return { success: true, message: "Registration successful", data };
  } catch (error) {
    return { success: false, message: error.message };
  }
};
