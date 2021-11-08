const requiredMessage = 'This field is required!';

export const required = [{ required: true, message: requiredMessage }];

export const requiredInput = [
  { required: true, message: requiredMessage },
  { whitespace: true, message: requiredMessage },
];

export const arrayRequired = [
  () => ({
    validator(_, value) {
      if (value && value.length !== 0) {
        return Promise.resolve();
      }
      return Promise.reject(new Error('You have to choose at least one!'));
    },
  }),
];

export const rangeRequired = [
  () => ({
    validator(_, value) {
      if (value && value.length === 2 && value[0] && value[1]) {
        return Promise.resolve();
      }
      return Promise.reject(new Error('Must specify date range!'));
    },
  }),
];

function useFormRules() {
  return {
    required,
    requiredInput,
    arrayRequired,
  };
}

export default useFormRules;
