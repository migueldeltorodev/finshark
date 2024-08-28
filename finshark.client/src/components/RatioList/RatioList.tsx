import { TestDataCompany } from "../Table/testData";

type Props = {};

//Estamos probando con solo 1 objeto, no con varias filas de los casos de prueba
const testDataCompany = TestDataCompany[0];

type Company = typeof testDataCompany;

const config = [
  {
    label: "Company Name",
    render: (company: Company) => company.companyName,
    subTitle: "This is the company name",
  },
];

function RatioList({}: Props) {
  const renderedRow = config.map((row) => {
    return (
      <li className="py-3 sm:py-4">
        <div className=".flex items-center space-x-4">
          <div className="flex-1 min-w-0">
            <p className="text-sm fonmt-medium text-gray-900 truncate">
              {row.label}
            </p>
            <p className="text-sm text-gray-500 truncate">
              {row.subTitle && row.subTitle}
            </p>
          </div>
          <div className="inline-flex items-center text-base font-semibold text-gray-900">
            {row.render(testDataCompany)}
          </div>
        </div>
      </li>
    );
  });
  return (
    <div className="bg-white shadow rounded-lg mb-4 p-4 sm:p-6 h-full">
      <ul className="divide-y divided-gray-200">{renderedRow}</ul>
    </div>
  );
}

export default RatioList;
