export const RegisterUser = async (email, password, fname, lname) => {
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
        }, \nreason: ${JSON.stringify(errorJson.errors) || "Unknown"}`
      );
    }

    const data = await response.json();
    return { success: true, message: "Registration successful", data };
  } catch (error) {
    return { success: false, message: error.message };
  }
};

export const LoginUser = async (email, password) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_API_BASE_URL}/user/login`,
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      }
    );
    if (!response.ok) {
      throw new Error(
        `Oops, login failed, \nresponse status: ${response.status}, \nreason:${
          (await response.text()) || "Unknown"
        }`
      );
    }
    return { success: true, message: "Login successful" };
  } catch (error) {
    return { success: false, message: error.message };
  }
};
