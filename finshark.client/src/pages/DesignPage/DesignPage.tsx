import React from "react";
import Table from "../../components/Table/Table";

type Props = {};

function DesignPage({}: Props) {
  return (
    <>
      <h1>FinShark Design Page</h1>
      <h2>
        This is FinShark's Design Page. This is where we well house various
        design aspect of the app
      </h2>
      <Table />
    </>
  );
}

export default DesignPage;
