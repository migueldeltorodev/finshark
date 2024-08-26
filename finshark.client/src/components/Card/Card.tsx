import { SyntheticEvent } from "react";
//import image from "../../../images/Z y T.png";
import { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";
import "./Card.css";

interface Props {
  id: string;
  searchResult: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({
  id,
  searchResult,
  onPortfolioCreate,
}: Props): JSX.Element => {
  return (
    <div className="card" id={id}>
      <img alt="company logo" className="card-img-top" />
      <div className="card-body">
        <h5 className="card-title">
          {searchResult.name}({searchResult.symbol})
        </h5>
        <p className="card-text">
          <span className="fw-bold">Currency:</span> {searchResult.currency}
        </p>
        <p className="card-text">
          <span className="fw-bold">Exchange:</span>
          {searchResult.exchangeShortName} - {searchResult.stockExchange}
        </p>
        <AddPortfolio
          onPortfolioCreate={onPortfolioCreate}
          symbol={searchResult.symbol}
        />
      </div>
    </div>
  );
};

export default Card;
