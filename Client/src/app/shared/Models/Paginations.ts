export interface IPagination<T> {
    pageSize: number
    pageNumber: number
    pageCount: number
    data: T
  }


  export interface IBaseResponse<T> {
    message?: string;
    status?: boolean;
    statusCode?: number;
    validationErrors?: any[];
    data?: T;
  }
  