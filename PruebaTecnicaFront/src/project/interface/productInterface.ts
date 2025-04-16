export interface Product {
  id: string | null;
  name: string;
  description: string;
  quantity: number;
  productPrices: ProductPrice[];
}

export interface ProductPrice {
  id: string | null;
  price: number;
  store: string;
}
