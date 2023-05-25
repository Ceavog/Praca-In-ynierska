import {
  Button,
  ButtonGroup,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Select as MuiSelect,
} from "@mui/material";
import Input from "components/input";
import { ModalBoxContext } from "components/modalBox/providers/modalBox";
import { useContext } from "react";
import { useForm } from "react-hook-form";
import { useMutation, useQuery } from "react-query";
import { addProduct, getCategories } from "api/actions/settings.actions";

interface onMenuEditProps {
  onMenuEdit: () => void;
}

const AddProductForm: React.FC<onMenuEditProps> = (props) => {
  const { onMenuEdit } = props;

  const { actionModalBox } = useContext(ModalBoxContext);
  const {
    handleSubmit,
    register,
    formState: { errors },
    setValue,
  } = useForm<any>({ defaultValues: { section: "test", userId: 1 } });

  const { data, isLoading } = useQuery(["categories"], getCategories);

  const { mutate } = useMutation(addProduct, {
    onSuccess: () => {
      onMenuEdit();
      actionModalBox(false, null, "");
    },
    onError: (err: any) => {
      alert(`Nie udało się - ${err.response.data}`);
    },
  });

  const onSubmit = async (data: any) => {
    mutate(data);
  };

  return (
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={4}>
          <Grid item xs={12}>
            <Input id="name" label="Nazwa" placeholder="Nazwa" register={register} required />
          </Grid>
          <Grid item xs={12}>
            <Input type="number" id="price" label="Cena" placeholder="Cena" register={register} required />
          </Grid>
          <Grid item xs={12}>
            <FormControl fullWidth variant="outlined" sx={{ width: "20%" }}>
              <InputLabel htmlFor="categoryId">Kategoria</InputLabel>
              <MuiSelect
                  label="Kategoria"
                  id="categoryId"
                  defaultValue={""}
                  {...register("categoryId", { required: true })}
              >
                {data?.map((category: any) => (
                    <MenuItem key={category.id} value={category.id}>
                      {category.name}
                    </MenuItem>
                ))}
              </MuiSelect>
            </FormControl>
          </Grid>
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

export default AddProductForm;
