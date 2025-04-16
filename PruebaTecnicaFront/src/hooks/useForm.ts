import { useCallback, useEffect, useRef, useState } from "react";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export const useForm = <T extends { [key: string]: any }>(
  initFormState: T,
  validations?: { [key in keyof T]?: [(value: T[key]) => boolean, string] }
) => {
  const [formState, setFormState] = useState<T>(initFormState);
  const [errors, setErrors] = useState<{ [key in keyof T]?: string }>({});
  const validationsRef = useRef(validations);

  // Mantener las validaciones en el ref
  useEffect(() => {
    validationsRef.current = validations;
  }, [validations]);

  // Validar el formulario
  const validateForm = useCallback(() => {
    const newErrors: { [key in keyof T]?: string } = {};
    if (validationsRef.current) {
      for (const field in validationsRef.current) {
        const [validationFn, errorMessage] = validationsRef.current[field]!;
        const isValid = validationFn(formState[field]);
        newErrors[field] = isValid ? undefined : errorMessage;
      }
    }
    setErrors(newErrors); // Actualiza errores sin depender de prevErrors
  }, [formState]);

  // Validar el formulario cuando el estado cambie
  useEffect(() => {
    if (validations) {
      validateForm();
    }
  }, [formState, validateForm]);

  // Manejar cambios en los campos
  const onChange = <K extends keyof T>(field: K, value: T[K]) => {
    setFormState((prevState) => ({
      ...prevState,
      [field]: value,
    }));
  };

  // Comprobar si el formulario es válido
  const isFormValid = useCallback(() => {
    return Object.values(errors).every((error) => error === undefined);
  }, [errors]);

  // Resetear el formulario
  const resetForm = () => {
    setFormState(initFormState);
    setErrors({});
  };

  return {
    formState,
    errors,
    onChange,
    isFormValid,
    resetForm,
    setFormState, // Exponer esto para actualizaciones manuales si es necesario
  };
};
