import { Component, inject } from '@angular/core';
import { MovementStore } from '../../../stores/MovementStore';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MovementViewModel } from '../../../models/movements/MovementViewModel';
import { CommonModule } from '@angular/common';
import { HomeStore } from '../../../stores/HomeStore';
import { CatalogStore } from '../../../stores/CatalogStore';

@Component({
  selector: 'app-new-movement',
  imports: [
    CommonModule,
    FormsModule, 
    ReactiveFormsModule,
  ],
  templateUrl: './new-movement.html',
  styles: ``
})
export class NewMovement {
  movementStore = inject(MovementStore)
  homeStore = inject(HomeStore)
  catalogStore = inject(CatalogStore)

  installmentQuickSelecction: number[] = [3, 6, 9, 12, 18];
  monthlyFrequencyId = 3;
  cashId = 1;

  newMovementForm = new FormGroup({
      movementDate: new FormControl(new Date(), [Validators.required]),
      description: new FormControl(''),
      amount: new FormControl(0, [Validators.required, Validators.min(0.01)]),
      categoryId: new FormControl(0, [Validators.required, Validators.min(1)]),
      paymentMethodId: new FormControl(0, [Validators.required, Validators.min(1)]),
      recurringPaymentId: new FormControl(null),
      movementTypeId: new FormControl(1),
      isInstallment: new FormControl(false),
      frequencyId: new FormControl(this.monthlyFrequencyId),
      installmentCount: new FormControl(+3),
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

  save() {
    const movementModel: Partial<MovementViewModel> = {
        movementDate: this.newMovementForm.value.movementDate || new Date(),
        description: this.newMovementForm.value.description || '',
        amount: this.newMovementForm.value.amount || 0,
        categoryId: this.newMovementForm.value.categoryId || 0,
        paymentMethodId: this.newMovementForm.value.paymentMethodId || 0,
        recurringPaymentId: this.newMovementForm.value.recurringPaymentId || null,
        movementTypeId: this.newMovementForm.value.movementTypeId || 0,
        installmentNumber: this.newMovementForm.value.installmentCount || null,
    };
    this.movementStore.createNewMovement(movementModel);
    this.homeStore.closeNewMovementModal();
  }
}
