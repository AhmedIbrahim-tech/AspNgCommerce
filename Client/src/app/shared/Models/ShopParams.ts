export class ShopParams 
{
    categoryid: number = 0;
    sorting:string='name';
    pageNumber:number=1;
    pageSize:number=8;
    totalCount?:number;
    search?:string;
    BrandId:number = 0;
    TypeId:number = 0;
    Name?:string;
    Description?:string;


}