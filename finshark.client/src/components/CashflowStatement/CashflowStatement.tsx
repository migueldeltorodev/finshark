import { useOutletContext } from "react-router";
import { CompanyCashFlow } from "../../company";
import { useEffect, useState } from "react";
import { getCashflowStatement } from "../../api";
import Table from "../Table/Table";

type Props = {};

const config = [
  {
    label: "Date",
    render: (company: CompanyCashFlow) => company.date,
  },
  {
    label: "Operating Cashflow",
    render: (company: CompanyCashFlow) => company.operatingCashFlow,
  },
  {
    label: "Investing Cashflow",
    render: (company: CompanyCashFlow) =>
      company.netCashUsedForInvestingActivites,
  },
  {
    label: "Financing Cashflow",
    render: (company: CompanyCashFlow) =>
      company.netCashUsedProvidedByFinancingActivities,
  },
  {
    label: "Cash At End of Period",
    render: (company: CompanyCashFlow) => company.cashAtEndOfPeriod,
  },
  {
    label: "CapEX",
    render: (company: CompanyCashFlow) => company.capitalExpenditure,
  },
  {
    label: "Issuance Of Stock",
    render: (company: CompanyCashFlow) => company.commonStockIssued,
  },
  {
    label: "Free Cash Flow",
    render: (company: CompanyCashFlow) => company.freeCashFlow,
  },
];

function CashflowStatement({}: Props) {
  const ticker = useOutletContext<string>();
  const [cashflowdata, setCashflowdata] = useState<CompanyCashFlow[]>();

  useEffect(() => {
    const getCashflow = async () => {
      const value = await getCashflowStatement(ticker!);
      setCashflowdata(value!.data);
    };
    getCashflow();
  }, []);

  return (
    <>
      {cashflowdata ? (
        <Table configs={config} incomeData={cashflowdata} />
      ) : (
        <h1>No Results!</h1>
      )}
    </>
  );
}

export default CashflowStatement;
