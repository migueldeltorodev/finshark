import React, { SyntheticEvent } from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";

interface Props {
  portfolioValue: string;
  onPortfolioDelete: (e: SyntheticEvent) => void;
}

function CardsPortfolio({ portfolioValue, onPortfolioDelete }: Props) {
  return (
    <>
      <h4>{portfolioValue}</h4>
      <DeletePortfolio
        onPortfolioDelete={onPortfolioDelete}
        portfolioValue={portfolioValue}
      ></DeletePortfolio>
    </>
  );
}

export default CardsPortfolio;
