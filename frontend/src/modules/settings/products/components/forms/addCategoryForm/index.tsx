import {Button, ButtonGroup, Grid} from "@mui/material";
import Input from "components/input";
import { ModalBoxContext } from "components/modalBox/providers/modalBox";
import { useContext } from "react";
import { useForm } from "react-hook-form";
import { useMutation } from "react-query";
import { addCategory } from "api/actions/settings.actions";

type AddCategoryFormValues = {
  name: string;
  userId: number;
};


interface onMenuEditProps {
  onMenuEdit: () => void;
}
const AddCategoryForm: React.FC<onMenuEditProps> = (props) =>  {
  const { actionModalBox } = useContext(ModalBoxContext);
  const {
    handleSubmit,
    register,
    formState: { errors },
  } = useForm({
    defaultValues: {
      name: "",
      userId: 1,
    },
  });
  const { onMenuEdit } = props;

  const {   mutate } = useMutation(addCategory, {
    onSuccess: () => {
      onMenuEdit()
      actionModalBox(false, null, "");
    },
    onError: (err: any) => {
      alert(`Nie udało się - ${err.response.data}`);
    },
  });

  const onSubmit = async (data: AddCategoryFormValues) => {
    mutate(data);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <Grid container spacing={2}>
        <Grid item xs={6}>
          <Input id="name" label="Nazwa" placeholder="Nazwa" register={register}  required />
        </Grid>
        <Grid item xs={6}></Grid>
      </Grid>
      <Grid container justifyContent="flex-end">
        <ButtonGroup variant="outlined">
          <Button onClick={() => actionModalBox(false, null, "")}>Anuluj</Button>
          <Button variant="contained" type="submit">
            Dodaj
          </Button>
        </ButtonGroup>
      </Grid>
    </form>
  );
};

export default AddCategoryForm;
