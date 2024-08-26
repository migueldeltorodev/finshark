import React, { SyntheticEvent } from "react";

interface Props {
  onPortfolioCreate: (e: SyntheticEvent) => void;
  symbol: string;
}

function AddPortfolio({ onPortfolioCreate, symbol }: Props) {
  return (
    <form onSubmit={onPortfolioCreate}>
      <input type="text" readOnly={true} hidden={true} value={symbol} />
      <button type="submit">Add</button>
    </form>
  );
}

export default AddPortfolio;
