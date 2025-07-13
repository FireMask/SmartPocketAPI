<script setup>
import { formatMoney, formatShowDate } from '@/helpers';
import { BsCashCoin, BsCreditCard } from 'vue-icons-plus/bs';

defineProps({
  data: {
    type: Object
  },
  type: {
    type: String
  },
  category: {
    type: String
  },
  payment: {
    type: String
  },
  cash: {
    type: String
  },
  frequency: {
    type: String
  },
})
</script>

<template>
  <div class="h-fit bg-ticket rounded-lg shadow-lg p-4">

    <div class="text-center mb-4">
      <h2 class="text-xl font-semibold">Movement</h2>
      <p class="text-gray-600">{{ formatShowDate(data.movementDate) }}</p>
    </div>

    <div class="mb-4">
      <h3 class="font-medium text-sm text-center">{{ data.description }}</h3>
      <p v-if="type" class="text-sm">Type: {{ type }}</p>
      <p v-if="category" class="text-sm">Category: {{ category }}</p>
    </div>


    <div class="mb-4">
      <h3 v-if="payment" class="text-lg font-medium flex">
        <BsCreditCard /> <span class="ml-3">{{ payment }} </span>
      </h3>
      <h3 v-if="cash" class="text-lg font-medium flex">
        <BsCashCoin /> <span class="ml-3">{{ cash }} </span>
      </h3>
      <ul v-if="data.isInstallment">
        <li class="flex justify-between text-center">
          <span class="w-1/3">Installment</span>
          <span class="w-1/3">{{ data.installmentCount }} times</span>
          <span class="w-1/3">{{ frequency }} </span>
        </li>
      </ul>
    </div>

    <!-- Total -->
    <div class="flex justify-between font-semibold">
      <span>Total:</span>
      <span>{{ formatMoney(data.amount) }}</span>
    </div>
  </div>
</template>

<style scoped>
/* Estilo de fondo para simular un ticket de papel */
.bg-ticket {
  background-color: #fefefe;
  background-image: url('https://www.transparenttextures.com/patterns/paper-1.png');
  /* Fondo de papel */
  background-size: cover;
  border-radius: 8px;
  border: 2px solid #ddd;
  padding: 20px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

/* Borde irregular para dar efecto de corte a mano */
.bg-ticket:before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-image: url('https://www.transparenttextures.com/patterns/cut-paper.png');
  /* Fondo de borde rasgado */
  background-size: cover;
  background-repeat: repeat-x;
  border-radius: 8px;
  pointer-events: none;
}

/* Estilos adicionales */
h2,
h3 {
  font-family: 'Courier New', Courier, monospace;
}

ul {
  list-style-type: none;
}

ul li {
  margin: 4px 0;
  padding: 4px 0;
  border-bottom: 1px dashed #ddd;
}

.bg-blue-500 {
  font-family: 'Courier New', Courier, monospace;
}

.anticon {
  vertical-align: 0;
}
</style>