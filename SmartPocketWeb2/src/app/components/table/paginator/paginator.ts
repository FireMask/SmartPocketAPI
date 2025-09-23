import { Component, EventEmitter, Input, Output} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-paginator',
  imports: [CommonModule],
  templateUrl: './paginator.html',
  styles: [
    `:host { display: block; }
    button:focus { outline: 2px solid rgba(59,130,246,0.25); outline-offset: 2px; }
    `
  ]
})
export class Paginator {
  @Input() pageNumber = 1;
  @Input() pageSize = 10;
  @Input() totalRecords = 0;
  @Input() totalPages = 1;
  @Input() pageSizeOptions: number[] = [10, 20, 50, 100];

  @Output() pageChange = new EventEmitter<{ pageNumber: number; pageSize: number }>();

  startRecord = () => {
    if (this.totalRecords === 0) 
      return 0;
    return (this.pageNumber - 1) * this.pageSize + 1;
  };

  endRecord = () => {
    return Math.min(this.pageNumber * this.pageSize, this.totalRecords);
  };

  private emitPageChange(page: number, pageSize?: number) {
    const newPage = Math.max(1, Math.min(page, this.totalPages));
    this.pageChange.emit({ pageNumber: newPage, pageSize: pageSize ?? this.pageSize });
  }

  goToFirst() { this.emitPageChange(1); }
  goToPrev() { this.emitPageChange(this.pageNumber - 1); }
  goToNext() { this.emitPageChange(this.pageNumber + 1); }
  goToLast() { this.emitPageChange(this.totalPages); }
  goTo(page: number) { this.emitPageChange(page); }

  onPageSizeChange(newSize: number | string) {
    const sizeNum = Number(newSize);
    this.pageChange.emit({ pageNumber: 1, pageSize: sizeNum });
  }

  pagesToShow(): (number | '...')[] {
    const current = this.pageNumber;    

    if (this.totalPages <= 5) {
      return Array.from({ length: this.totalPages }, (_, i) => i + 1);
    }

    const pages: (number | '...')[] = [];

    pages.push(1);

    const left = Math.max(2, current - 1);
    const right = Math.min(this.totalPages - 1, current + 1);

    if (left > 2) {
      pages.push('...');
    }

    for (let p = left; p <= right; p++) {
      pages.push(p);
    }

    if (right < this.totalPages - 1) {
      pages.push('...');
    }

    pages.push(this.totalPages);

    return pages.filter((v, idx, arr) => arr.indexOf(v) === idx);
  }

}
