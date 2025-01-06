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

  let items = [
    {
      id: 1,
      nama: "Paracetamol 500mg",
      no_batch: "LKASD12",
      stok: 23,
      tgl_msk: 2017,
      tgl_klr: 2018,
      harga1: 2000,
      harga2: 2000,
      diskon: 80,
    },
    {
      id: 2,
      nama: "Ibuprofen 200mg",
      no_batch: "AKMK232",
      stok: 2,
      tgl_msk: 2017,
      tgl_klr: 2018,
      harga1: 2000,
      harga2: 2000,
      diskon: 80,
    },
    {
      id: 3,
      nama: "Dexamethasone",
      no_batch: "ASJ1231",
      stok: 4,
      tgl_msk: 2017,
      tgl_klr: 2018,
      harga1: 2000,
      harga2: 2000,
      diskon: 80,
    },
    {
      id: 4,
      nama: "Metformin",
      no_batch: "AKSDM12",
      stok: 23,
      tgl_msk: 2017,
      tgl_klr: 2018,
      harga1: 2000,
      harga2: 2000,
      diskon: 80,
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
    <TabItem open title="Stok Obat">
      <div class="flex space-x-4 justify-between">
        <h1 class="text-2xl font-semibold">Data Stok Obat</h1>
        <Button>
          <PlusOutline class="w-5 h-5 me-2" />Tambah Data
        </Button>
      </div>

      <Table
        innerDivClass="left-0 my-2"
        {items}
        placeholder="Cari data obat"
        hoverable={true}
        filter={(item, searchTerm) =>
          item.no_batch.toLowerCase().includes(searchTerm.toLowerCase())}
      >
        <TableHead>
          <TableHeadCell>No</TableHeadCell>
          <TableHeadCell>Nama Obat</TableHeadCell>
          <TableHeadCell>No Batch</TableHeadCell>
          <TableHeadCell>Jumlah Stok</TableHeadCell>
          <TableHeadCell>Tanggal Masuk</TableHeadCell>
          <TableHeadCell>Tanggal Keluar</TableHeadCell>
          <TableHeadCell>Harga 1</TableHeadCell>
          <TableHeadCell>Harga 2</TableHeadCell>
          <TableHeadCell>Diskon</TableHeadCell>
        </TableHead>
        <TableBody tableBodyClass="divide-y">
          <TableBodyRow slot="row" let:item>
            <TableBodyCell>{item.id}</TableBodyCell>
            <TableBodyCell>{item.nama}</TableBodyCell>
            <TableBodyCell>{item.no_batch}</TableBodyCell>
            <TableBodyCell>{item.stok}</TableBodyCell>
            <TableBodyCell>{item.tgl_msk}</TableBodyCell>
            <TableBodyCell>{item.tgl_klr}</TableBodyCell>
            <TableBodyCell>{item.harga1}</TableBodyCell>
            <TableBodyCell>{item.harga2}</TableBodyCell>
            <TableBodyCell>{item.diskon}</TableBodyCell>
          </TableBodyRow>
        </TableBody>
      </Table>
    </TabItem>
    <TabItem title="Faktur">
      <p class="text-sm text-gray-500 dark:text-gray-400">
        <b>Faktur:</b>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
        incididunt ut labore et dolore magna aliqua.
      </p>
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
