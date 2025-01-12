<script>
  import {
    Button,
    Dropdown,
    DropdownItem,
    Table,
    TableBody,
    TableBodyCell,
    TableBodyRow,
    TableHead,
    TableHeadCell,
    Tabs,
    TabItem,
  } from "flowbite-svelte";
  import { PlusOutline, BellSolid, EyeSolid } from "flowbite-svelte-icons";

  let searchTermBarang = "";
  let searchTermStok = "";

  let items_barang = [
    {
      id_obat: 1,
      nama_obat: "Paracetamol 500mg",
      harga_beli: 2000,
      harga_jual: 2200,
      pbf: "PT BioHealth",
      satuan: "Tablet",
      diskon: 10,
    },
    {
      id_obat: 2,
      nama_obat: "Amoxicillin",
      harga_beli: 3500,
      harga_jual: 3850,
      pbf: "PT AMS",
      satuan: "Kapsul",
      diskon: 2,
    },
    {
      id_obat: 3,
      nama_obat: "Ibuprofen",
      harga_beli: 7000,
      harga_jual: 7700,
      pbf: "PT ABC",
      satuan: "Tablet",
      diskon: 5,
    },
  ];

  let items_stok = [
    {
      id_stok: 1,
      id_obat: 1,
      nama_obat: "Paracetamol 500mg",
      no_batch: "LKASD12",
      tanggal_expired: "12 Maret 2025",
      jumlah: 100,
    },
    {
      id_stok: 2,
      id_obat: 2,
      nama_obat: "Amoxicillin",
      no_batch: "AKDSA21",
      tanggal_expired: "13 Maret 2025",
      jumlah: 50,
    },
    {
      id_stok: 3,
      id_obat: 3,
      nama_obat: "Ibuprofen",
      no_batch: "KDASA11",
      tanggal_expired: "14 Maret 2025",
      jumlah: 75,
    },
  ];
</script>

<main class="m-4">
  <div class="flex justify-between">
    <h1 class="text-3xl font-bold">PharmAssist</h1>
    <div
      id="bell"
      class="inline-flex items-center text-sm font-medium text-center text-gray-500 hover:text-gray-900 focus:outline-none dark:hover:text-white dark:text-gray-400 hover:cursor-pointer"
    >
      <BellSolid class="w-8 h-8" />
      <div class="flex relative">
        <div
          class="inline-flex relative -top-2 end-4 w-3 h-3 bg-red-500 rounded-full border-2 border-white dark:border-gray-900"
        ></div>
      </div>
    </div>
    <Dropdown
      placement="left"
      triggeredBy="#bell"
      class="w-full max-w-sm rounded divide-y divide-gray-100 shadow dark:bg-gray-800 dark:divide-gray-700"
    >
      <div slot="header" class="text-center py-2 font-bold">
        Obat Mendekati Expired
      </div>
      <DropdownItem class="flex space-x-4 rtl:space-x-reverse">
        <div class="ps-3 w-full">
          <div class="text-gray-500 text-sm mb-1.5 dark:text-gray-400">
            No. Batch: <span class="font-semibold text-gray-900 dark:text-white"
              >LKASD12</span
            ><br />
            Item:
            <span class="font-semibold text-gray-900 dark:text-white"
              >Paracetamol 500mg</span
            >
          </div>
          <div class="text-xs text-primary-600 dark:text-primary-500">
            Exp: 31 Februari 2025
          </div>
        </div>
      </DropdownItem>
      <DropdownItem class="flex space-x-4 rtl:space-x-reverse">
        <div class="ps-3 w-full">
          <div class="text-gray-500 text-sm mb-1.5 dark:text-gray-400">
            No. Batch: <span class="font-semibold text-gray-900 dark:text-white"
              >LKASD12</span
            ><br />
            Item:
            <span class="font-semibold text-gray-900 dark:text-white"
              >Dexamethasone</span
            >
          </div>
          <div class="text-xs text-primary-600 dark:text-primary-500">
            Exp: 22 April 2025
          </div>
        </div>
      </DropdownItem>
      <DropdownItem class="flex space-x-4 rtl:space-x-reverse">
        <div class="ps-3 w-full">
          <div class="text-gray-500 text-sm mb-1.5 dark:text-gray-400">
            No. Batch: <span class="font-semibold text-gray-900 dark:text-white"
              >ASJ1231</span
            ><br />
            Item:
            <span class="font-semibold text-gray-900 dark:text-white"
              >Ibuprofen</span
            >
          </div>
          <div class="text-xs text-primary-600 dark:text-primary-500">
            Exp: 12 Maret 2025
          </div>
        </div>
      </DropdownItem>
      <a
        slot="footer"
        href="/"
        class="block py-2 -my-1 text-sm font-medium text-center text-gray-900 bg-gray-50 hover:bg-gray-100 dark:bg-gray-800 dark:hover:bg-gray-700 dark:text-white"
      >
        <div class="inline-flex items-center">
          <EyeSolid class="me-2 w-4 h-4 text-gray-500 dark:text-gray-400" />
          View all
        </div>
      </a>
    </Dropdown>
  </div>
  <Tabs tabStyle="underline">
    <TabItem open title="Data Obat">
      <div class="flex space-x-4 mb-4 justify-between">
        <input
          type="text"
          class="border border-gray-400 p-2 rounded w-full flex-1"
          placeholder="Cari data obat..."
          bind:value={searchTermBarang}
        />
        <Button>
          <PlusOutline class="w-5 h-5 me-2" />Tambah Data
        </Button>
      </div>
      <p class="text-sm text-gray-500 dark:text-gray-400">
        <Table innerDivClass="left-0 my-2" hoverable={true}>
          <TableHead>
            <TableHeadCell>No</TableHeadCell>
            <TableHeadCell>Nama Obat</TableHeadCell>
            <TableHeadCell>Harga Beli</TableHeadCell>
            <TableHeadCell>Harga Jual</TableHeadCell>
            <TableHeadCell>PBF</TableHeadCell>
            <TableHeadCell>Satuan</TableHeadCell>
            <TableHeadCell>Diskon</TableHeadCell>
          </TableHead>
          <TableBody tableBodyClass="divide-y">
            {#each items_barang.filter((item) => item.nama_obat
                .toLowerCase()
                .includes(searchTermBarang.toLowerCase())) as item}
              <TableBodyRow>
                <TableBodyCell>{item.id_obat}</TableBodyCell>
                <TableBodyCell>{item.nama_obat}</TableBodyCell>
                <TableBodyCell>{item.harga_beli}</TableBodyCell>
                <TableBodyCell>{item.harga_jual}</TableBodyCell>
                <TableBodyCell>{item.pbf}</TableBodyCell>
                <TableBodyCell>{item.satuan}</TableBodyCell>
                <TableBodyCell>{item.diskon}</TableBodyCell>
              </TableBodyRow>
            {/each}
          </TableBody>
        </Table>
      </p>
    </TabItem>
    <TabItem title="Stok Obat">
      <div class="flex space-x-4 mb-4 justify-between">
        <input
          type="text"
          class="border border-gray-400 p-2 rounded w-full flex-1"
          placeholder="Cari data obat..."
          bind:value={searchTermStok}
        />
        <Button>
          <PlusOutline class="w-5 h-5 me-2" />Tambah Data
        </Button>
      </div>

      <Table innerDivClass="left-0 my-2" hoverable={true}>
        <TableHead>
          <TableHeadCell>No</TableHeadCell>
          <TableHeadCell>Nama Obat</TableHeadCell>
          <TableHeadCell>No Batch</TableHeadCell>
          <TableHeadCell>Tanggal Expired</TableHeadCell>
          <TableHeadCell>Jumlah</TableHeadCell>
        </TableHead>
        <TableBody tableBodyClass="divide-y">
          {#each items_stok.filter((item) => item.nama_obat
              .toLowerCase()
              .includes(searchTermStok.toLowerCase())) as item}
            <TableBodyRow>
              <TableBodyCell>{item.id_stok}</TableBodyCell>
              <TableBodyCell>{item.nama_obat}</TableBodyCell>
              <TableBodyCell>{item.no_batch}</TableBodyCell>
              <TableBodyCell>{item.tanggal_expired}</TableBodyCell>
              <TableBodyCell>{item.jumlah}</TableBodyCell>
            </TableBodyRow>
          {/each}
        </TableBody>
      </Table>
    </TabItem>
    <TabItem title="Tanggal Expired">
      <p class="text-sm text-gray-500 dark:text-gray-400">
        <b>Tanggal Expired:</b>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
        incididunt ut labore et dolore magna aliqua.
      </p>
    </TabItem>
  </Tabs>
</main>

<style lang="postcss">
  :global(html) {
    background-color: theme(colors.gray.100);
  }
</style>
