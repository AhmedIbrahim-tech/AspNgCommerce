export interface Product {
    id:number;
    name:string;
    description:string;
    price:string;
    pictureUrl:string;
    productType:string;
    productBrand:string
}

export class Product implements Product {}