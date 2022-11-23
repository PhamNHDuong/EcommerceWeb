import { useState } from "react";
import { loginAdmin } from "../../services/userService";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import "./login.scss";

const Login = () => {
  const [errorMessages, setErrorMessages] = useState({});
  const [isLogin, setIsLogin] = useState(false);

  let nav = useNavigate();

  const errors = {
    uname: "invalid username",
    pass: "invalid password",
  };

  const handleSubmit = (event) => {
    //Prevent page reload
    event.preventDefault();

    var { uname, pass } = document.forms[0];
    const logindata = {
      username: uname.value,
      password: pass.value,
    };

    // Find user login info
    const res = loginAdmin(logindata);
    res
      .then((res) => {
        if (res.data.userInfo.role == "Admin") {
          Cookies.set("role", res.data.userInfo.role);
          Cookies.set("username", uname.value);
          alert("Login success");
          setIsLogin(true);
          nav("/");
        }
        else{
          alert("You are not allowed to log in!")
        }
      })
      .catch((err) => {
        alert("Fail to login");
      });
  };

  const renderErrorMessage = (name) =>
    name === errorMessages.name && (
      <div className="error">{errorMessages.message}</div>
    );

  return (
    <div>
      <h1 className="title"> Login </h1>
      <div className="form">
        <form onSubmit={handleSubmit}>
          <div className="input">
            <label>Username </label>
            <input type="text" name="uname" required />
            {renderErrorMessage("uname")}
          </div>
          <div className="input">
            <label>Password </label>
            <input type="password" name="pass" required />
            {renderErrorMessage("pass")}
          </div>
          <div className="btn">
            <button type="submit">Login </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
