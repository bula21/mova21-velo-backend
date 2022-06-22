import { ActivityCategory } from "./activityCategory";

export class Activity {
  id = 0;
  isPermanent = false;
  titleDe = "";
  titleFr = "";
  titleIt = "";
  locationDe = "";
  locationFr = "";
  locationIt = "";
  descriptionDe = "";
  descriptionFr = "";
  descriptionIt = "";
  openingHoursDe = "";
  openingHoursFr = "";
  openingHoursIt = "";
  category = ActivityCategory.Rover;
  date = new Date();
}
