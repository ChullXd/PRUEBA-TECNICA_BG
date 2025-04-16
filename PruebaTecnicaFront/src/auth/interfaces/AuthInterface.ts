export type AuthStatusType = "checking" | "not-authenticated" | "authenticated";

export interface JWTInfo {
  token: string;
  expiracion: string;
  userName: string;
}
export interface AuthInfo {
  jwtInfo: JWTInfo;
  status: AuthStatusType;
}

export interface LoginInfo {
  username: string;
  password: string;
}
