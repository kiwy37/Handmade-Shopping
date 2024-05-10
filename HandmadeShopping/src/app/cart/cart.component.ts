import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service'; // adjust the path as needed
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: any[] = [];
  cartSubscription: Subscription = new Subscription();
  nume: string ='';

  constructor(private dataService: DataService, private router: Router, private cdr: ChangeDetectorRef) 
  { 
    this.nume = '';
  }

  ngOnInit() {
    this.dataService.currentUser.subscribe(nume => this.nume = nume);
    this.cartSubscription = this.dataService.getCart(this.nume).subscribe(cart => {
    this.cart = cart;
    });
  }

  navigateToPay() {
    this.router.navigate(['/pay']);
  }

  ngOnDestroy() {
    if (this.cartSubscription) {
      this.cartSubscription.unsubscribe();
    }
  }

  async removeItemFromCart(image: string) {
    var item = this.cart.find(item => item.imagePath === image);
    while(item && item.count > 0) {
      await this.decreaseCount(item);
    }
    this.totalCount();
    this.totalCart();
    this.updateCartUI();
  }
  
  totalCount() {
    let total = 0;
    for(let item of this.cart) {
      total += item.count;
    }
    return total;
  }
  
  totalCart() {
    let total = 0;
    for(let item of this.cart) {
      total += item.price * item.count;
    }
    return total;
  }

  async clearCart() {
    for (const item of this.cart) {
      while(item.count >= 0) {
        await this.decreaseCount(item);
      }
    }
  }


  async increaseCount(item: any) {
    await this.dataService.increaseCount(item).toPromise();
    item.count++;
    this.totalCount();
    this.totalCart();
    this.updateCartUI();
  }
  
  async decreaseCount(item: any) {
    await this.dataService.decreaseCount(item).toPromise();
    item.count--;
    if(item.count == 0) {
      const cart = await this.dataService.getCart(this.nume).toPromise();
      if (cart) {
        this.cart = cart;
      }
    }
    this.totalCount();
    this.totalCart();
    this.updateCartUI();
  }

updateCartUI() {
  this.cdr.detectChanges();
}

  navigateToProducts() {
    this.router.navigate(['/products']);
  }
}