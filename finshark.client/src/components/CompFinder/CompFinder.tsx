import { useEffect, useState } from "react";
import { CompanyCompData } from "../../company";
import { getCompData } from "../../api";
import CompFinderItem from "./CompFinderItem/CompFinderItem";
import Spinner from "../Spinner/Spinner";

type Props = {
  ticker: string;
};

function CompFinder({ ticker }: Props) {
  const [companyData, setCompanyData] = useState<CompanyCompData>();

  useEffect(() => {
    const getCompsData = async () => {
      const value = await getCompData(ticker);
      setCompanyData(value?.data[0]);
    };
    getCompsData();
  }, [ticker]);

  return (
    <div className="inline-flex rounded-md shadow-sm m-4">
      {companyData ? (
        companyData?.peersList.map((ticker) => {
          return <CompFinderItem ticker={ticker} />;
        })
      ) : (
        <Spinner />
      )}
    </div>
  );
}

export default CompFinder;
