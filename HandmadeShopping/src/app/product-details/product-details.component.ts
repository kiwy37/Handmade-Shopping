import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../data.service'; // Update this path to the actual path of your service
import { Subscription } from 'rxjs';

interface Item {
  name: string;
  price: number;
  count: number;
  imagePath: string;
}

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product: any; // Declare the product variable
  selectedQuantity = 1;
  cartItems: any[] = [];
  products: any[] = [];
  cartSubscription: Subscription = new Subscription();  
  nume: string ='';

  constructor(private route: ActivatedRoute, private dataService: DataService) {
    this.nume = '';
  }

  ngOnInit() {
    this.dataService.currentUser.subscribe(nume => this.nume = nume);
    const image = this.route.snapshot.paramMap.get('image');
    this.dataService.getProducts().subscribe(products => {
      this.product = products.find(product => product.imagePath === image);
    });
    this.cartSubscription = this.dataService.getCart(this.nume).subscribe(cart => {
      this.cartItems = cart;
      });
    this.dataService.getProducts().subscribe(products => {
        this.products = products;
      });
  }

  addToCart(product: any, quantity: number) {
    this.dataService.addProductWithQuantity(product.imagePath, quantity).subscribe(() => {
      console.log('Product added to cart');
    }, error => {
      console.error('Error adding product to cart', error);
    });
  }

}