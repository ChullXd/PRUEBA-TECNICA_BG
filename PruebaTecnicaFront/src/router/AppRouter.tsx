import { useLocation } from "react-router-dom";
import { AuthRouter } from "../auth/router";
import { useLocalStorage } from "../data";
import { useEffect, useMemo } from "react";
import { useAuthStore } from "../shared";
import { ProjectRouter } from "../project/router/ProjectRouter";
import { LoadingScreen } from "../theme";

export const AppRouter = () => {
  const {
    status,
    onChecking,
    jwtInfo: { token },
  } = useAuthStore();
  const { SaveLastPath } = useLocalStorage();
  const { pathname, search } = useLocation();
  useMemo(
    async () => await SaveLastPath(pathname + search),
    [pathname, search, SaveLastPath]
  );

  useEffect(() => {
    onChecking();
  }, []);

  if (status === "checking") return <LoadingScreen />;

  return (
    <>
      {status === "not-authenticated" || (token ?? "").length === 0 ? (
        <AuthRouter />
      ) : (
        <ProjectRouter />
      )}
    </>
  );
};
