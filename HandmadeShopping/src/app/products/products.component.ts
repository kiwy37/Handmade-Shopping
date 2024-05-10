import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service'; // Update this path to the actual path of your service
import { Subscription } from 'rxjs';
import { Router } from '@angular/router'; // Import Router
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  cartItems: any[] = [];
  cartSubscription: Subscription = new Subscription();
  products: any[] = [];
  nume: string ='';

  constructor(private dataService: DataService, private router: Router) 
  { 
    this.nume = '';
  } 

  addItemToCart(name: string, price: number, description:string, image:string, count: number = 1) {
    
    this.cartSubscription = this.dataService.getCart(this.nume).subscribe(cart => {
      this.cartItems = cart;
    });
    
    const item = this.cartItems.find(item => item.imagePath === image);

    if (item) {
      this.dataService.increaseCount(item).subscribe(response => {
        item.count++;
      });
    } else {
      this.dataService.addProductToUserByImagePath(image).subscribe(response => {
        // this.dataService.addItemToCart(name, price, description, image, count);
      });
      this.cartSubscription = this.dataService.getCart(this.nume).subscribe(cart => {
        this.cartItems = cart;
      });
    }
  }

  viewProductDetails(image: string) {
    this.router.navigate(['/product-details', image]);
  }

  ngOnInit() {
    this.dataService.getProducts().subscribe(products => {
      this.products = products;
    });

    this.cartSubscription = this.dataService.getCart(this.nume).subscribe(cart => {
      this.cartItems = cart;
    });
  }

  ngOnDestroy() {
    if (this.cartSubscription) {
      this.cartSubscription.unsubscribe();
    }
  }

  loadCartItems() {
    // this.cartItems = this.dataService.listCart();
  }
  
  setCountForItem(name: string, count: number) {
    // this.dataService.setCountForItem(name, count);
    this.loadCartItems();
  }

}