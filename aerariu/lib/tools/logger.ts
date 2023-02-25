import { isDevelopment } from '../utils/common';

export const logToConsole = (flag: string, message: any) => {
  if (isDevelopment()) {
    if (typeof message === 'object') {
      console.log({ flag, message });
      return;
    }
    console.log(`${flag.toUpperCase()} | ${message}`);
  }
};
