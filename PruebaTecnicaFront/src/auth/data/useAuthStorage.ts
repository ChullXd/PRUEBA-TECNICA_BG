import { useBaseLocalStorage } from "../../data";
import { JWTInfo } from "../interfaces";

export const useAuthStorage = () => {
  const { SaveData, GetData, CheckData, RemoveData } = useBaseLocalStorage();
  const SaveJWTInfo = async (data: JWTInfo) => await SaveData(data, "token");
  const GetJWTInfo = async (): Promise<JWTInfo> =>
    await GetData<JWTInfo>("token");
  const CheckJWTInfo = async (): Promise<boolean> => await CheckData("token");
  const DeleteJWTInfo = async (): Promise<void> => await RemoveData(["token"]);
  return {
    SaveJWTInfo,
    GetJWTInfo,
    CheckJWTInfo,
    DeleteJWTInfo,
  };
};
