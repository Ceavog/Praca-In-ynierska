import React from "react";
import { IUser } from "../types";

type State = {
  authUser: IUser | null;
};

type Action = {
  type: string;
  payload: IUser | null;
};

type Dispatch = (action: Action) => void;

const initialState: State = {
  authUser: null,
};

type UserProviderProps = { children: React.ReactNode };
const stateReducer = (state: State, action: Action) => {
  switch (action.type) {
    case "SET_USER": {
      return {
        ...state,
        authUser: action.payload,
      };
    }
    default: {
      throw new Error(`Unhandled action type`);
    }
  }
};

const UserContext = React.createContext<{ state: State; dispatch: Dispatch } | undefined>(undefined);

const UserProvider = ({ children }: UserProviderProps) => {
  const [state, dispatch] = React.useReducer(stateReducer, initialState);
  const value = { state, dispatch };
  return <UserContext.Provider value={value}>{children}</UserContext.Provider>;
};
const useUser = () => React.useContext(UserContext);
export { UserProvider, useUser };
