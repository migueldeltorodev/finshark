import image from "../../../images/Z y T.png";

interface Props {
  companyName: string;
  ticker: string;
  price: number;
}

const Card: React.FC<Props> = ({
  companyName,
  ticker,
  price,
}: Props): JSX.Element => {
  return (
    <div className="Card">
      <img src={image} alt="Image" />
      <div className="details">
        <h2>
          {companyName}({ticker})
        </h2>
        <p>${price}</p>
      </div>
      <p className="info">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Explicabo hic
        eum mollitia impedit, ad sapiente voluptatum iure veritatis dolorum
        commodi ipsa molestiae soluta modi doloribus dolor ipsum voluptate nisi
        magni?
      </p>
    </div>
  );
};

export default Card;
