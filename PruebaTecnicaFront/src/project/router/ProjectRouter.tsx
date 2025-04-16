import { Navigate, Route, Routes } from "react-router-dom";
import { HomePage } from "../pages";
import { ProjectLayout } from "../layout";
export const ProjectRouter = () => {
  return (
    <ProjectLayout>
      <div className="container">
        <Routes>
          <Route path="home" element={<HomePage />} />
          <Route path="/" element={<Navigate to="/home" />} />
          <Route path="/*" element={<Navigate to="/home" />} />
        </Routes>
      </div>
    </ProjectLayout>
  );
};
