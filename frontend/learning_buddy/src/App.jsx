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
import LearningSourceEditPage from './pages/learning-sources/LearningSourceEditPage';
import SubjectTaskCreatePage from './pages/subject-tasks/SubjectTaskCreatePage';
import SubjectTaskDeletePage from './pages/subject-tasks/SubjectTaskDeletePage';
import SubjectTaskEditPage from './pages/subject-tasks/SubjectTaskEditPage';
import SubjectCreatePage from './pages/subjects/SubjectCreatePage';

function App() {

  document.body.style.backgroundColor = "#E4F6FF";

  const mainRouter = createBrowserRouter(
    createRoutesFromElements([
      <Route path="/" element={<MainLayout />} >
        <Route index element={<MainPage />} />
        <Route path="subjects" element={<SubjectListPage />} />
        <Route path="subjects/:subjectId" element={<SubjectPage />} />
        <Route path="subjects/:subjectId/learning-sources" element={<LearningSourceListPage />} />
        <Route element={<Authorize />}>
          <Route path="subjects/:subjectId/edit" />
          <Route path="subjects/:subjectId/delete" />
          <Route path="subjects/new" element={<SubjectCreatePage />} />
          <Route path="subjects/:subjectId/subject-tasks" element={<SubjectTaskListPage />} />
          <Route path="subjects/:subjectId/learning-sources/new" element={<LearningSourceCreatePage />} />
          <Route path="subjects/:subjectId/subject-tasks/new" element={<SubjectTaskCreatePage />} />
          <Route path="learning-sources/:learningSourceId/edit" element={<LearningSourceEditPage />} />
          <Route path="learning-sources/:learningSourceId/delete" element={<LearningSourceDeletePage />} />
          <Route path="subject-tasks/:subjectTaskId/edit" element={<SubjectTaskEditPage />} />
          <Route path="subject-tasks/:subjectTaskId/delete" element={<SubjectTaskDeletePage />} />
        </Route>
        <Route path="login" element={<LoginPage />} />
        <Route path="*" element={<NotFoundPage />} />
      </Route>
    ])
  );

  return <RouterProvider router={mainRouter}/>
}

export default App;
