import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../Environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PriceService {
  private hubConnection!: signalR.HubConnection;
  private priceUpdatesSubject = new BehaviorSubject<any[]>([]);
  public priceUpdates$ = this.priceUpdatesSubject.asObservable();
  private baseUrl = environment.apiUrl + '/api';

  constructor(private http: HttpClient) {
    this.startSignalRConnection();
  }

  private startSignalRConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.apiUrl}/priceHub`, {
      skipNegotiation: true, 
      transport: signalR.HttpTransportType.WebSockets
    })
    .withAutomaticReconnect()
    .build();

    this.hubConnection.start().catch(err => console.error('Error connecting to SignalR:', err));

    // Listen for updates from SignalR
    this.hubConnection.on('ReceivePriceUpdate', (updatedPrice) => {
      this.updateLocalPrices(updatedPrice);
    });
  }

  private updateLocalPrices(updatedPrice: any) {
    const currentPrices = this.priceUpdatesSubject.value;
    const index = currentPrices.findIndex(p => p.companyId === updatedPrice.companyId && p.marketId === updatedPrice.marketId);

    if (index !== -1) {
      currentPrices[index] = updatedPrice;
    } else {
      currentPrices.push(updatedPrice);
    }

    this.priceUpdatesSubject.next([...currentPrices]);
  }

  getCompanies(): Observable<any[]> {
    const token = localStorage.getItem('token');
  
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  
    
    return this.http.get<any[]>(`${this.baseUrl}/CompanyMarket/companies`, { headers });
  }

  getMarkets(): Observable<any[]> {
    const token = localStorage.getItem('token');
  
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  
    return this.http.get<any[]>(`${this.baseUrl}/CompanyMarket/markets`, { headers });
  }

  updatePrice(companyId: number, marketId: number, NewPrice: number) {
    const PriceUpdateDto = {
      NewPrice: NewPrice
    };
  
    this.http.put(`${this.baseUrl}/CompanyMarketPrice/${companyId}/${marketId}`, PriceUpdateDto)
      .subscribe(
        response => {
          console.log('Price update successful');
        },
        error => {
          console.error('Error updating price:');
        }
      );
  }

  getPrice(companyId: number, marketId: number) {
    return this.http.get<number>(`${this.baseUrl}/CompanyMarketPrice/${companyId}/${marketId}`);
  }
}
