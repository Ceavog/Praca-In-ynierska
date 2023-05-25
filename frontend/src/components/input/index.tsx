import { TextField, StandardTextFieldProps } from "@mui/material";

type Props = StandardTextFieldProps & {
  id: string;
  register?: any;
  minLength?: number;
  maxLength?: number;
};

const Input: React.FC<Props> = ({ id, maxLength, register, variant = "outlined", ...props }) => {
  return (
    <TextField
      id={id}
      variant={variant}
      {...props}
      {...register(id)}
    />
  );
};

export default Input;
