import { Component, OnInit } from "@angular/core";
import { WeatherService } from "../shared/services/weather.service";
import { WeatherEntry } from "../shared/models/weatherEntry";
import { WeatherType } from "../shared/models/weatherType";
import { MessageService } from "primeng/api";
import { Subject } from "rxjs";
import { debounceTime } from "rxjs/operators";

@Component({
  selector: "app-weather",
  templateUrl: "./weather.component.html",
  styleUrls: ["./weather.component.css"]
})
export class WeatherComponent implements OnInit {
  weatherEntries: WeatherEntry[] = [];
  startDateRange = new Date();
  endDateRange = this.addDays(2);
  weatherOptions: any[];
  updateTimeouts: Map<number, Subject<WeatherEntry>>;
  dayTimeStringMap: Map<number, string>;

  constructor(private weatherService: WeatherService/*, private messageService: MessageService*/) {
    this.weatherOptions = [
      { value: WeatherType.Cloud, description: "Bewölkt", src: "/assets/weather-icons/mova_app-icons_def_weather.svg" },
      { value: WeatherType.CloudRain, description: "Regnerisch", src: "/assets/weather-icons/mova_app-icons_def_regen.svg" },
      { value: WeatherType.CloudSun, description: "Leicht bewölkt", src: "/assets/weather-icons/mova_app-icons-wetter_SVG_wolke-sonne.svg" },
      { value: WeatherType.CloudSunRain, description: "Leicht bewölkt mit regen", src: "/assets/weather-icons/mova_app-icons-wetter_SVG_wolke-sonne-regen.svg" },
      { value: WeatherType.Fog, description: "Neblig", src: "/assets/weather-icons/mova_app-icons-wetter_SVG_nebel.svg" },
      { value: WeatherType.Snow, description: "Schnee", src: "/assets/weather-icons/mova_app-icons-wetter_SVG_schnee.svg" },
      { value: WeatherType.Sun, description: "Sonnig", src: "/assets/weather-icons/mova_app-icons_def_sonne.svg" },
      { value: WeatherType.Thunderstorm, description: "Gewitter", src: "/assets/weather-icons/mova_app-icons_def_blitz.svg" }
    ];
    this.dayTimeStringMap = new Map<number, string>([
      [0, "Morgen"],
      [1, "Mittag"],
      [2, "Abend"],
      [3, "Nacht"]
    ]);
    this.updateTimeouts = new Map<number, Subject<WeatherEntry>>();
  }

  ngOnInit(): void {
    this.refreshEntries();
  }

  addDays(days: number) {
    const today = new Date();
    const newDate = new Date();
    
    newDate.setDate(today.getDate() + days);
    return newDate;
  }

  refreshEntries() {
    this.weatherService.getEntriesByDateRange(this.startDateRange, this.endDateRange).subscribe(weatherEntries => {
      this.weatherEntries = weatherEntries.entries;
      this.weatherEntries.forEach(entry => {
        let updateTimeout = new Subject<WeatherEntry>();
        updateTimeout.pipe(debounceTime(1000)).subscribe(entry => this.updateWeatherEntry(entry));
        this.updateTimeouts.set(entry.id, updateTimeout);
      });
    });
  }

  weatherEntryChanged(entry: WeatherEntry) {
    this.updateTimeouts.get(entry.id)?.next(entry);
  }

  updateWeatherEntry(entry: WeatherEntry) {
    this.weatherService.updateEntry(entry).subscribe(_ => {
      //this.messageService.add({
      //  severity: "success",
      //  summary: "Update erfolgreich",
      //  detail: `${entry.date} - ${entry.dayTime}`
      //}); 
    });
  }

  addWeatherEntry(weather: WeatherEntry) {
    this.weatherService.addEntry(weather).subscribe(newWeather => weather.id = newWeather.id);
  }
}
