import { ChangeDetectorRef, Component, inject, TemplateRef, ViewChild} from '@angular/core';
import { MovementStore } from '../../stores/MovementStore';
import { FormControl, FormGroup } from '@angular/forms';
import { MovementsRequest } from '../../models/movements/movements-request';
import { TableConfiguration } from '../../models/component/table-configuration';
import { MovementTypeIcon } from "../shared/movement-type-icon/movement-type-icon";
import { Table } from '../table/table';
import { PagedResult } from '../../models/apiResults/paged-result';
import { Movement } from '../../models/movements/movement';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-movements',
  imports: [MovementTypeIcon, Table, CommonModule],
  templateUrl: './movements.html',
  styles: ``
})
export class Movements {
  movementStore = inject(MovementStore);
  cd = inject(ChangeDetectorRef);

  @ViewChild('typeCell', { read: TemplateRef }) typeCell:TemplateRef<any> | undefined;
  @ViewChild('dateCell', { read: TemplateRef }) dateCell:TemplateRef<any> | undefined;
  @ViewChild('categoryCell', { read: TemplateRef }) categoryCell:TemplateRef<any> | undefined;
  @ViewChild('paymentCell', { read: TemplateRef }) paymentCell:TemplateRef<any> | undefined;
  @ViewChild('amountCell', { read: TemplateRef }) amountCell:TemplateRef<any> | undefined;
  @ViewChild('actionsCell', { read: TemplateRef }) actionsCell:TemplateRef<any> | undefined;

  filters = new FormGroup({
    search: new FormControl(''),
    categoryId: new FormControl([]),
    paymentMethodId: new FormControl([]),
    movementTypeId: new FormControl([]),
    startDate: new FormControl(''),
    endDate: new FormControl(''),
    pageNumber: new FormControl(1),
    pageSize: new FormControl(10),
  });

  ngAfterViewInit(): void {
    this.loadMovements();
  }

  loadMovements() {
    const movementsRequest: MovementsRequest = {
      search: this.filters.get('search')?.value ?? '',
      categoryId: this.filters.get('categoryId')?.value ?? [],
      paymentMethodId: this.filters.get('paymentMethodId')?.value ?? [],
      movementTypeId: this.filters.get('movementTypeId')?.value ?? [],
      startDate: this.filters.get('startDate')?.value ?? '',
      endDate: this.filters.get('endDate')?.value ?? '',
      pageNumber: this.filters.get('pageNumber')?.value ?? 1,
      pageSize: this.filters.get('pageSize')?.value ?? 10,
    };
    this.movementStore.getMovementsData(movementsRequest);
  }

  getTableConfiguration(movementsLocal: PagedResult<Movement>): TableConfiguration | null {
    const tableConfiguration = {
      columns: [
        { id: 1, header: 'Type', field: 'movementType', width: '3%', cellTemplate: this.typeCell },
        { id: 2, header: 'Date', field: 'movementDate', width: '10%', cellTemplate: this.dateCell  },
        { id: 3, header: 'Payment Method', field: 'paymentMethod', width: '15%', cellTemplate: this.paymentCell },
        { id: 4, header: 'Description', field: 'description', width: '30%' },
        { id: 5, header: 'Amount', field: 'amount', width: '10%',cellTemplate: this.amountCell },
        { id: 6, header: 'Category', field: 'category', width: '15%', cellTemplate: this.categoryCell },
        { id: 7, header: '', field: 'actions', width: '6%', cellTemplate: this.actionsCell }
      ],
      rows: movementsLocal?.items ?? [],
      totalRecords: movementsLocal?.totalCount ?? 0,
      totalPages: movementsLocal?.totalPages ?? 0,
      pageSize: this.filters.get('pageSize')?.value ?? 10,
      pageNumber: this.filters.get('pageNumber')?.value ?? 1,
      loading: this.movementStore.select.isLoading(),
      showPaginator: true,
      rowsPerPageOptions: [10, 20, 50, 100]
    };
    return tableConfiguration;
  }

  onPageChange(event: { pageNumber: number, pageSize: number }) {
    this.filters.get('pageNumber')?.setValue(event.pageNumber);
    this.filters.get('pageSize')?.setValue(event.pageSize);
    this.loadMovements();
  }
}