<script>
  import { fly } from "svelte/transition";

  import {
    Button,
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
    Search,
    Select,
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
    ExclamationCircleOutline,
    ChevronDownOutline,
    PenSolid,
    TrashBinSolid,
  } from "flowbite-svelte-icons";

  import Database from "@tauri-apps/plugin-sql";
  import pbfService from "./services/pbfService";
  import barangService from "./services/barangService";

  let notificationGranted = false;
  let dropdownPBFOpen = $state(false);
  let clickCreateDataModal = $state(false);
  let clickCreatePBFModal = $state(false);
  let clickEditPBFModal = $state(false);
  let isPBF = $state(true);
  let isDataObat = $state(false);
  let isStokObat = $state(false);
  let isTglExp = $state(false);
  let isStockAlert = $state(false);
  let PBFDelAlert = $state(false);

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

  let searchTermBarang = $state("");
  let searchTermStok = $state("");
  let searchPBF = $state("");
  let searchStockAlert = $state("");
  let selectedPBF = $state("Pilih PBF disini");
  let selectedPBFId = $state(0);

  let items_barang = $state([
    {
      id_obat: 0,
      nama_barang: "",
      satuan: "",
      jml_stok: 0,
    },
  ]);

  let items_stok = $state([
    {
      id_stok: 0,
      id_obat: null,
      nama_barang: "",
      no_batch: "",
      harga_beli: null,
      harga_jual: null,
      tanggal_expired: "",
      jumlah: null,
    },
  ]);

  let items_pbf = $state([
    {
      id_pbf: 0,
      nama_pbf: "",
    },
  ]);

  let nama_pbf = $state("");
  let nama_barang = $state("");
  let satuan = $state("");

  async function getPBF() {
    try {
      const pbf = await pbfService.getItems();
      items_pbf = pbf;
    } catch (error) {
      console.error(error);
      alert("Gagal mengambil data");
    }
  }

  async function getBarang() {
    try {
      const barang = await barangService.getItems();
      items_barang = barang;
    } catch (error) {
      console.error(error);
      alert("Gagal mengambil data");
    }
  }

  async function getItems() {
    try {
      await getPBF();
      await getBarang();
    } catch (error) {
      console.error(error);
      alert("Gagal mengambil data");
    }
  }

  getItems();

  async function setPBF() {
    try {
      const result = await pbfService.createItem(nama_pbf);
      alert(result.message);
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function setDataObat() {
    try {
      const result = await barangService.createItem(nama_barang, satuan);
      alert(result.message);
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function updatePBF() {
    try {
      const result = await pbfService.updatePBF(nama_pbf, selectedPBFId);
      alert(result.message);
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function deletePBF() {
    try {
      const result = await pbfService.deleteItem(selectedPBF);
      alert(result.message);
      selectedPBF = "Pilih PBF disini";
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function resetPBF() {
    try {
      const result = await pbfService.resetPBF();
      alert(result.message);
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }
</script>

<main class="m-4">
  <Modal bind:open={PBFDelAlert} size="xs" autoclose>
    <div class="text-center">
      <ExclamationCircleOutline
        class="mx-auto mb-4 text-gray-400 w-12 h-12 dark:text-gray-200"
      />
      <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
        Apakah anda yakin ingin menghapus {selectedPBF}?
      </h3>
      <Button color="red" class="me-2" onclick={() => deletePBF()}>Ya</Button>
      <Button
        color="alternative"
        onclick={() => (selectedPBF = "Pilih PBF disini")}>Tidak</Button
      >
    </div>
  </Modal>
  <Modal bind:open={clickCreatePBFModal} autoclose={false} outsideclose>
    <form
      class="flex flex-col space-y-4"
      onsubmit={() => {
        setPBF();
        clickCreatePBFModal = false;
        selectedPBF = "Pilih PBF disini";
      }}
    >
      <h3 class="text-xl font-medium text-gray-900 dark:text-white">
        Tambah Data PBF
      </h3>
      <hr />
      <Label class="space-y-2">
        <span class="text-gray-900">Nama PBF</span>
        <Input
          bind:value={nama_pbf}
          type="text"
          name="pbf"
          placeholder="PT ABC"
          class="font-normal"
          required
        />
      </Label>
      <hr />
      <div class="flex flex-row justify-between space-x-4">
        <Button class="flex flex-1" type="submit" disabled={!nama_pbf}
          >Simpan</Button
        >
        <Button
          class="flex flex-1"
          type="button"
          color="alternative"
          onclick={() => {
            clickCreatePBFModal = false;
          }}
        >
          Batal
        </Button>
      </div>
    </form>
  </Modal>
  <Modal bind:open={clickEditPBFModal} autoclose={false} outsideclose>
    <form
      class="flex flex-col space-y-4"
      onsubmit={() => {
        updatePBF();
        clickCreatePBFModal = false;
        selectedPBF = "Pilih PBF disini";
      }}
    >
      <h3 class="text-xl font-medium text-gray-900 dark:text-white">
        Edit Data PBF
      </h3>
      <hr />
      <Label class="space-y-2">
        <span class="text-gray-900">Nama PBF</span>
        <Input
          bind:value={nama_pbf}
          type="text"
          name="pbf"
          placeholder="PT ABC"
          class="font-normal"
          required
        />
      </Label>
      <div class="flex flex-row justify-between space-x-4">
        <Button class="flex flex-1" type="submit" disabled={!nama_pbf}
          >Simpan</Button
        >
        <Button
          class="flex flex-1"
          type="submit"
          color="alternative"
          onclick={() => {
            clickEditPBFModal = false;
          }}
        >
          Batal
        </Button>
      </div>
    </form>
  </Modal>
  <Modal bind:open={clickCreateDataModal} autoclose={false} outsideclose>
    <form
      class="flex flex-col space-y-4"
      onsubmit={() => {
        setDataObat();
        clickCreateDataModal = false;
        selectedPBF = "Pilih PBF disini";
      }}
    >
      <h3 class="text-xl font-medium text-gray-900 dark:text-white">
        Tambah Data
      </h3>
      <hr />
      <Label class="space-y-2">
        <span class="text-gray-900">Nama Obat</span>
        <Input
          bind:value={nama_barang}
          type="text"
          name="nama_barang"
          placeholder="Paracetamol"
          class="font-normal"
          required
        />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Satuan</span>
        <Input
          bind:value={satuan}
          type="text"
          name="satuan"
          placeholder="Tablet"
          class="font-normal"
          required
        />
      </Label>
      <hr />
      <div class="flex flex-row justify-between space-x-4">
        <Button
          class="flex flex-1"
          type="submit"
          disabled={!nama_barang && !satuan}>Simpan</Button
        >
        <Button
          class="flex flex-1"
          type="button"
          color="alternative"
          onclick={() => {
            clickCreateDataModal = false;
          }}
        >
          Batal
        </Button>
      </div>
    </form>
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
  </div>
  <Tabs tabStyle="underline">
    <TabItem
      open={isPBF}
      title="Data PBF"
      on:click={() => {
        isPBF = true;
        isDataObat = false;
        isStokObat = false;
        isTglExp = false;
        isStockAlert = false;
      }}
    >
      <div class="flex space-x-4 mb-4 justify-between">
        <input
          type="text"
          class="border border-gray-400 p-2 rounded w-full flex-1"
          placeholder="Cari data PBF..."
          bind:value={searchPBF}
        />
        <Button
          color="red"
          on:click={() => {
            resetPBF();
            getItems();
            selectedPBF = "Pilih PBF disini";
          }}>RESET PBF</Button
        >
        <Button on:click={() => (clickCreatePBFModal = true)}>
          <PlusOutline class="w-5 h-5 me-2" />Tambah Data
        </Button>
      </div>
      <p class="text-sm text-gray-500 dark:text-gray-400">
        <Table innerDivClass="left-0 my-2" hoverable={true}>
          <TableHead>
            <TableHeadCell>No</TableHeadCell>
            <TableHeadCell>Nama PBF</TableHeadCell>
            <TableHeadCell>
              <span class="sr-only">Aksi</span>
            </TableHeadCell>
          </TableHead>
          <TableBody tableBodyClass="divide-y">
            {#each items_pbf.filter((item) => item.nama_pbf
                .toLowerCase()
                .includes(searchPBF.toLowerCase())) as item, index}
              <TableBodyRow>
                <TableBodyCell>{index + 1}</TableBodyCell>
                <TableBodyCell>{item.nama_pbf}</TableBodyCell>
                <TableBodyCell>
                  <div class="flex space-x-4">
                    <Button
                      color="yellow"
                      pill={true}
                      class="!p-2"
                      onclick={() => {
                        selectedPBFId = item.id_pbf;
                        selectedPBF = item.nama_pbf;
                        nama_pbf = item.nama_pbf;
                        clickEditPBFModal = true;
                      }}><PenSolid class="w-6 h-6" /></Button
                    ><Button
                      color="red"
                      pill={true}
                      class="!p-2"
                      onclick={() => {
                        selectedPBF = item.nama_pbf;
                        PBFDelAlert = true;
                      }}><TrashBinSolid class="w-6 h-6" /></Button
                    >
                  </div>
                </TableBodyCell>
              </TableBodyRow>
            {/each}
            <!-- {/each} -->
          </TableBody>
        </Table>
      </p>
    </TabItem>
    <TabItem
      open={isDataObat}
      title="Data Obat"
      on:click={() => {
        isPBF = false;
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
        <Button
          on:click={() => {
            clickCreateDataModal = true;
            selectedPBF = "Pilih PBF disini";
          }}
        >
          <PlusOutline class="w-5 h-5 me-2" />Tambah Data
        </Button>
      </div>
      <p class="text-sm text-gray-500 dark:text-gray-400">
        <Table innerDivClass="left-0 my-2" hoverable={true}>
          <TableHead>
            <TableHeadCell>NO</TableHeadCell>
            <TableHeadCell>NAMA OBAT</TableHeadCell>
            <TableHeadCell>Satuan</TableHeadCell>
            <TableHeadCell>
              <span class="sr-only">Aksi</span>
            </TableHeadCell>
          </TableHead>
          <TableBody tableBodyClass="divide-y">
            {#each items_barang.filter((item) => item.nama_barang
                .toLowerCase()
                .includes(searchTermBarang.toLowerCase())) as item, index}
              <TableBodyRow>
                <TableBodyCell>{index + 1}</TableBodyCell>
                <TableBodyCell>{item.nama_barang}</TableBodyCell>
                <TableBodyCell>{item.satuan}</TableBodyCell>
                <TableBodyCell>{item.jml_stok}</TableBodyCell>
                <TableBodyCell>
                  <div class="flex space-x-4">
                    <Button color="yellow" pill={true} class="!p-2"
                      ><PenSolid class="w-6 h-6" /></Button
                    ><Button color="red" pill={true} class="!p-2"
                      ><TrashBinSolid class="w-6 h-6" /></Button
                    >
                  </div>
                </TableBodyCell>
              </TableBodyRow>
            {/each}
            <!-- {/each} -->
          </TableBody>
        </Table>
      </p>
    </TabItem>
    <TabItem
      open={isStokObat}
      title="Stok Obat"
      on:click={() => {
        isPBF = false;
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
          <!-- {#each items_stok.filter((item) => item.nama_barang
              .toLowerCase()
              .includes(searchTermStok.toLowerCase())) as item} -->
          {#each items_stok.filter((item) => item.nama_barang
              .toLowerCase()
              .includes(searchTermBarang.toLowerCase())) as item, index}
            <TableBodyRow>
              <TableBodyCell>{index + 1}</TableBodyCell>
              <TableBodyCell>{item.nama_barang}</TableBodyCell>
              <TableBodyCell>{item.no_batch}</TableBodyCell>
              <TableBodyCell>{item.harga_beli}</TableBodyCell>
              <TableBodyCell>{item.harga_jual}</TableBodyCell>
              <TableBodyCell>{item.tanggal_expired}</TableBodyCell>
              <TableBodyCell>{item.jumlah}</TableBodyCell>
            </TableBodyRow>
          {/each}
          <!-- {/each} -->
        </TableBody>
      </Table>
    </TabItem>
    <TabItem
      open={isTglExp}
      title="Tanggal Expired"
      on:click={() => {
        isPBF = false;
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
        isPBF = false;
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
