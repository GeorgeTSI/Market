import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../Environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = environment.apiUrl; 

  constructor(private http: HttpClient) {}

  login() {
    const credentials = {
      username: 'admin',
      password: 'admin'
    };

    this.http.post<any>(`${this.baseUrl}/api/auth/login`, credentials).subscribe((resp) => {
        localStorage.setItem('token', resp.token);
    });
  }
}
