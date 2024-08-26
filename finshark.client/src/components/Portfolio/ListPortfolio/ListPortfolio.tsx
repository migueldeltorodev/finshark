import React, { SyntheticEvent } from "react";
import CardsPortfolio from "../CardPortfolio/CardsPortfolio";

interface Props {
  portfolioValues: string[];
  onPortfolioDelete: (e: SyntheticEvent) => void;
}

function ListPortfolio({ portfolioValues, onPortfolioDelete }: Props) {
  return (
    <>
      <h3>My Portfolio</h3>
      <ul>
        {portfolioValues &&
          portfolioValues.map((portfolioValue) => {
            return (
              <CardsPortfolio
                portfolioValue={portfolioValue}
                onPortfolioDelete={onPortfolioDelete}
              />
            );
          })}
      </ul>
    </>
  );
}

export default ListPortfolio;
