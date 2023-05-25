import React, { useState } from "react";

type ModalBoxType = {
  open: boolean;
  body: React.ReactNode;
  title: string;
  actionModalBox(openValue: boolean, body: React.ReactNode, title: string): void;
};

const initialData: ModalBoxType = {
  open: false,
  body: null,
  title: "",
  actionModalBox: () => null
};

export const ModalBoxContext = React.createContext<ModalBoxType>(initialData);

type Props = {
  children: React.ReactNode;
};

export const ModalBoxProvider: React.FC<Props> = (props) => {
  const [open, setOpen] = useState<boolean>(initialData.open);
  const [body, setBody] = useState<React.ReactNode>(null);
  const [title, setTitle] = useState<string>("");

  const actionModalBox = (openValue: boolean, body: React.ReactNode, title: string) => {
    setOpen(openValue);
    setBody(body);
    setTitle(title);
  };

  const values: ModalBoxType = {
    open,
    body,
    title,
    actionModalBox,
  };

  return <ModalBoxContext.Provider value={values}>{props.children}</ModalBoxContext.Provider>;
};
