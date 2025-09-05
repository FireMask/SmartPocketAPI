import { Component, inject, signal } from '@angular/core';
import { MovementStore } from '../../../stores/MovementStore';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MovementViewModel } from '../../../models/movements/MovementViewModel';
import { CommonModule } from '@angular/common';
import { HomeStore } from '../../../stores/HomeStore';
import { CatalogStore } from '../../../stores/CatalogStore';
import { GroupButton } from '../../shared/group-button/group-button';
import { GButton, GButtonSize } from '../../../models/props/GButton';
import { dateToString } from '../../../helpers/utils';

@Component({
  selector: 'app-new-movement',
  imports: [
    CommonModule,
    FormsModule, 
    ReactiveFormsModule,
    GroupButton
  ],
  templateUrl: './new-movement.html',
  styles: ``
})
export class NewMovement {
  movementStore = inject(MovementStore)
  homeStore = inject(HomeStore)
  catalogStore = inject(CatalogStore)

  installmentQuickSelecction = [3, 6, 9, 12, 18];
  today = new Date();
  monthlyFrequencyId = 3;
  cashId = 1;

  defaultDateQuickSelection = [
    { id: 1, label: 'Today', date: this.today },
    { id: 2, label: 'Yesterday', date: new Date(new Date().setDate(this.today.getDate() - 1)) }
  ];

  newMovementForm = new FormGroup({
      movementDate: new FormControl(this.today, [Validators.required]),
      description: new FormControl(''),
      amount: new FormControl(0, [Validators.required, Validators.min(0.01)]),
      categoryId: new FormControl(0, [Validators.required, Validators.min(1)]),
      paymentMethodId: new FormControl(0, [Validators.required, Validators.min(1)]),
      movementTypeId: new FormControl(1),
      isInstallment: new FormControl(false),
      frequencyId: new FormControl(this.monthlyFrequencyId),
      installmentCount: new FormControl(3),
  });

  get selectedCategoryName(): string {
      const categoryId = this.newMovementForm.get('categoryId')?.value;
      const categories = this.catalogStore.select.categories();
      const category = categories?.find((c: any) => c.id == categoryId);
      return category?.name ?? '';
  }

  get selectedPaymentMethodName(): string {
      const paymentMethodId = this.newMovementForm.get('paymentMethodId')?.value;
      const paymentMethods = this.catalogStore.select.paymentMethods();
      const paymentMethod = paymentMethods?.find((c: any) => c.id == paymentMethodId);
      return paymentMethod?.name ?? '';
  }

  get selectedFrequencyName(): string {
      const frequencyId = this.newMovementForm.get('frequencyId')?.value;
      const frequencies = this.catalogStore.select.frequencies();
      const frequency = frequencies?.find((c: any) => c.id == frequencyId);
      return frequency?.name ?? '';
  }

  get installmentAmount(): string {
      const count = this.newMovementForm.get('installmentCount')?.value;
      const amount = this.newMovementForm.get('amount')?.value;
      return amount && count ? (amount / count).toFixed(2) : '0.00';
  }

  get isCashPayment(): boolean {
      const paymentMethodIdId = this.newMovementForm.get('paymentMethodId')?.value;
      return paymentMethodIdId == this.cashId;
  }

  get isCardPayment(): boolean {
      const paymentMethodIdId = this.newMovementForm.get('paymentMethodId')?.value ?? 0;
      return paymentMethodIdId > this.cashId;
  }

  get getCategoryButtons(): GButton[] {
    return this.catalogStore.select.topCategories().map(category => ({
      id: category.id ?? 0,
      label: category.name,
      style: 'bg-default btn-noround',
      selected: this.newMovementForm.get('categoryId')?.value == category?.id,
      selectedStyle: 'btn-active',
      buttonSize: GButtonSize.Small,
      action: () => {
        this.newMovementForm.controls['categoryId'].setValue(category?.id??0)
      }
    }));
  }

  get getDefaultDateButtons(): GButton[] {
    return this.defaultDateQuickSelection.map(_date => ({
      id: _date.id,
      label: _date.label,
      style: 'bg-default btn-noround',
      selected: this.newMovementForm.get('movementDate')?.value === _date.date,
      selectedStyle: 'btn-active',
      buttonSize: GButtonSize.Small,
      action: () => {
        this.newMovementForm.controls['movementDate'].setValue(_date.date);
      }
    }));
  }

  get getInstallmentButtons(): GButton[] {
    return this.installmentQuickSelecction.map(num => ({
      id: num,
      label: num.toString() + ' months',
      style: 'bg-default btn-noround',
      selected: this.newMovementForm.controls['installmentCount'].value === num,
      selectedStyle: 'btn-active',
      buttonSize: GButtonSize.Small,
      action: () => {
        this.newMovementForm.controls['frequencyId'].setValue(3); 
        this.newMovementForm.controls['installmentCount'].setValue(num);
      }
    }));
  }

   get getFrequencyButtons(): GButton[] {
    return this.catalogStore.select.frequencies().map(frequency => ({
      id: frequency.id,
      label: frequency.name,
      style: 'bg-default btn-noround',
      selected: this.newMovementForm.get('frequencyId')?.value == frequency?.id,
      selectedStyle: 'btn-active',
      buttonSize: GButtonSize.Small,
      action: () => {
        this.newMovementForm.controls['frequencyId'].setValue(frequency?.id??null)
      }
    }));
  }

  save() {
    const movementModel: Partial<MovementViewModel> = {
      movementDate: dateToString(this.newMovementForm.value.movementDate ?? new Date()),
      description: this.newMovementForm.value.description || '',
      amount: this.newMovementForm.value.amount || 0,
      categoryId: this.newMovementForm.value.categoryId || 0,
      paymentMethodId: this.newMovementForm.value.paymentMethodId || 0,
      movementTypeId: this.newMovementForm.value.movementTypeId || 0,
      installmentCount: this.newMovementForm.value.installmentCount || null,
      frequencyId: this.newMovementForm.value.frequencyId || null,
      isInstallment: this.newMovementForm.value.isInstallment || false,
    };
    this.movementStore.createNewMovement(movementModel);
    this.homeStore.closeNewMovementModal();
  }
}
