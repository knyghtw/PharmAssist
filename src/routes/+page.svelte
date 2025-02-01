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

  let notificationGranted = false;
  let dropdownPBFOpen = $state(false);
  let clickCreateDataModal = $state(false);
  let clickCreatePBFModal = $state(false);
  let clickEditPBFModal = $state(false);
  let isPBF = $state(true);
  let isFromDataObat = $state(false);
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
      nama_obat: "",
      satuan: "",
      jml_stok: 0,
    },
  ]);

  let items_stok = $state([
    {
      id_stok: 0,
      id_obat: null,
      nama_obat: "",
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
  let nama_obat = $state("");
  let satuan = $state("");

  async function getItems() {
    try {
      const db = await Database.load("sqlite:test.db");
      const dbItemBarang = await db.select("SELECT * FROM barang");
      const dbItemStok = await db.select("SELECT * FROM stok_obat");
      const dbItemPBF = await db.select("SELECT * FROM pbf");

      items_barang = dbItemBarang;
      items_stok = dbItemStok;
      items_pbf = dbItemPBF;
    } catch (error) {
      console.log(error);
    }
  }

  getItems();

  async function setPBF() {
    try {
      const db = await Database.load("sqlite:test.db");
      const existingPBF = await db.select(
        "SELECT nama_pbf FROM pbf WHERE nama_pbf = $1",
        [nama_pbf]
      );

      if (existingPBF.length > 0) {
        throw new Error(`PBF dengan nama "${nama_pbf}" sudah ada`);
      }

      const result = await db.execute(
        "INSERT INTO pbf (nama_pbf) VALUES ($1)",
        [nama_pbf]
      );
      if (result) {
        alert("Added PBF: " + nama_pbf);
        console.log("Added PBF: " + nama_pbf);
        getItems();
        return { success: true, message: "Data PBF berhasil ditambahkan" };
      }
    } catch (error) {
      alert(error);
      console.log(error);
      return { success: false, message: error };
    }
  }

  async function setDataObat() {
    try {
      const db = await Database.load("sqlite:test.db");
      const existingData = await db.select(
        "SELECT nama_obat FROM barang WHERE nama_obat = $1",
        [nama_obat]
      );

      if (existingData.length > 0) {
        throw new Error(`PBF dengan nama "${nama_pbf}" sudah ada`);
      }

      const result = await db.execute(
        "INSERT INTO barang (nama_obat, satuan, total_stok) VALUES ($1, $2, 0)",
        [nama_obat, satuan]
      );
      if (result) {
        alert("Added PBF: " + nama_obat);
        console.log("Added PBF: " + nama_obat);
        getItems();
        return { success: true, message: "Data PBF berhasil ditambahkan" };
      }
    } catch (error) {
      alert(error);
      console.log(error);
      return { success: false, message: error };
    }
  }

  async function updatePBF() {
    try {
      const db = await Database.load("sqlite:test.db");
      await db.execute("UPDATE pbf SET nama_pbf = $1 WHERE id_pbf = $2", [
        nama_pbf,
        selectedPBFId,
      ]);
      alert("PBF updated success");
      console.log("PBF updated success");
      getItems();
      return { success: true, message: "Data PBF berhasil diubah" };
    } catch (error) {
      alert(error);
      console.log(error);
      return { success: false, message: error };
    }
  }

  async function deletePBF() {
    try {
      const db = await Database.load("sqlite:test.db");
      await db.execute("DELETE FROM pbf WHERE nama_pbf = $1", [selectedPBF]);
      alert("PBF " + selectedPBF + " berhasil dihapus");
      console.log("PBF deleted success");
      selectedPBF = "Pilih PBF disini";
      getItems();
    } catch (error) {
      alert("Error: " + error);
      console.log("Error: " + error);
    }
  }

  async function resetPBF() {
    try {
      const db = await Database.load("sqlite:test.db");
      await db.execute("DELETE FROM pbf");
      alert("PBF reset success");
      console.log("PBF reset success");
    } catch (error) {
      alert("Error: " + error);
      console.log("Error: " + error);
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
      <div class="flex flex-row justify-between space-x-4">
        <Button class="flex flex-1" type="submit" disabled={!nama_pbf}
          >Simpan</Button
        >
        {#if isFromDataObat}
          <Button
            class="flex flex-1"
            type="button"
            color="alternative"
            onclick={() => {
              isPBF = false;
              isDataObat = true;
              clickCreatePBFModal = false;
              clickCreateDataModal = true;
            }}
          >
            Kembali
          </Button>
        {:else}
          <Button
            class="flex flex-1"
            type="button"
            color="alternative"
            onclick={() => {
              clickEditPBFModal = false;
            }}
          >
            Batal
          </Button>
        {/if}
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
          type="button"
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
          bind:value={nama_obat}
          type="text"
          name="nama_obat"
          placeholder="Paracetamol"
          class="font-normal"
          required
        />
      </Label>
      <!-- <Label class="space-y-2">
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
        <Select bind:value={selectedPBF} class="w-full">
          {#each items_pbf as item_pbf}
            <option value={item_pbf.nama_pbf}>{item_pbf.nama_pbf}</option>
          {/each}
        </Select>
      </Label> -->
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
      <!-- <Label class="space-y-2">
        <span class="text-gray-900">Diskon (%)</span>
        <Input
          type="number"
          name="diskon"
          placeholder="10"
          class="font-normal"
          required
        />
      </Label> -->
      <hr />
      <Button
        on:click={() => {
          setDataObat();
          clickCreateDataModal = false;
        }}>Tambah</Button
      >
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
            <TableHeadCell sort={(a, b) => a.id_obat - b.id_obat}
              >No</TableHeadCell
            >
            <TableHeadCell
              sort={(a, b) => a.nama_obat.localeCompare(b.nama_obat)}
              >Nama Obat</TableHeadCell
            >
            <TableHeadCell>Satuan</TableHeadCell>
            <TableHeadCell sort={(a, b) => a.jml_stok - b.jml_stok}
              >Jumlah Stok</TableHeadCell
            >
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
          <!-- {#each items_stok.filter((item) => item.nama_obat
              .toLowerCase()
              .includes(searchTermStok.toLowerCase())) as item} -->
          {#each items_stok as item}
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
