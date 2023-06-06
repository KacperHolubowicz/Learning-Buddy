import './App.css';
import MainPage from "./pages/MainPage";
import SubjectListPage from './pages/SubjectListPage';
import SubjectPage from './pages/SubjectPage';
import MainLayout from "./templates/MainLayout";
import LoginPage from "./pages/LoginPage";
import Authorize from "./logic/Authorize";
import {
  createBrowserRouter,
  RouterProvider,
  Route,
  createRoutesFromElements,
} from "react-router-dom";

function App() {

  document.body.style.backgroundColor = "#E4F6FF";

  const mainRouter = createBrowserRouter(
    createRoutesFromElements([
      <Route path="/" element={<MainLayout />} >
        <Route index element={<MainPage />} />
        <Route path="subjects" element={<SubjectListPage />} />
        <Route path="subjects/:id" element={<SubjectPage />} />
        <Route path="login" element={<LoginPage />} />
      </Route>
    ])
  );

  return <RouterProvider router={mainRouter}/>
}

export default App;
