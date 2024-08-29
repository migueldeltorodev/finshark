import { Link } from "react-router-dom";

type Props = {
  ticker: string;
};

function CompFinderItem({ ticker }: Props) {
  return (
    <Link
      reloadDocument
      to={`/company/${ticker}/company-profile`}
      type="button"
      className="inline-flex items-center p-4 rounded-l-lg"
    >
      {ticker}
    </Link>
  );
}

export default CompFinderItem;
