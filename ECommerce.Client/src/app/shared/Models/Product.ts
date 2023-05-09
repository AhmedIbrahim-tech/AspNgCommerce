export interface Product {
    id:number;
    name:string;
    description:string;
    price:string;
    pictureURL:string;
    productType:string;
    productBrand:string
}

export class Product implements Product {}