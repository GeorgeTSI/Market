import { Component, OnInit } from '@angular/core';
import { PriceService } from '../app/Services/price.service';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'MarketSPA';

  public companyMarketPrices: any[] = [];
  public companies: any[] = [];
  public markets: any[] = [];
  public companyId?: number;
  public marketId?: number;
  public newPrice?: number;
  public price?: any;

  constructor(private priceService: PriceService, private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.login();
    setTimeout(() => {
      this.priceService.priceUpdates$.subscribe((prices) => {
        this.companyMarketPrices = prices;
      });
     
      this.priceService.getCompanies().subscribe((data) => {
        this.companies = data;
      });
  
      this.priceService.getMarkets().subscribe((data) => {
        this.markets = data;
      });
    }, 5000);
  }

  onPriceUpdate() {
    if (this.companyId && this.marketId && this.newPrice) {
      this.priceService.updatePrice(this.companyId, this.marketId, this.newPrice);
    }
  }

  getPrice() {
    if (this.companyId && this.marketId) {
      this.priceService.getPrice(this.companyId, this.marketId).subscribe((data) => {
        this.price = data.toFixed(2);
        console.log("Received Price :", data);
      }, error => {
        console.error("Error fetching price:", error);
      });
    }
  }
  
}
