import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../pages/HomePage/HomePage";
import CompanyPage from "../pages/CompanyPage/CompanyPage";
import SearchPage from "../pages/SearchPage/SearchPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "search", element: <SearchPage /> },
      { path: "company/:ticker", element: <CompanyPage /> },
    ],
  },
]);
