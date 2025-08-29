import { CommonModule } from '@angular/common';
import { Component, TemplateRef, ViewChild } from '@angular/core';
import { NgxTimelineComponent, NgxTimelineEvent, NgxTimelineEventGroup, NgxTimelineItemPosition, NgxTimelineOrientation } from '@frxjs/ngx-timeline';

@Component({
  selector: 'app-timeline',
  imports: [
    NgxTimelineComponent,
    CommonModule
  ],
  templateUrl: './timeline.html',
  styles: ``
})
export class Timeline {

  orientation: NgxTimelineOrientation = NgxTimelineOrientation.HORIZONTAL;
  groupEvent: NgxTimelineEventGroup = NgxTimelineEventGroup.MONTH_YEAR;

  descriptionText: string = 'Nómina';
  hoveredEventId: number | null = null;

  handleClick(event: NgxTimelineEvent) {
    // console.log('Event clicked:', event);
  }

  handleHover(id: number | null) {
    // console.log('Event hovered:', event);
    this.hoveredEventId = id;
  }

  topEvents: NgxTimelineEvent[] = [
    {
      timestamp: new Date('2025-08-09T00:00:00'),
      title: 'BBVA',
      description: 'Amount: $200.00',
      id: 1,
      itemPosition: NgxTimelineItemPosition.ON_LEFT
    },
    {
      timestamp: new Date('2025-08-29T00:00:00'),
      title: 'BBVA',
      description: 'DueDate',
      id: 1,
      itemPosition: NgxTimelineItemPosition.ON_LEFT
    },
    {
      timestamp: new Date('2025-08-15T00:00:00'),
      title: 'HSBC',
      description: 'Amount: $400.00',
      id: 2,
      itemPosition: NgxTimelineItemPosition.ON_LEFT
    },
    {
      timestamp: new Date('2025-09-03T00:00:00'),
      title: 'HSBC',
      description: 'DueDate',
      id: 2,
      itemPosition: NgxTimelineItemPosition.ON_LEFT
    },
  ];

  bottomEvents: NgxTimelineEvent[] = [
    {
      timestamp: new Date('2025-07-15T00:00:00'),
      title: this.descriptionText,
      description: '',
      id: -1,
      itemPosition: NgxTimelineItemPosition.ON_RIGHT
    },
    {
      timestamp: new Date('2025-07-29T00:00:00'),
      title: this.descriptionText,
      description: '',
      id: -1,
      itemPosition: NgxTimelineItemPosition.ON_RIGHT
    },
    {
      timestamp: new Date('2025-08-15T00:00:00'),
      title: this.descriptionText,
      description: '',
      id: -1,
      itemPosition: NgxTimelineItemPosition.ON_RIGHT
    },
    {
      timestamp: new Date('2025-08-29T00:00:00'),
      title: this.descriptionText,
      description: '',
      id: -1,
      itemPosition: NgxTimelineItemPosition.ON_RIGHT
    },
    {
      timestamp: new Date('2025-09-15T00:00:00'),
      title: this.descriptionText,
      description: '',
      id: -1,
      itemPosition: NgxTimelineItemPosition.ON_RIGHT
    },
    {
      timestamp: new Date('2025-09-30T00:00:00'),
      title: this.descriptionText,
      description: '',
      id: -1,
      itemPosition: NgxTimelineItemPosition.ON_RIGHT
    },
  ];

  events: NgxTimelineEvent[] = [...this.topEvents, ...this.bottomEvents];

  get eventsFiltered(): NgxTimelineEvent[] {
    // return this.events.filter(e => e.id === 1);
    return this.events;
  }
}
