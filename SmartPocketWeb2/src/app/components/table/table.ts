import { Component, Input, Output, EventEmitter, computed } from '@angular/core';
import { TableConfiguration } from '../../models/component/table-configuration';
import { CommonModule } from '@angular/common';
import { Paginator } from './paginator/paginator';

@Component({
  selector: 'app-table',
  imports: [CommonModule, Paginator],
  templateUrl: './table.html',
  styles: ``
})
export class Table {
  @Input() tableConfiguration: TableConfiguration | null = null;
  @Output() pageChange = new EventEmitter<{ pageNumber: number, pageSize: number }>();

  onPageChange({pageNumber, pageSize}: { pageNumber: number, pageSize: number }) {
    this.pageChange.emit({ pageNumber, pageSize});
  }
}
