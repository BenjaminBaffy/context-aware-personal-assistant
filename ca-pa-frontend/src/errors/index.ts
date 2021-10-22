import { CommonError } from './common.error';

export * from './common.error';

type ErrorTypeOption = CommonError | number | string

export type ErrorType = ErrorTypeOption | null | undefined;


export class ErrorUtility {
  /**
   * Checks if has any or specific error. 
   * @param error - error from action
   * @param specific - error that you want to match
   */
  static hasError(error: ErrorType, specific?: ErrorTypeOption[]): boolean {
    if (error) {
      if (specific) {
        return specific.includes(error as ErrorTypeOption);
      }

      return true;
    }
    return false;

  }
}