import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../pages/HomePage/HomePage";
import CompanyPage from "../pages/CompanyPage/CompanyPage";
import SearchPage from "../pages/SearchPage/SearchPage";
import CompanyProfile from "../components/CompanyProfile/CompanyProfile";
import IncomeStatement from "../components/IncomeStatement/IncomeStatement";
import DesignGuide from "../pages/DesignGuide/DesignGuide";
import BalanceSheet from "../components/BalanceSheet/BalanceSheet";
import CashflowStatement from "../components/CashflowStatement/CashflowStatement";
import LoginPage from "../pages/LoginPage/LoginPage";
import RegisterPage from "../pages/RegisterPage/RegisterPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "login", element: <LoginPage /> },
      { path: "register", element: <RegisterPage /> },
      { path: "search", element: <SearchPage /> },
      { path: "design-guide", element: <DesignGuide /> },
      {
        path: "company/:ticker",
        element: <CompanyPage />,
        children: [
          { path: "company-profile", element: <CompanyProfile /> },
          { path: "income-statement", element: <IncomeStatement /> },
          { path: "balance-sheet", element: <BalanceSheet /> },
          { path: "cashflow-statement", element: <CashflowStatement /> },
        ],
      },
    ],
  },
]);
