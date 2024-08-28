import { testIncomeStatementData } from "./testData";

const incomeData = testIncomeStatementData;

type Props = {};

type Company = (typeof incomeData)[0];

//Esta variable la creamos para solamente trabajar con 2 columnas, Year y Cost of Revenue
const configs = [
  {
    label: "Year",
    render: (company: Company) => company.acceptedDate,
  },
  {
    label: "Cost of Revenue",
    render: (company: Company) => company.costOfRevenue,
  },
];

function Table({}: Props) {
  //Mediante esta función se van a renderizar todos los datos de prueba en filas
  //Mediante la función .map se iteran los datos y se van creando filas
  //Termina con la creación de filas en renderedRows
  const renderedRows = incomeData.map((company) => {
    return (
      <tr key={company.cik}>
        {configs.map((val: any) => {
          return (
            <td className="p-4 whitespace-nowrap text-sm font-normal text-gray-900">
              {val.render(company)}
            </td>
          );
        })}
      </tr>
    );
  });
  //Lo mismo, pero esta crea el encabezado, en este caso solo 2 porque config tiene solo 2 objetos dentro
  const renderedHeaders = configs.map((config: any) => {
    return (
      <th
        className="p-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
        key={config.label}
      >
        {config.label}
      </th>
    );
  });
  //El componente devuelve la tabla utilizando los datos anteriores.
  return (
    <div className="bt-white shadow rounded-lg p-4 sm:p-6 xl:p-8">
      <table>
        <thead className="min-w-full divide-y divide-gray-200 m-5">
          {renderedHeaders}
        </thead>
        <tbody>{renderedRows}</tbody>
      </table>
    </div>
  );
}

export default Table;
