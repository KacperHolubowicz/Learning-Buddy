import './App.css';
import MainPage from "./pages/MainPage";
import SubjectListPage from './pages/SubjectListPage';
import MainLayout from "./templates/MainLayout";
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
      </Route>
    ])
  );

  return <RouterProvider router={mainRouter}/>
}

export default App;
