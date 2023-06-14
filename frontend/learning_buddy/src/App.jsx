import './App.css';
import MainPage from "./pages/MainPage";
import SubjectListPage from './pages/SubjectListPage';
import SubjectPage from './pages/SubjectPage';
import MainLayout from "./templates/MainLayout";
import LoginPage from "./pages/LoginPage";
import Authorize from "./logic/Authorize";
import LearningSourceListPage from "./pages/LearningSourceListPage";

import {
  createBrowserRouter,
  RouterProvider,
  Route,
  createRoutesFromElements,
} from "react-router-dom";
import NotFoundPage from './pages/NotFoundPage';

function App() {

  document.body.style.backgroundColor = "#E4F6FF";

  const mainRouter = createBrowserRouter(
    createRoutesFromElements([
      <Route path="/" element={<MainLayout />} >
        <Route index element={<MainPage />} />
        <Route path="subjects" element={<SubjectListPage />} />
        <Route path="subjects/:subjectId" element={<SubjectPage />} />
        <Route path="subjects/:subjectId/learning-sources" element={<LearningSourceListPage />} />
        <Route path="login" element={<LoginPage />} />
        <Route path="*" element={<NotFoundPage />} />
      </Route>
    ])
  );

  return <RouterProvider router={mainRouter}/>
}

export default App;
