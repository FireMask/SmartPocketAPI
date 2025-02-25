<script setup>
  import { ref } from 'vue';
  import { formatShowDate, formatMoney } from '../../helpers';
  import { useMovementsStore } from '@/stores/movements';
  import { DeleteOutlined } from '@ant-design/icons-vue';
  import { h } from 'vue';

  const store = useMovementsStore()

  const loading = ref(false);
  const openAdd = ref(false);
  const pendingData = ref ({})
  
  const showModal = (movement) => {
    pendingData.value = movement
    console.log(pendingData.value)
    openAdd.value = true;
  };

  const handleOk = async() => {
    loading.value = true;
    await store.AddPendingMovement(pendingData.value);
    loading.value = false;
    openAdd.value = false;
  };
  const handleCancel = () => {
    openAdd.value = false;
  };

  const columns = [
    {
      title: 'Date',
      dataIndex: 'movementDate',
      key: 'date',
    },
    {
      title: 'Movement',
      dataIndex: 'age',
      key: 'age',
      width: 80,
    },
    {
      title: 'Address',
      dataIndex: 'address',
      key: 'address 1',
      ellipsis: true,
    },
    {
      title: 'Long Column Long Column Long Column',
      dataIndex: 'address',
      key: 'address 2',
      ellipsis: true,
    },
    {
      title: 'Long Column Long Column',
      dataIndex: 'address',
      key: 'address 3',
      ellipsis: true,
    },
    {
      title: 'Long Column',
      dataIndex: 'address',
      key: 'address 4',
      ellipsis: true,
    },
  ];
</script>

<template>
  <div class="overflow-auto h-full">
    <table class="w-full h-max align-middle border-b border-gray-200 shadow sm:rounded-lg">
      <thead>
        <tr>
          <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
            Date
          </th>
          <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
            Movement
          </th>
          <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
            Payment Method
          </th>
          <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
            type
          </th>
          <th class="px-6 py-3 text-xs font-medium leading-4 tracking-wider text-left text-gray-500 uppercase border-b border-gray-200 bg-gray-50">
            Amount
          </th>
          <th class="px-6 py-3 border-b border-gray-200 bg-gray-50"></th>
        </tr>
      </thead>

      <tbody class="bg-white">
        <tr v-for="(m, recurringPaymentId) in store.userPendingMovements" :key="recurringPaymentId">
          <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
            <div class="text-sm leading-5 text-gray-800"> {{ formatShowDate(m.movementDate) }} </div>
          </td>

          <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
            <div class="text-sm font-medium leading-5 text-gray-900">
              {{ m.categoryName }}
            </div>
            <div class="text-sm leading-5 text-gray-500">
              {{ m.description }}
            </div>
          </td>

          <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
            <div class="text-sm leading-5 text-gray-900">
              {{ m.paymentMethodName }}
            </div>
            <div class="text-sm leading-5 text-gray-500">
              <div v-if="m.installmentCount > 0">
                Installment {{ m.installmentNumber }} / {{ m.installmentCount }}
              </div>
              <div v-else>
                Payment # {{ m.installmentNumber }}
              </div>
            </div>
          </td>

          <td class="px-6 py-4 border-b border-gray-200 whitespace-nowrap">
            <div class="text-sm font-medium leading-5 text-gray-900">
              {{ m.movementTypeName }}
            </div>
            <div class="text-sm leading-5 text-gray-500">
              {{ m.frequencyName }}
            </div>
          </td>

          <td class="px-6 py-4 text-sm leading-5 font-semibold border-b border-gray-200 whitespace-nowrap">
            {{ formatMoney(m.amount) }}
          </td>
          <td class="px-6 py-4 text-sm leading-5 font-semibold border-b border-gray-200 w-auto">
            <a-button shape="circle" :icon="h(DeleteOutlined)"></a-button>
            <button type="button" @click=""
              class="px-4 py-2 mx-2 text-center text-white bg-indigo-600 rounded-md focus:outline-none hover:bg-indigo-500">
              Delete
            </button>
            <button type="button" @click="showModal(m)"
              class="px-4 py-2 mx-2 text-sm text-center text-white bg-indigo-600 rounded-md focus:outline-none hover:bg-indigo-500">
              Add
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <a-table :dataSource="dataSource" :columns="columns" />
  </div>

  <a-modal v-model:open="openAdd" title="Add pending movement" @ok="handleOk">
    <template #footer>
      <a-button key="back" @click="handleCancel">Return</a-button>
      <a-button key="submit" type="primary" :loading="loading" @click="handleOk">Add movement</a-button>
    </template>
    <p>{{ pendingData.description }}</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
  </a-modal>

</template>