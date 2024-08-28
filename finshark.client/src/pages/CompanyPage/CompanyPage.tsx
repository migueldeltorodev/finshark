import { useParams } from "react-router";
import { CompanyProfile } from "../../company";
import { useEffect, useState } from "react";
import { getCompanyProfile } from "../../api";
import Sidebar from "../../components/Sidebar/Sidebar";
import CompanyDashboard from "../../components/CompanyDashboard/CompanyDashboard";
import Title from "../../components/Title/Title";

interface Props {}

function CompanyPage({}: Props) {
  //en este caso lo que debe devolver es el nombre de la compañía, useParagrams() se usa para retornar
  //parametros dinamicos en tu URL actual
  let { ticker } = useParams();
  const [company, setCompany] = useState<CompanyProfile>();

  useEffect(() => {
    const getProfileInit = async () => {
      const result = await getCompanyProfile(ticker!);
      setCompany(result?.data[0]);
    };
    getProfileInit();
  }, []);

  return (
    <>
      {company ? (
        <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
          <Sidebar />
          <CompanyDashboard ticker={ticker!}>
            <Title title="Company Name" subtitle={company.companyName} />
            <Title title="Price" subtitle={company.price.toString()} />
            <Title title="Sector" subtitle={company.sector} />
            <Title title="DCF" subtitle={company.dcf.toString()} />
            <p className="bg-white shadow rounded text-medium text-gray-900 p-3 mt-1 m-4">
              {company.description}
            </p>
          </CompanyDashboard>
        </div>
      ) : (
        <div>Company Not Found!</div>
      )}
    </>
  );
}

export default CompanyPage;
