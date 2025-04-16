import { Delete, Edit } from "@mui/icons-material";
import { IconButton } from "@mui/material";

interface Props {
  onEdit: () => void;
  onDelete: () => void;
}

export const OptionsTable = ({ onEdit, onDelete }: Props) => {
  return (
    <>
      <IconButton onClick={onEdit} color="secondary">
        <Edit />
      </IconButton>
      <IconButton onClick={onDelete} color="error">
        <Delete />
      </IconButton>
    </>
  );
};
