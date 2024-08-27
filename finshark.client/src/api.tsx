import axios from "axios";
import { CompanyProfile, CompanySearch } from "./company";

interface SearchResponse {
  data: CompanySearch[];
}

export const searchCompanies = async (query: string) => {
  try {
    const apikey = process.env.REACT_APP_API_KEY;
    const searchedData = await axios.get<SearchResponse>(
      `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&exchange=NASDAQ&apikey=${apikey}`
    );
    return searchedData;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log("error message: ", error.message);
      return error.message;
    } else {
      console.log("unexpected error: ", error);
      return "An unexpected error occured";
    }
  }
};

export const getCompanyProfile = async (query: string) => {
  try {
    const dataOfCompanies = await axios.get<CompanyProfile[]>(
      `https://financialmodelingprep.com/api/v3/profile/${query}?apikey=${process.env.REACT_APP_API_KEY}`
    );
    return dataOfCompanies;
  } catch (error: any) {
    console.log("error message from API: ", error.message);
  }
};
