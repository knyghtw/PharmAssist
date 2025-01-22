<script>
  import { fly } from "svelte/transition";

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
    Modal,
    Label,
    Input,
  } from "flowbite-svelte";

  import {
    isPermissionGranted,
    requestPermission,
    sendNotification,
  } from "@tauri-apps/plugin-notification";

  import {
    PlusOutline,
    BellSolid,
    EyeSolid,
    ChevronDownOutline,
    PenSolid,
    TrashBinSolid,
  } from "flowbite-svelte-icons";

  let notificationGranted = false;
  let isAddingPBF = false;
  let clickCreateDataModal = false;
  let isDataObat = true;
  let isStokObat = false;
  let isTglExp = false;
  let isStockAlert = false;

  // async function setupNotification() {
  //   notificationGranted = await isPermissionGranted();

  //   if (!notificationGranted) {
  //     const permission = await requestPermission();
  //     notificationGranted = permission === "granted";
  //   }

  //   if (notificationGranted) {
  //     sendNotification({
  //       title: "Notifikasi",
  //       body: "Notifikasi berhasil diaktifkan",
  //     });
  //   }
  // }

  // setupNotification();

  let searchTermBarang = "";
  let searchTermStok = "";
  let searchStockAlert = "";

  let items_barang = [
    {
      id_obat: 1,
      nama_obat: "Paracetamol 500mg",
      satuan: "Tablet",
      jml_stok: 10,
    },
    {
      id_obat: 2,
      nama_obat: "Amoxicillin",
      satuan: "Kapsul",
      jml_stok: 2,
    },
    {
      id_obat: 3,
      nama_obat: "Ibuprofen",
      satuan: "Tablet",
      jml_stok: 5,
    },
  ];

  let items_stok = [
    {
      id_stok: 1,
      id_obat: 1,
      nama_obat: "Paracetamol 500mg",
      no_batch: "LKASD12",
      harga_beli: 2000,
      harga_jual: 2200,
      tanggal_expired: "12 Maret 2025",
      jumlah: 100,
    },
    {
      id_stok: 2,
      id_obat: 2,
      nama_obat: "Amoxicillin",
      no_batch: "AKDSA21",
      harga_beli: 3000,
      harga_jual: 3300,
      tanggal_expired: "13 Maret 2025",
      jumlah: 50,
    },
    {
      id_stok: 3,
      id_obat: 3,
      nama_obat: "Ibuprofen",
      no_batch: "KDASA11",
      harga_beli: 4000,
      harga_jual: 4400,
      tanggal_expired: "14 Maret 2025",
      jumlah: 75,
    },
  ];
</script>

<main class="m-4">
  <Modal bind:open={clickCreateDataModal} autoclose={false} outsideclose>
    {#if !isAddingPBF}
      <form
        in:fly={{ x: 50, duration: 300 }}
        out:fly={{ x: -50, duration: 300 }}
        class="flex flex-col space-y-4"
        action="#"
      >
        <h3 class="text-xl font-medium text-gray-900 dark:text-white">
          Tambah Data
        </h3>
        <hr />
        <Label class="space-y-2">
          <span class="text-gray-900">Nama Obat</span>
          <Input
            type="text"
            name="nama_obat"
            placeholder="Paracetamol"
            class="font-normal"
            required
          />
        </Label>
        <Label class="space-y-2">
          <span class="text-gray-900">Harga Beli</span>
          <Input
            type="number"
            name="harga_beli"
            placeholder="10000"
            class="font-normal"
            required
          />
        </Label>
        <Label class="space-y-2">
          <span class="text-gray-900">Harga Jual</span>
          <Input
            type="number"
            name="harga_jual"
            placeholder="11000"
            class="font-normal"
            required
          />
        </Label>
        <Label class="space-y-2 flex flex-col">
          <span class="text-gray-900">PBF</span>
          <Button color="alternative" class="hover:text-black text-black"
            >Pilih PBF disini<ChevronDownOutline
              class="w-6 h-6 ms-2 text-black dark:text-white"
            /></Button
          >
          <Dropdown class="w-48 overflow-y-auto py-1 h-48 ">
            <DropdownItem
              class="flex items-center text-base font-semibold gap-2"
            >
              Leslie Livingston
            </DropdownItem>
            <a
              slot="footer"
              href="/"
              class="flex justify-center px-3 py-2 -mb-1 text-sm font-medium text-primary-600 bg-gray-50 hover:bg-gray-100 dark:bg-gray-700 dark:hover:bg-gray-600 dark:text-primary-500 hover:underline"
              on:click={() => (isAddingPBF = true)}
            >
              Tambah PBF Baru
            </a>
          </Dropdown>
        </Label>
        <Label class="space-y-2">
          <span class="text-gray-900">Satuan</span>
          <Input
            type="text"
            name="satuan"
            placeholder="Tablet"
            class="font-normal"
            required
          />
        </Label>
        <Label class="space-y-2">
          <span class="text-gray-900">Diskon (%)</span>
          <Input
            type="number"
            name="diskon"
            placeholder="10"
            class="font-normal"
            required
          />
        </Label>
        <hr />
        <Button
          on:click={() => {
            alert('Handle "success"');
            clickCreateDataModal = false;
          }}>Tambah</Button
        >
      </form>
    {:else}
      <form
        in:fly={{ x: -50, duration: 300 }}
        out:fly={{ x: 50, duration: 300 }}
        class="flex flex-col space-y-4"
        on:submit|preventDefault={() => {
          isAddingPBF = false;
          clickCreateDataModal = false;
        }}
      >
        <h3 class="text-xl font-medium text-gray-900 dark:text-white">
          Tambah Data PBF
        </h3>
        <hr />
        <Label class="space-y-2">
          <span class="text-gray-900">Nama PBF</span>
          <Input
            type="text"
            name="pbf"
            placeholder="PT ABC"
            class="font-normal"
            required
          />
        </Label>
        <div class="flex flex-row justify-between space-x-4">
          <Button class="flex flex-1" type="submit">Simpan</Button>
          <Button
            class="flex flex-1"
            type="button"
            color="alternative"
            on:click={() => (isAddingPBF = false)}
          >
            Kembali
          </Button>
        </div>
      </form>
    {/if}
  </Modal>
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
    <TabItem
      open={isDataObat}
      title="Data Obat"
      on:click={() => {
        isDataObat = true;
        isStokObat = false;
        isTglExp = false;
        isStockAlert = false;
      }}
    >
      <div class="flex space-x-4 mb-4 justify-between">
        <input
          type="text"
          class="border border-gray-400 p-2 rounded w-full flex-1"
          placeholder="Cari data obat..."
          bind:value={searchTermBarang}
        />
        <Button on:click={() => (clickCreateDataModal = true)}>
          <PlusOutline class="w-5 h-5 me-2" />Tambah Data
        </Button>
      </div>
      <p class="text-sm text-gray-500 dark:text-gray-400">
        <Table innerDivClass="left-0 my-2" hoverable={true}>
          <TableHead>
            <TableHeadCell>No</TableHeadCell>
            <TableHeadCell>Nama Obat</TableHeadCell>
            <TableHeadCell>Satuan</TableHeadCell>
            <TableHeadCell>Jumlah Stok</TableHeadCell>
            <TableHeadCell>Aksi</TableHeadCell>
          </TableHead>
          <TableBody tableBodyClass="divide-y">
            {#each items_barang.filter((item) => item.nama_obat
                .toLowerCase()
                .includes(searchTermBarang.toLowerCase())) as item}
              <TableBodyRow>
                <TableBodyCell>{item.id_obat}</TableBodyCell>
                <TableBodyCell>{item.nama_obat}</TableBodyCell>
                <TableBodyCell>{item.satuan}</TableBodyCell>
                <TableBodyCell>{item.jml_stok}</TableBodyCell>
                <TableBodyCell>
                  <div class="flex space-x-4">
                    <Button
                      on:click={() => {
                        isStokObat = true;
                        isDataObat = false;
                        searchTermStok = item.nama_obat;
                      }}
                      color="blue"
                      pill={true}
                      class="!p-2"><EyeSolid class="w-6 h-6" /></Button
                    ><Button color="yellow" pill={true} class="!p-2"
                      ><PenSolid class="w-6 h-6" /></Button
                    ><Button color="red" pill={true} class="!p-2"
                      ><TrashBinSolid class="w-6 h-6" /></Button
                    >
                  </div>
                </TableBodyCell>
              </TableBodyRow>
            {/each}
          </TableBody>
        </Table>
      </p>
    </TabItem>
    <TabItem
      open={isStokObat}
      title="Stok Obat"
      on:click={() => {
        isDataObat = false;
        isStokObat = true;
        isTglExp = false;
        isStockAlert = false;
        searchTermStok = "";
      }}
    >
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
          <TableHeadCell>Harga Beli</TableHeadCell>
          <TableHeadCell>Harga Jual</TableHeadCell>
          <TableHeadCell>Tanggal Expired</TableHeadCell>
          <TableHeadCell>Jumlah Stok</TableHeadCell>
        </TableHead>
        <TableBody tableBodyClass="divide-y">
          {#each items_stok.filter((item) => item.nama_obat
              .toLowerCase()
              .includes(searchTermStok.toLowerCase())) as item}
            <TableBodyRow>
              <TableBodyCell>{item.id_stok}</TableBodyCell>
              <TableBodyCell>{item.nama_obat}</TableBodyCell>
              <TableBodyCell>{item.no_batch}</TableBodyCell>
              <TableBodyCell>{item.harga_beli}</TableBodyCell>
              <TableBodyCell>{item.harga_jual}</TableBodyCell>
              <TableBodyCell>{item.tanggal_expired}</TableBodyCell>
              <TableBodyCell>{item.jumlah}</TableBodyCell>
            </TableBodyRow>
          {/each}
        </TableBody>
      </Table>
    </TabItem>
    <TabItem
      open={isTglExp}
      title="Tanggal Expired"
      on:click={() => {
        isDataObat = false;
        isStokObat = false;
        isTglExp = true;
        isStockAlert = false;
      }}
    >
      <p class="text-sm text-gray-500 dark:text-gray-400">
        <b>Tanggal Expired:</b>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
        incididunt ut labore et dolore magna aliqua.
      </p>
    </TabItem>
    <TabItem
      open={isStockAlert}
      title="Stok < 25"
      on:click={() => {
        isDataObat = false;
        isStokObat = false;
        isTglExp = false;
        isStockAlert = true;
      }}
    ></TabItem>
  </Tabs>
</main>

<style lang="postcss">
  :global(html) {
    background-color: theme(colors.gray.100);
  }
</style>
