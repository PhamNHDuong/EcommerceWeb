import Home from "./pages/home/Home";
import Login from "./pages/login/Login";
import List from "./pages/list/List";
import Single from "./pages/single/Single";
import New from "./pages/new/New";
import {
  BrowserRouter,
  Routes,
  Route,
  useParams,
} from "react-router-dom";

function App() {
  let { productId, categoryId } = useParams();
  let a = 2;
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path="login" index element={<Login />} />
          <Route exact path="/">
            <Route index element={<Home />} />
            <Route path="users">
              <Route index element={<List matrix="user" />} />
            </Route>
            <Route path="products">
              <Route index element={<List matrix="product" />} />
              <Route path=":productId" element={<Single matrix="product" />} />
              <Route path="new" element={<New matrix="product" />} />
            </Route>
            <Route path="categories">
              <Route index element={<List matrix="category" />} />
              <Route
                path=":categoryId"
                element={<Single matrix="category" />}
              />
              <Route path="new" element={<New matrix="category" />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
