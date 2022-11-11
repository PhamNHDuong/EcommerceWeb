import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Link, Navigate } from "react-router-dom";
import { LogIn, Register } from "../../service/AccountService.js";
import { TextField } from "@mui/material";

const SignIn = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [role, setRole] = useState();

  useEffect(() => {
    localStorage.clear();
  }, []);

  const onSubmit = (data) => {
    LogIn(data)
      .then((res) => {
        sessionStorage.clear();
        localStorage.setItem("token", res.data.accessToken);
        localStorage.setItem("accId", res.data.accId);
        localStorage.setItem("username", res.data.username);
        setRole(res.data.role);
      })
      .catch((err) => {
        console.log(err);
      });
  };
  return (
    <div className="log-in d-flex flex-column justify-content-center align-items-center">
      {role === "USER" && <Navigate to={"/"}></Navigate>}
      {role === "ADMIN" && <Navigate to={"/admin"}></Navigate>}
      <div className="label">LOG IN</div>
      <div className="form-log-in" style={{ width: "30rem" }}>
        <form onSubmit={handleSubmit(onSubmit)} className=" d-flex flex-column">
          <label>Username</label>
          <input
            {...register("username", {
              required: true,
              maxLength: 20,
            })}
            type="text"
            style={{ marginBottom: ".5rem" }}
          />
          {errors?.firstName?.type === "required" && (
            <p>This field is required</p>
          )}
          {errors?.firstName?.type === "maxLength" && (
            <p>First name cannot exceed 20 characters</p>
          )}
          <label>Password</label>
          <input
            {...register("password", {
              required: true,
              maxLength: 20,
            })}
            type="password"
            style={{ marginBottom: ".5rem" }}
          />
          {errors?.lastName?.type === "required" && (
            <p>This field is required</p>
          )}
          {errors?.lastName?.type === "maxLength" && (
            <p>First name cannot exceed 20 characters</p>
          )}
          <input type="submit" className="btn btn-primary" value={"Log In"} />
          <small className="text-center">
            <Link to={"/register"}>Create New Account</Link>
          </small>
        </form>
      </div>
    </div>
  );
};
// TODO: check is exist username in db
const SignUp = () => {
  const { register, handleSubmit } = useForm();
  const [error, setError] = useState("");
  const [role, setRole] = useState();

  const onSubmit = (data) => {
    console.log(data);
    Register(data)
      .then((res) => {
        sessionStorage.clear();
        localStorage.setItem("token", res.data.accessToken);
        localStorage.setItem("accId", res.data.accId);
        localStorage.setItem("username", res.data.username);
        setRole(res.data.role);
      })
      .catch((error) => {
        setError("Can not register");
        console.log(error);
      });
  };
  return (
    <>
      {role === "USER" && <Navigate to={"/"}></Navigate>}
      {role === "ADMIN" && <Navigate to={"/admin"}></Navigate>}
      <div className="register">
        <div
          className="form w-100 d-flex justify-content-center"
          style={{ marginTop: "2rem" }}
        >
          {error && <div className="text-danger">{error}</div>}
          <form
            onSubmit={handleSubmit(onSubmit)}
            className="d-flex flex-column w-25"
          >
            <TextField
              type="email"
              id="outlined-basic"
              label="Email"
              variant="outlined"
              style={{ marginBottom: "1rem" }}
              {...register("email", { required: true, maxLength: 30 })}
            />
            <TextField
              type="text"
              id="outlined-basic"
              label="Username"
              variant="outlined"
              style={{ marginBottom: "1rem" }}
              {...register("username", { required: true, maxLength: 20 })}
            />
            <TextField
              id="outlined-basic"
              label="Password"
              variant="outlined"
              style={{ marginBottom: "1rem" }}
              type="password"
              {...register("password", { required: true, maxLength: 20 })}
            />
            <TextField
              id="outlined-basic"
              label="Address"
              variant="outlined"
              style={{ marginBottom: "1rem" }}
              type="text"
              {...register("address", { required: true, maxLength: 20 })}
            />
            <TextField
              id="outlined-basic"
              label="Phone Number"
              variant="outlined"
              style={{ marginBottom: "1rem" }}
              type="number"
              {...register("phone", {
                required: true,
              })}
            />
            <input
              type="submit"
              className="btn btn-primary"
              value={"Register"}
            />
          </form>
        </div>
      </div>
    </>
  );
};

export { SignIn, SignUp };
