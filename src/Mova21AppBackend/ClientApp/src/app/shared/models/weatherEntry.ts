import { WeatherType } from "./weatherType";
import { DayTime } from "./dayTime";

export class WeatherEntry {
  id = 0;
  date: Date = new Date();
  dayTime: DayTime = DayTime.Morning;
  temperature: number = 0;
  weather: WeatherType = WeatherType.Sun;
}
