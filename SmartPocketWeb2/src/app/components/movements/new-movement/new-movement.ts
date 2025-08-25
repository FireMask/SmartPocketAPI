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

  newMovementForm = new FormGroup({
      movementDate: new FormControl(new Date(), [Validators.required]),
      description: new FormControl(''),
      amount: new FormControl(0, [Validators.required, Validators.min(0.01)]),
      categoryId: new FormControl(0, [Validators.required, Validators.min(1)]),
      paymentMethodId: new FormControl(0, [Validators.required, Validators.min(1)]),
      recurringPaymentId: new FormControl(null),
      movementTypeId: new FormControl(0, [Validators.required, Validators.min(1)]),
      installmentNumber: new FormControl(null),
      creditCardPaymentId: new FormControl(null),
      isInstallment: new FormControl(false),
      frequencyId: new FormControl(null),
      installmentCount: new FormControl(null),
  });

  save() {
    const movementModel: Partial<MovementViewModel> = {
        movementDate: this.newMovementForm.value.movementDate || new Date(),
        description: this.newMovementForm.value.description || '',
        amount: this.newMovementForm.value.amount || 0,
        categoryId: this.newMovementForm.value.categoryId || 0,
        paymentMethodId: this.newMovementForm.value.paymentMethodId || 0,
        recurringPaymentId: this.newMovementForm.value.recurringPaymentId || null,
        movementTypeId: this.newMovementForm.value.movementTypeId || 0,
        installmentNumber: this.newMovementForm.value.installmentNumber || null,
        creditCardPaymentId: this.newMovementForm.value.creditCardPaymentId || null,
    };
    this.movementStore.createNewMovement(movementModel);
    this.homeStore.closeNewMovementModal();
  }
}
