import { FC } from "react";
import { useFormContext, Controller } from "react-hook-form";
import { TextField, TextFieldProps } from "@mui/material";
import { styled } from "@mui/material/styles";

const CssTextField = styled(TextField)({
    "& .MuiOutlinedInput-root": {
        "& fieldset": {
            borderColor: "#c8d0d4",
        },
        "&:hover fieldset": {
            borderColor: "#c8d0d4",
        },
        "&.Mui-focused fieldset": {
            borderColor: "#c8d0d4",
        },
    },
});

type FormInputProps = {
  name: string;
} & TextFieldProps;

const FormInput: FC<FormInputProps> = ({ name, ...otherProps }) => {
  const {
    control
  } = useFormContext();

  return (
      <Controller
          control={control}
          name={name}
          defaultValue=""
          render={({ field }) => (
              <CssTextField
                  {...field}
                  {...otherProps}
                  variant="outlined"
                  sx={{ mb: "1.5rem" }}
              />
          )}
      />
  );
};

export default FormInput;
