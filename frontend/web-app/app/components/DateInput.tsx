import React from "react";
import { useController, UseControllerProps } from "react-hook-form";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker, { ReactDatePickerCustomHeaderProps } from "react-datepicker";

type Props = {
  label: string;
  type?: string;
  showLabel?: boolean;
  showTimeSelect: boolean;
  dateFormat: string;
} & UseControllerProps &
  Partial<ReactDatePickerCustomHeaderProps>;

export default function DateInput(props: Props) {
  const { fieldState, field } = useController({ ...props, defaultValue: "" });
  return (
    <div className="block  ">
      <DatePicker
        {...props}
        {...field}
        onChange={(value) => field.onChange(value)}
        selected={field.value}
        placeholderText={props.label}
        className={`
            block rounded-lg w-[100%] p-2 border text-sm   
            ${
              fieldState.error
                ? "bg-red-50 border-red-500 text-red-900"
                : !fieldState.invalid && fieldState.isDirty
                ? "bg-green-50 border-green-500 : text-green-900"
                : ""
            }
            `}
      />
      {fieldState.error && (
        <div className="text-red-500 text-sm">{fieldState.error.message}</div>
      )}
    </div>
  );
}
