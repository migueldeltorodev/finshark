import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import "./output.css";
import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";
import { RouterProvider } from "react-router-dom";
import { router } from "./routes/Routes.tsx";

//console.log(searchCompanies("tsla"));

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
