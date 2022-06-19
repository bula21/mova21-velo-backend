import { Component, OnInit } from '@angular/core';

import { ActivityService } from "../shared/services/activity.service";

import { ActivityCategory } from "../shared/models/activityCategory";
import { CreationStatus } from "../shared/models/creationStatus";
import { Activity } from "../shared/models/activity";


@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.css']
})
export class ActivitiesComponent implements OnInit {
  creationStatus = CreationStatus.DataEnter;
  activityCategories: any[];

  activity = new Activity();
  
  constructor(private activityService: ActivityService) {
    this.activityCategories = [
      { value: ActivityCategory.Both, description: "Beide" },
      { value: ActivityCategory.WalkIn, description: "Walk-In" },
      { value: ActivityCategory.Rover, description: "Rover" }
    ];
  }

  ngOnInit(): void {
  }

  resetInput(): void {
    this.activity = new Activity();
    this.creationStatus = CreationStatus.DataEnter;
  }
  
  canCreateActivity(): boolean {
    return !this.isNullOrWhiteSpace(this.activity.descriptionDe) &&
      !this.isNullOrWhiteSpace(this.activity.descriptionFr) &&
      !this.isNullOrWhiteSpace(this.activity.descriptionIt) &&
      !this.isNullOrWhiteSpace(this.activity.titleDe) &&
      !this.isNullOrWhiteSpace(this.activity.titleFr) &&
      !this.isNullOrWhiteSpace(this.activity.titleIt) &&
      !this.isNullOrWhiteSpace(this.activity.locationDe) &&
      !this.isNullOrWhiteSpace(this.activity.locationFr) &&
      !this.isNullOrWhiteSpace(this.activity.locationIt) &&
      !this.isNullOrWhiteSpace(this.activity.openingHoursDe) &&
      !this.isNullOrWhiteSpace(this.activity.openingHoursFr) &&
      !this.isNullOrWhiteSpace(this.activity.openingHoursIt);
  }

  createActivity():void {
    this.activityService.createActivity(this.activity)
      .subscribe(_ => this.creationStatus = CreationStatus.Successful);
  }

  isNullOrWhiteSpace(text: string) {
    return text && text.trim().length > 0;
  }
}
