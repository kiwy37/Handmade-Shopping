import { Component, OnInit} from '@angular/core';
import { DataService } from '../data.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css'
})
export class MenuComponent implements OnInit{
  nume: string ='';

  constructor(private dataService: DataService) {
    this.nume = ''; // Assign an initial value to 'nume'
  }

  ngOnInit() {
    this.dataService.currentUser.subscribe(nume => this.nume = nume);
  }
}
