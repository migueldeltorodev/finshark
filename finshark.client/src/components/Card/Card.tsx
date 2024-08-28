import { SyntheticEvent } from "react";
import { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";
import "./Card.css";
import { Link } from "react-router-dom";

interface Props {
  idCard: string;
  searchResult: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

function Card({ idCard, searchResult, onPortfolioCreate }: Props) {
  return (
    <div
      className="flex flex-col items-center justify-between w-full p-6 bg-slate-100 rounded-lg md:flex-row"
      key={idCard}
      id={idCard}
    >
      <Link
        to={`/company/${searchResult.symbol}/company-profile`}
        className="font-bold text-center text-black md:text-left"
      >
        {searchResult.name} ({searchResult.symbol})
      </Link>
      <p className="text-black">{searchResult.currency}</p>
      <p className="font-bold text-black">
        {searchResult.exchangeShortName} - {searchResult.stockExchange}
      </p>
      <AddPortfolio
        onPortfolioCreate={onPortfolioCreate}
        symbol={searchResult.symbol}
      />
    </div>
  );
}

export default Card;
