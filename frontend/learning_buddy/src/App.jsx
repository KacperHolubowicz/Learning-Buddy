import './App.css';
import MainPage from "./pages/MainPage";
import SubjectListPage from './pages/subjects/SubjectListPage';
import SubjectPage from './pages/subjects/SubjectPage';
import MainLayout from "./templates/MainLayout";
import LoginPage from "./pages/auth/LoginPage";
import Authorize from "./logic/Authorize";
import LearningSourceListPage from "./pages/learning-sources/LearningSourceListPage";

import {
  createBrowserRouter,
  RouterProvider,
  Route,
  createRoutesFromElements,
} from "react-router-dom";
import NotFoundPage from './pages/auth/NotFoundPage';
import SubjectTaskListPage from './pages/subject-tasks/SubjectTaskListPage';
import LearningSourceCreatePage from './pages/learning-sources/LearningSourceCreatePage';
import LearningSourceDeletePage from './pages/learning-sources/LearningSourceDeletePage';

function App() {

  document.body.style.backgroundColor = "#E4F6FF";

  const mainRouter = createBrowserRouter(
    createRoutesFromElements([
      <Route path="/" element={<MainLayout />} >
        <Route index element={<MainPage />} />
        <Route path="subjects" element={<SubjectListPage />} />
        <Route path="subjects/:subjectId" element={<SubjectPage />} />
        <Route path="subjects/:subjectId/learning-sources" element={<LearningSourceListPage />} />
        <Route path="subjects/:subjectId/subject-tasks" element={<SubjectTaskListPage />} />
        <Route path="subjects/:subjectId/learning-sources/new" element={<LearningSourceCreatePage />} />
        <Route path="learning-sources/:learningSourceId/edit"/>
        <Route path="learning-sources/:learningSourceId/delete" element={<LearningSourceDeletePage />}/>
        <Route path="login" element={<LoginPage />} />
        <Route path="*" element={<NotFoundPage />} />
      </Route>
    ])
  );

  return <RouterProvider router={mainRouter}/>
}

export default App;
