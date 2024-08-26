import { ChangeEvent, SyntheticEvent } from "react";

interface Props {
  onClick: (e: SyntheticEvent) => void;
  search: string | undefined;
  handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

function Search({ onClick, search, handleChange }: Props) {
  return (
    <div>
      <input value={search} type="text" onChange={(e) => handleChange(e)} />
      <button onClick={(e) => onClick(e)} />
    </div>
  );
}

export default Search;
