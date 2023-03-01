export type Nullable<T> = T | null | undefined;

export type StateStatus = {
  loading: boolean;
  success: boolean;
  error: Nullable<string>;
};

export type StateWithStatus<T> = {
  data: Nullable<T>;
  status: StateStatus;
};
