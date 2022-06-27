import { Component, OnInit } from "@angular/core";
import { BikeService } from "../shared/services/bike.service";
import { BikeAvailability } from "../shared/models/bikeavailability";

@Component({
  selector: "app-bike",
  templateUrl: "./bike.component.html",
  styleUrls: ["./bike.component.css"]
})
export class BikeComponent implements OnInit {
  availability: BikeAvailability = new BikeAvailability;

  constructor(private bikeService: BikeService) { }

  ngOnInit(): void {
    this.bikeService.getAvailability().subscribe(availability => {
      this.availability = availability;
    });
  }

  updateRegularBike(delta: number): void {
    this.availability.regularBikes += delta;
    this.update();
  }

  updateCargoBike(delta: number): void {
    this.availability.cargoBikes += delta;
    this.update();
  }

  updateBikeTrailes(delta: number): void {
    this.availability.bikeTrailers += delta;
    this.update();
  }

  update() {
    this.bikeService.update(this.availability).subscribe(newAvailability => this.availability = newAvailability);
  }
}
