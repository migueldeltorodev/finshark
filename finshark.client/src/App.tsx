import { ChangeEvent, SyntheticEvent, useState } from "react";
import CardList from "./components/CardList/CardList";
import Search from "./components/Search/Search";
import { CompanySearch } from "./company";
import { searchCompanies } from "./api";
import ListPortfolio from "./components/Portfolio/ListPortfolio/ListPortfolio";
import Navbar from "./components/Navbar/Navbar";
//import Hero from "./components/Hero/Hero";

function App() {
  //variable para almacenar lo que va a buscar el usuario
  const [search, setSearch] = useState("");
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);

  //variable para almacenar los resultados de las busquedas de los usuarios
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  //variable para almacenar los errores que devuelve el servidor a las consultas, el error
  //puede ser almacenado como un null, como cuando se instancia, o puede ser también un string
  //es buena práctica declarar especificamente la mayor cantidad de cosas posibles para facilitar el trabajo a JS
  const [serverError, setServerError] = useState<string | null>(null);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    //la palabra clave any permite que el parametro e sea de cualquier tipo
    setSearch(e.target.value);
    console.log(e);
  };

  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    const exists = portfolioValues.find((value) => value === e.target[0].value);
    if (exists) return;
    const updatedPortfolio = [...portfolioValues, e.target[0].value];
    setPortfolioValues(updatedPortfolio);
  };

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    const removed = portfolioValues.filter((value) => {
      return value !== e.target[0].value;
    });
    setPortfolioValues(removed);
  };

  //Esto es para validar mejor lo que obtenemos de las respuestas, es decir, si recibimos un arreglo
  //CompanySearch[] que es lo que debe devolver el metodo searchCompanies si se ejecuta correctamente
  //entonces se ejecuta la linea setSearchResult(result.data) y almacenamos los datos que obtuvimos
  //de lo contrario, searchCompanies devuelve un string con algun mensaje de error, por lo que entonces
  //almacenamos dicho error con setServerError.
  const onSearchSubmit = async (e: SyntheticEvent) => {
    //SyntheticEvent es como un evento generico
    e.preventDefault();
    const result = await searchCompanies(search);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
    console.log(searchResult);
  };

  return (
    <div className="App">
      <Navbar />
      {/* <Hero /> */}
      <Search
        onSearchSubmit={onSearchSubmit}
        handleSearchChange={handleSearchChange}
        search={search}
      />
      <ListPortfolio
        portfolioValues={portfolioValues}
        onPortfolioDelete={onPortfolioDelete}
      />
      {serverError && <h1>{serverError}</h1>}
      <CardList
        searchResults={searchResult}
        onPortfolioCreate={onPortfolioCreate}
      />
    </div>
  );
}

export default App;
