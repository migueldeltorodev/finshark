import RatioList from "../../components/RatioList/RatioList";
import Table from "../../components/Table/Table";

type Props = {};

function DesignGuide({}: Props) {
  return (
    <>
      <h1>FinShark Design Page</h1>
      <h2>
        This is FinShark's Design Page. This is where we will house various
        design aspect of the app
      </h2>
      <RatioList />
      <Table />
    </>
  );
}

export default DesignGuide;
