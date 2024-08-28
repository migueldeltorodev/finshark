import RatioList from "../../components/RatioList/RatioList";
import Table from "../../components/Table/Table";
import { testIncomeStatementData } from "../../components/Table/testData";

type Props = {};

const tableConfig = [
  {
    label: "Market Cap",
    render: (company: any) => company.marketCapTTM,
    subTitle: "Total value of all a company's shares of stock",
  },
];

function DesignGuide({}: Props) {
  return (
    <>
      <h1>FinShark Design Page</h1>
      <h2>
        This is FinShark's Design Page. This is where we will house various
        design aspect of the app
      </h2>
      <RatioList data={testIncomeStatementData} config={tableConfig} />
      <Table />
    </>
  );
}

export default DesignGuide;
