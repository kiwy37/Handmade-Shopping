import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators'; 

interface Item {
  name: string;
  price: number;
  count: number;
  description: string;
  imagePath: string;
}

interface ProductData {
  produs: {
    productName: string;
    price: number;
    quantity: number;
    description: string;
    imagePath: string;
    id: number;
  };
  quantity: number;
}


@Injectable({
  providedIn: 'root'
})
export class DataService {
  private baseUrl = 'https://localhost:7217'; 
  private user = new BehaviorSubject<string>('');
  currentUser = this.user.asObservable();
  nameCurrentUser: string = '';

  private loggedIn = new BehaviorSubject<boolean>(false);
  isLoggedIn: Observable<boolean> = this.loggedIn.asObservable();

  constructor(private router: Router, private http: HttpClient) { // Inject HttpClient
  }

  addProductToUserByImagePath(imagePath: string): Observable<any> {
    const decreaseUrl = `${this.baseUrl}/AddProductToUserByImagePath?username=${this.nameCurrentUser}&imagePath=${encodeURIComponent(imagePath)}`;
    return this.http.post(decreaseUrl, {});
}

  getProducts(): Observable<Item[]> {
    const productsUrl = `${this.baseUrl}/Products/getProducts`; // replace with your actual API endpoint
    return this.http.get<Item[]>(productsUrl);
  }

getCart(currentUser: any): Observable<Item[]> {
  const cartUrl = `${this.baseUrl}/UsersProducts?Name=${this.nameCurrentUser}`;
  return this.http.get<ProductData[]>(cartUrl).pipe(
    map(products => products.map((p: ProductData) => ({
      name: p.produs.productName,
      price: p.produs.price,
      count: p.quantity,
      description: p.produs.description,
      imagePath: p.produs.imagePath
    })))
  );
}

  shouldDisplay(): boolean {
    const route = this.router.url;
    return this.loggedIn.getValue() && !['/login', '/home'].includes(route);
  }

  setUser(name: string) {
    this.user.next(name);
    this.nameCurrentUser = name;
    this.loggedIn.next(true);
  }

  setLoggedIn(value: boolean) { // Add this method
    this.loggedIn.next(value);
  }

  clearCart() {
    var cartProd: Item[] = [];
    this.getCart(this.nameCurrentUser).subscribe(cart => {
      cartProd = cart;
      });

      cartProd.forEach(item => {
      while (item.count > 0) {
        item.count--;
        this.decreaseCount(item).subscribe(); // Assuming decreaseCount makes a server request to decrease the count
      }
    });
  }

  increaseCount(item: Item): Observable<any> {
    const increaseUrl = `${this.baseUrl}/IncreaseQuantity?name=${this.nameCurrentUser}&imagePath=${encodeURIComponent(item.imagePath)}`;
    return this.http.post(increaseUrl, {});
  }
  
  decreaseCount(item: Item): Observable<any> {
    const decreaseUrl = `${this.baseUrl}/DecreaseQuantity?name=${this.nameCurrentUser}&imagePath=${encodeURIComponent(item.imagePath)}`;
    return this.http.post(decreaseUrl, {});
  }

  addProductWithQuantity(imagePath: string, quantity: number): Observable<any> {
    const addProductUrl = `${this.baseUrl}/AddProductWithQuantity?name=${encodeURIComponent(this.nameCurrentUser)}&imagePath=${encodeURIComponent(imagePath)}&quantity=${quantity}`;
    return this.http.post(addProductUrl, {});
}

  removeItemFromCart(item: Item) {
    
    while (item.count > 0) {
      item.count--;
      this.decreaseCount(item).subscribe(); 
    }
  }

}