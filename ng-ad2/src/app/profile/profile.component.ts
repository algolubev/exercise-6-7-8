import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  forecast: WeatherForecast[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getProfile();
  }

  getProfile() {
    this.http.get<WeatherForecast[]>('https://localhost:5011/weatherforecast')
      .subscribe(data => {
        this.forecast = data;
      });
  }

}

export interface WeatherForecast {
  date: Date,
  temperatureC: number,
  temperatureF: number,
  summary: string
}
