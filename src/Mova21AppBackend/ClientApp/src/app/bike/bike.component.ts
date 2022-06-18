import { Component, OnInit } from "@angular/core";
import { BikeService } from "../shared/services/bike.service";
import { BikeAvailabilities } from "../shared/models/bikeavailabilities";

@Component({
  selector: "app-bike",
  templateUrl: "./bike.component.html",
  styleUrls: ["./bike.component.css"]
})
export class BikeComponent implements OnInit {
  availabilities: BikeAvailabilities= <BikeAvailabilities>{ availabilities: [] };

  constructor(private bikeService: BikeService) { }

  ngOnInit(): void {
    this.bikeService.getAvailabilities().subscribe(availabilities => {
      this.availabilities = availabilities;
    });
  }

  changeAvailability(id: number, change: number) {
    this.bikeService.changeCount({ id: id, amountChange: change}).subscribe(newAvailabilities => this.availabilities = newAvailabilities);
  }
}
