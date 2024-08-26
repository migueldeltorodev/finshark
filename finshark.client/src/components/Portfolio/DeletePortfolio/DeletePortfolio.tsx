import React, { SyntheticEvent } from "react";

interface Props {
  onPortfolioDelete: (e: SyntheticEvent) => void;
  portfolioValue: string;
}

function DeletePortfolio({ onPortfolioDelete, portfolioValue }: Props) {
  return (
    <div>
      <form onSubmit={onPortfolioDelete}>
        <input type="text" hidden={true} value={portfolioValue} />
        <button>X</button>
      </form>
    </div>
  );
}

export default DeletePortfolio;
