import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../data.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  signupForm = this.fb.group({
    name: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  constructor(
    private authService: DataService,
    private router: Router,
    private http: HttpClient,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    // Aici poți adăuga cod care trebuie să ruleze la inițializarea componentei
  }

  loginUser(name: string, password: string) {
      return this.http.post('https://localhost:7217/User/login', { name, password }, { responseType: 'text' });
    }
  
    onLogin(event: Event): void {
      event.preventDefault();
      if (this.loginForm.valid) {
        const name = this.loginForm.value.email; // folosește 'name' în loc de 'email'
        const password = this.loginForm.value.password;
        if (name && password) {
          this.loginUser(name, password).subscribe(response => {
            if (response === 'User found') {
              this.authService.setUser(name); // folosește 'name' în loc de 'email'
              this.router.navigate(['/home']);
            } else {
              console.log('Login failed'); // Add this line
              window.alert('Login failed. Please check your username and password.');
            }
          });
        }
      }
    }

    signupUser(name: string, password: string) {
      return this.http.post('https://localhost:7217/User/register', { name, password }, { responseType: 'text' });
    }
    
    onSignup(event: Event): void {
      event.preventDefault();
    
      if (this.signupForm.valid) {
        const name = this.signupForm.value.email; // use 'name' instead of 'email'
        const password = this.signupForm.value.password;
        if (name && password) {
          this.signupUser(name, password).subscribe(response => {
            if (response === 'User created') {
              this.authService.setUser(name); // use 'name' instead of 'email'
              this.router.navigate(['/home']);
            } 
          });
        }
      }
    }
}
