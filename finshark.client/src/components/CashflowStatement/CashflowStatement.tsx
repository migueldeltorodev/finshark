import { useOutletContext } from "react-router";
import { CompanyCashFlow } from "../../company";
import { useEffect, useState } from "react";
import { getCashflowStatement } from "../../api";
import Table from "../Table/Table";
import Spinner from "../Spinner/Spinner";
import { formatLargeMonetaryNumber } from "../Helpers/NumberFormatting";

type Props = {};

const config = [
  {
    label: "Date",
    render: (company: CompanyCashFlow) => company.date,
  },
  {
    label: "Operating Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.operatingCashFlow),
  },
  {
    label: "Investing Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.netCashUsedForInvestingActivites),
  },
  {
    label: "Financing Cashflow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(
        company.netCashUsedProvidedByFinancingActivities
      ),
  },
  {
    label: "Cash At End of Period",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.cashAtEndOfPeriod),
  },
  {
    label: "CapEX",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.capitalExpenditure),
  },
  {
    label: "Issuance Of Stock",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.commonStockIssued),
  },
  {
    label: "Free Cash Flow",
    render: (company: CompanyCashFlow) =>
      formatLargeMonetaryNumber(company.freeCashFlow),
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
        <Spinner />
      )}
    </>
  );
}

export default CashflowStatement;
