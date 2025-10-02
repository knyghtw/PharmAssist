<script>
  import { fly } from "svelte/transition";

  import {
    Button,
    Datepicker,
    Dropdown,
    DropdownItem,
    DropdownHeader,
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
    AdjustmentsHorizontalSolid,
    CheckCircleOutline,
    PlusOutline,
    BellActiveSolid,
    BellSolid,
    EyeSolid,
    ExclamationCircleOutline,
    ChevronDownOutline,
    InfoCircleSolid,
    PenSolid,
    TrashBinSolid,
  } from "flowbite-svelte-icons";

  import Database from "@tauri-apps/plugin-sql";
  import pbfService from "./services/pbfService";
  import barangService from "./services/barangService";
  import stokService from "./services/stokService";
  import { event } from "@tauri-apps/api";

  let inputRef;

  let notificationGranted = false;
  let dropdownPBFOpen = $state(false);
  let clickCreateDataModal = $state(false);
  let clickCreatePBFModal = $state(false);
  let clickCreateStokModal = $state(false);
  let clickEditPBFModal = $state(false);
  let clickEditBarangModal = $state(false);
  let clickEditStokModal = $state(false);
  let isPBF = $state(false);
  let isDataObat = $state(false);
  let isStokObat = $state(true);
  let isTglExp = $state(false);
  let isStockAlert = $state(false);
  let deleteConfirmation = $state(false);
  let deletePBFAlert = $state(false);
  let deleteBarangAlert = $state(false);
  let deleteStokAlert = $state(false);
  let resetConfirmation = $state(false);
  let resetPBFAlert = $state(false);
  let resetBarangAlert = $state(false);
  let resetStokAlert = $state(false);
  let addDataAction = $state(false);
  let editDataAction = $state(false);
  let deleteDataAction = $state(false);
  let resetDataAction = $state(false);
  let actionSuccess = $state(false);
  let showSuggestionsBarang = $state(false);
  let showSuggestionsPBF = $state(false);
  let newNotification = $state(false);
  let hasClickedNotification = $state(false);

  let searchTermBarang = $state("");
  let searchTermStok = $state("");
  let searchPBF = $state("");
  let searchStockAlert = $state("");
  let selectedPBF = $state("Pilih PBF disini");
  let selectedBarang = $state("");
  let selectedStok = $state("");
  let selectedPBFId = $state(0);
  let selectedBarangId = $state(0);
  let selectedStokId = $state(0);

  let items_barang = $state([
    {
      id_barang: 0,
      nama_barang: "",
      satuan: "",
      jml_stok: 0,
    },
  ]);

  let items_stok = $state([
    {
      id_stok: 0,
      id_barang: null,
      nama_barang: "",
      no_batch: "",
      harga_beli_per_satuan: null,
      harga_jual_per_satuan: null,
      tanggal_expired: "",
      jumlah: null,
    },
  ]);

  let expw_items_stok = $state([
    {
      id_stok: 0,
      id_barang: null,
      nama_barang: "",
      no_batch: "",
      harga_beli_per_satuan: null,
      harga_jual_per_satuan: null,
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

  let suggestionsBarang = $state([]);
  let suggestionsPBF = $state([]);

  let nama_pbf = $state("");
  let nama_barang = $state("");
  let satuan = $state("");

  let nomor_batch = $state("");
  let harga_beli_per_satuan = $state(0);
  let harga_jual_per_satuan = $state(0);
  let tanggal_expired = $state(null);
  let jumlah_stok = $state(0);

  async function getPBF() {
    const pbf = await pbfService.getItems();
    items_pbf = pbf;
  }

  async function getBarang() {
    const barang = await barangService.getItems();
    items_barang = barang;
  }

  async function getStok() {
    const stok = await stokService.getItems();
    items_stok = stok;
  }

  async function getExpiryWarnItems() {
    const expiryWarnItems = await stokService.getExpiryWarnItems();
    expw_items_stok = expiryWarnItems;
    if (expw_items_stok.length > 0 && !hasClickedNotification) {
      newNotification = true;
    } else {
      newNotification = false;
    }
  }

  async function getItems() {
    try {
      await getPBF();
      await getBarang();
      await getStok();
      await getExpiryWarnItems();
    } catch (error) {
      console.error(error);
      alert("Gagal mengambil data");
    }
  }

  getItems();

  async function setupNotification() {
    notificationGranted = await isPermissionGranted();

    if (!notificationGranted) {
      const permission = await requestPermission();
      notificationGranted = permission === "granted";
    } else {
      await getItems();
      if (expw_items_stok.length > 0) {
        sendNotification({
          title: "Peringatan Barang Expired",
          body:
            "Terdapat " +
            expw_items_stok.length +
            " barang yang akan mendekati tanggal kedaluwarsa",
        });
      }
    }
  }

  setupNotification();

  async function getBarangItem(id_barang) {
    try {
      const barang = await barangService.getItem(id_barang);
      selectedBarangId = barang.id_barang;
      nama_barang = barang.nama_barang;
      satuan = barang.satuan;
    } catch (error) {
      console.error(error);
      alert("Gagal mengambil data barang");
    }
  }

  async function setPBF() {
    try {
      const result = await pbfService.createItem(nama_pbf);
      actionSuccess = true;
      addDataAction = true;
      await getItems();
      nama_pbf = "";
    } catch (error) {
      alert(error.message);
    }
  }

  async function setDataObat() {
    try {
      const result = await barangService.createItem(nama_barang, satuan);
      actionSuccess = true;
      addDataAction = true;
      await getItems();
      nama_barang = "";
      satuan = "";
    } catch (error) {
      alert(error.message);
    }
  }

  async function setStokObat() {
    try {
      const result = await stokService.createItem(
        selectedBarangId,
        selectedPBFId,
        nomor_batch,
        harga_beli_per_satuan,
        harga_jual_per_satuan,
        tanggal_expired.toISOString().slice(0, 10),
        jumlah_stok
      );
      actionSuccess = true;
      addDataAction = true;
      nama_barang = "";
      nama_pbf = "";
      selectedBarangId = 0;
      selectedPBFId = 0;
      nomor_batch = "";
      harga_beli_per_satuan = 0;
      harga_jual_per_satuan = 0;
      tanggal_expired = null;
      jumlah_stok = 0;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function updateBarang(selectedBarangId, nama_barang, satuan) {
    try {
      const result = await barangService.updateItem(
        selectedBarangId,
        nama_barang,
        satuan
      );
      actionSuccess = true;
      editDataAction = true;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function updatePBF() {
    try {
      const result = await pbfService.updatePBF(nama_pbf, selectedPBFId);
      actionSuccess = true;
      editDataAction = true;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function updateStok(
    selectedStokId,
    selectedBarangId,
    selectedPBFId,
    no_batch,
    harga_beli_per_satuan,
    harga_jual_per_satuan,
    tanggal_expired,
    jumlah_stok
  ) {
    try {
      const result = await stokService.updateItem(
        selectedStokId,
        selectedBarangId,
        selectedPBFId,
        no_batch,
        harga_beli_per_satuan,
        harga_jual_per_satuan,
        tanggal_expired.toISOString().slice(0, 10),
        jumlah_stok
      );
      actionSuccess = true;
      editDataAction = true;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function deletePBF() {
    try {
      const result = await pbfService.deleteItem(selectedPBF);
      actionSuccess = true;
      deleteDataAction = true;
      selectedPBF = "Pilih PBF disini";
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function deleteBarang() {
    try {
      const result = await barangService.deleteItem(selectedBarangId);
      actionSuccess = true;
      deleteDataAction = true;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function deleteStok() {
    try {
      const result = await stokService.deleteItem(selectedStokId);
      actionSuccess = true;
      deleteDataAction = true;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function resetPBF() {
    try {
      const result = await pbfService.resetPBF();
      actionSuccess = true;
      resetDataAction = true;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function resetStok() {
    try {
      const result = await stokService.resetStok();
      actionSuccess = true;
      resetDataAction = true;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  async function resetBarang() {
    try {
      const result = await barangService.resetBarang();
      actionSuccess = true;
      resetDataAction = true;
      await getItems();
    } catch (error) {
      alert(error.message);
    }
  }

  const autocompleteBarang = (event) => {
    if (nama_barang.length <= 1) {
      suggestionsBarang = [];
      showSuggestionsBarang = false;
      return;
    }

    const filteredItems = items_barang.filter((item) =>
      item.nama_barang.toLowerCase().includes(event.target.value.toLowerCase())
    );

    suggestionsBarang = filteredItems.slice(0, 5);
    showSuggestionsBarang = true;
  };

  const autocompletePBF = (event) => {
    if (nama_pbf.length <= 1) {
      suggestionsPBF = [];
      showSuggestionsPBF = false;
      return;
    }

    const filteredItems = items_pbf.filter((item) =>
      item.nama_pbf.toLowerCase().includes(event.target.value.toLowerCase())
    );

    suggestionsPBF = filteredItems.slice(0, 5);
    showSuggestionsPBF = true;
  };

  const selectSuggestionBarang = (selectedItem) => {
    nama_barang = selectedItem.nama_barang;
    selectedBarangId = selectedItem.id_barang;
    suggestionsBarang = [];
    showSuggestionsBarang = false;
  };

  const selectSuggestionPBF = (selectedItem) => {
    nama_pbf = selectedItem.nama_pbf;
    selectedPBFId = selectedItem.id_pbf;
    suggestionsPBF = [];
    showSuggestionsPBF = false;
  };

  const formatTanggal = (isoDate) => {
    const date = new Date(isoDate);
    return date.toLocaleDateString("id-ID", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
    });
  };
</script>

<main class="m-4">
  <Modal bind:open={resetConfirmation} size="xs" autoclose outsideclose>
    <div class="text-center">
      <ExclamationCircleOutline
        class="mx-auto mb-4 text-gray-400 w-12 h-12 dark:text-gray-200"
      />

      <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
        Apakah anda yakin ingin menghapus seluruh data <b
          >{#if resetPBFAlert}PBF{:else if resetBarangAlert}Data Obat{:else if resetStokAlert}Stok
            Obat{/if}</b
        >?
      </h3>
      <Button
        color="red"
        class="me-2"
        onclick={() => {
          if (resetPBFAlert) {
            resetPBF();
          } else if (resetBarangAlert) {
            resetBarang();
          } else if (resetStokAlert) {
            resetStok();
          }
          selectedPBF = "Pilih PBF disini";
          resetPBFAlert = false;
          resetBarangAlert = false;
          resetStokAlert = false;
          resetConfirmation = false;
        }}>Ya</Button
      >
      <Button
        color="alternative"
        onclick={() => {
          selectedPBF = "Pilih PBF disini";
          resetPBFAlert = false;
          resetBarangAlert = false;
          resetStokAlert = false;
        }}>Tidak</Button
      >
    </div>
  </Modal>
  <Modal bind:open={actionSuccess} size="xs" autoclose outsideclose>
    <div class="text-center">
      <CheckCircleOutline
        class="mx-auto mb-4 text-green-400 w-12 h-12 dark:text-green-200"
      />
      <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
        Data berhasil {#if addDataAction}ditambahkan{:else if editDataAction}diubah{:else if deleteDataAction}dihapus{:else if resetDataAction}dibersihkan{/if}
      </h3>
      <Button
        color="green"
        class="me-2"
        onclick={() => {
          actionSuccess = false;
          addDataAction = false;
          editDataAction = false;
          deleteDataAction = false;
          resetDataAction = false;
        }}>Tutup</Button
      >
    </div>
  </Modal>
  <Modal bind:open={deleteConfirmation} size="xs" autoclose outsideclose>
    <div class="text-center">
      <ExclamationCircleOutline
        class="mx-auto mb-4 text-gray-400 w-12 h-12 dark:text-gray-200"
      />

      <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
        Apakah anda yakin ingin menghapus {#if deletePBFAlert}{selectedPBF}{:else if deleteBarangAlert}{selectedBarang}{:else if deleteStokAlert}Stok
          {selectedBarang}{/if}?
      </h3>
      <Button
        color="red"
        class="me-2"
        onclick={() => {
          if (deletePBFAlert) {
            deletePBF();
          } else if (deleteBarangAlert) {
            deleteBarang();
          } else if (deleteStokAlert) {
            deleteStok();
          }
          selectedPBF = "Pilih PBF disini";
          deletePBFAlert = false;
          deleteBarangAlert = false;
          deleteStokAlert = false;
          deleteConfirmation = false;
        }}>Ya</Button
      >
      <Button
        color="alternative"
        onclick={() => {
          selectedPBF = "Pilih PBF disini";
          deletePBFAlert = false;
          deleteBarangAlert = false;
          deleteStokAlert = false;
        }}>Tidak</Button
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
        clickEditPBFModal = false;
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
          >Ubah</Button
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
  <Modal bind:open={clickEditBarangModal} autoclose={false} outsideclose>
    <form
      class="flex flex-col space-y-4"
      onsubmit={() => {
        updateBarang(selectedBarangId, nama_barang, satuan);
        clickEditBarangModal = false;
      }}
    >
      <h3 class="text-xl font-medium text-gray-900 dark:text-white">
        Edit Data Barang
      </h3>
      <hr />
      <Label class="space-y-2">
        <span class="text-gray-900">Nama PBF</span>
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
      <div class="flex flex-row justify-between space-x-4">
        <Button
          class="flex flex-1"
          type="submit"
          disabled={!nama_barang || !satuan}>Ubah</Button
        >
        <Button
          class="flex flex-1"
          type="submit"
          color="alternative"
          onclick={() => {
            clickEditBarangModal = false;
          }}
        >
          Batal
        </Button>
      </div>
    </form>
  </Modal>
  <Modal bind:open={clickEditStokModal} autoclose={false} outsideclose>
    <form
      class="flex flex-col space-y-4"
      autocomplete="off"
      onsubmit={() => {
        updateStok(
          selectedStokId,
          selectedBarangId,
          selectedPBFId,
          nomor_batch,
          harga_beli_per_satuan,
          harga_jual_per_satuan,
          tanggal_expired,
          jumlah_stok
        );
        clickEditStokModal = false;
      }}
    >
      <h3 class="text-xl font-medium text-gray-900 dark:text-white">
        Edit Data Stok
      </h3>
      <hr />
      <Label class="space-y-2">
        <span class="text-gray-900">Nama Obat</span>
        <div onfocusout={() => (showSuggestionsBarang = false)}>
          <Input
            bind:value={nama_barang}
            type="text"
            name="nama_barang"
            placeholder="Paracetamol"
            class="font-normal"
            onkeydown={autocompleteBarang}
            onfocus={() => {
              if (nama_barang.length > 0) showSuggestionsBarang = true;
            }}
            required
          />
          {#if showSuggestionsBarang && suggestionsBarang.length > 0}
            <div
              class="absolute z-10 w-full bg-white border border-gray-200 rounded-md shadow-lg mt-1"
            >
              {#each suggestionsBarang as item}
                <!-- svelte-ignore a11y_click_events_have_key_events -->
                <!-- svelte-ignore a11y_no_static_element_interactions -->
                <div
                  class="px-4 py-2 hover:bg-gray-100 cursor-pointer"
                  onmousedown={() => selectSuggestionBarang(item)}
                >
                  {item.nama_barang}
                </div>
              {/each}
            </div>
          {/if}
        </div>
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">PBF</span>
        <div onfocusout={() => (showSuggestionsPBF = false)}>
          <Input
            bind:value={nama_pbf}
            type="text"
            name="nama_pbf"
            placeholder="PT ABC"
            class="font-normal"
            onkeydown={autocompletePBF}
            onfocus={() => {
              if (nama_pbf.length > 0) showSuggestionsPBF = true;
            }}
            required
          />
          {#if showSuggestionsPBF && suggestionsPBF.length > 0}
            <div
              class="absolute z-10 w-full bg-white border border-gray-200 rounded-md shadow-lg mt-1"
            >
              {#each suggestionsPBF as item}
                <!-- svelte-ignore a11y_click_events_have_key_events -->
                <!-- svelte-ignore a11y_no_static_element_interactions -->
                <div
                  class="px-4 py-2 hover:bg-gray-100 cursor-pointer"
                  onmousedown={() => selectSuggestionPBF(item)}
                >
                  {item.nama_pbf}
                </div>
              {/each}
            </div>
          {/if}
        </div>
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Nomor Batch</span>
        <Input
          bind:value={nomor_batch}
          type="text"
          name="nomor_batch"
          placeholder="ABCDEF12345"
          class="font-normal"
          required
        />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Harga Beli</span>
        <Input
          bind:value={harga_beli_per_satuan}
          type="number"
          name="harga_beli_per_satuan"
          placeholder="10000"
          class="font-normal"
          required
        />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Harga Jual</span>
        <Input
          bind:value={harga_jual_per_satuan}
          type="number"
          name="harga_jual_per_satuan"
          placeholder="11000"
          class="font-normal"
          required
        />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Tanggal Expired</span>
        <Datepicker bind:value={tanggal_expired} required />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Jumlah Stok</span>
        <Input
          bind:value={jumlah_stok}
          type="number"
          name="jumlah_stok"
          placeholder="50"
          class="font-normal"
          required
        />
      </Label>
      <div class="flex flex-row justify-between space-x-4">
        <Button
          class="flex flex-1"
          type="submit"
          disabled={!selectedBarangId ||
            !selectedPBFId ||
            !nomor_batch ||
            !harga_beli_per_satuan ||
            !harga_jual_per_satuan ||
            !tanggal_expired ||
            !jumlah_stok}>Ubah</Button
        >
        <Button
          class="flex flex-1"
          type="submit"
          color="alternative"
          onclick={() => {
            clickEditStokModal = false;
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
  <Modal bind:open={clickCreateStokModal} autoclose={false} outsideclose>
    <form
      class="flex flex-col space-y-4"
      autocomplete="off"
      onsubmit={() => {
        setStokObat();
        clickCreateStokModal = false;
      }}
    >
      <h3 class="text-xl font-medium text-gray-900 dark:text-white">
        Tambah Data Stok Obat
      </h3>
      <hr />
      <Label class="space-y-2">
        <span class="text-gray-900">Nama Obat</span>
        <div onfocusout={() => (showSuggestionsBarang = false)}>
          <Input
            bind:value={nama_barang}
            type="text"
            name="nama_barang"
            placeholder="Paracetamol"
            class="font-normal"
            onkeydown={autocompleteBarang}
            onfocus={() => {
              if (nama_barang.length > 0) showSuggestionsBarang = true;
            }}
            required
          />
          {#if showSuggestionsBarang && suggestionsBarang.length > 0}
            <div
              class="absolute z-10 w-full bg-white border border-gray-200 rounded-md shadow-lg mt-1"
            >
              {#each suggestionsBarang as item}
                <!-- svelte-ignore a11y_click_events_have_key_events -->
                <!-- svelte-ignore a11y_no_static_element_interactions -->
                <div
                  class="px-4 py-2 hover:bg-gray-100 cursor-pointer"
                  onmousedown={() => selectSuggestionBarang(item)}
                >
                  {item.nama_barang}
                </div>
              {/each}
            </div>
          {/if}
        </div>
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">PBF</span>
        <div onfocusout={() => (showSuggestionsPBF = false)}>
          <Input
            bind:value={nama_pbf}
            type="text"
            name="nama_pbf"
            placeholder="PT ABC"
            class="font-normal"
            onkeydown={autocompletePBF}
            onfocus={() => {
              if (nama_pbf.length > 0) showSuggestionsPBF = true;
            }}
            required
          />
          {#if showSuggestionsPBF && suggestionsPBF.length > 0}
            <div
              class="absolute z-10 w-full bg-white border border-gray-200 rounded-md shadow-lg mt-1"
            >
              {#each suggestionsPBF as item}
                <!-- svelte-ignore a11y_click_events_have_key_events -->
                <!-- svelte-ignore a11y_no_static_element_interactions -->
                <div
                  class="px-4 py-2 hover:bg-gray-100 cursor-pointer"
                  onmousedown={() => selectSuggestionPBF(item)}
                >
                  {item.nama_pbf}
                </div>
              {/each}
            </div>
          {/if}
        </div>
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Nomor Batch</span>
        <Input
          bind:value={nomor_batch}
          type="text"
          name="nomor_batch"
          placeholder="ABCDEF12345"
          class="font-normal"
          required
        />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Harga Beli</span>
        <Input
          bind:value={harga_beli_per_satuan}
          type="number"
          name="harga_beli_per_satuan"
          placeholder="10000"
          class="font-normal"
          required
        />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Harga Jual</span>
        <Input
          bind:value={harga_jual_per_satuan}
          type="number"
          name="harga_jual_per_satuan"
          placeholder="11000"
          class="font-normal"
          required
        />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Tanggal Expired</span>
        <Datepicker bind:value={tanggal_expired} required />
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Jumlah Stok</span>
        <Input
          bind:value={jumlah_stok}
          type="number"
          name="jumlah_stok"
          placeholder="50"
          class="font-normal"
          required
        />
      </Label>
      <hr />
      <div class="flex flex-row justify-between space-x-4">
        <Button
          class="flex flex-1"
          type="submit"
          disabled={!selectedBarangId ||
            !selectedPBFId ||
            !nomor_batch ||
            !harga_beli_per_satuan ||
            !harga_jual_per_satuan ||
            !tanggal_expired ||
            !jumlah_stok}>Simpan</Button
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
    <div class="flex space-x-2">
      <div
        id="appSettings"
        class="inline-flex items-center text-sm font-medium text-center text-gray-500 hover:text-gray-900 focus:outline-none dark:hover:text-white dark:text-gray-400 hover:cursor-pointer"
      >
        <AdjustmentsHorizontalSolid class="w-8 h-8" />
      </div>
      <Dropdown triggeredBy="#appSettings">
        <div slot="header" class="text-center py-2 font-bold">Pengaturan</div>
        <DropdownItem
          class="flex space-x-4 rtl:space-x-reverse"
          onclick={() => {
            resetConfirmation = true;
            resetPBFAlert = true;
          }}
        >
          Reset PBF
        </DropdownItem>
        <DropdownItem
          class="flex space-x-4 rtl:space-x-reverse"
          onclick={() => {
            resetConfirmation = true;
            resetBarangAlert = true;
          }}
        >
          Reset Data Obat
        </DropdownItem>
        <DropdownItem
          class="flex space-x-4 rtl:space-x-reverse"
          onclick={() => {
            resetConfirmation = true;
            resetStokAlert = true;
          }}
        >
          Reset Stok Obat
        </DropdownItem>
      </Dropdown>
      <div
        id="itemExpNotification"
        class="inline-flex items-center text-sm font-medium text-center text-gray-500 hover:text-gray-900 focus:outline-none dark:hover:text-white dark:text-gray-400 hover:cursor-pointer"
        data-placement="left-start"
      >
        {#if newNotification && !hasClickedNotification}
          <BellActiveSolid
            class="w-8 h-8"
            onclick={() => {
              newNotification = false;
              hasClickedNotification = true;
            }}
          />
          <div class="flex relative">
            <div
              class="inline-flex relative -top-2 end-4 w-3 h-3 bg-red-500 rounded-full border-2 border-white dark:border-gray-900"
            ></div>
          </div>
        {:else}
          <BellSolid class="w-8 h-8" />
        {/if}
        <Dropdown triggeredBy="#itemExpNotification">
          <div slot="header" class="text-center py-2 font-bold">Notifikasi</div>
          {#each expw_items_stok
            .filter((item) => item.tanggal_expired)
            .slice(0, 3) as item}
            <DropdownItem
              class="flex space-x-4 rtl:space-x-reverse"
              onclick={() => {
                isPBF = false;
                isDataObat = false;
                isStokObat = false;
                isTglExp = true;
                isStockAlert = false;
                searchStockAlert = item.nama_barang;
              }}
            >
              <div class="flex flex-col space-y-2">
                <div class="flex font-bold">{item.nama_barang}</div>
                <div class="text-sm mb-1.5">
                  Tgl Expired: <div class="text-sm font-bold text-primary-600">
                    {formatTanggal(item.tanggal_expired)}
                  </div>
                </div>
              </div>
            </DropdownItem>
          {/each}
          <DropdownItem
            slot="footer"
            onclick={() => {
              isPBF = false;
              isDataObat = false;
              isStokObat = false;
              isTglExp = true;
              isStockAlert = false;
            }}
            class="block py-2 -my-1 text-sm font-medium text-center text-gray-900 bg-gray-50 hover:bg-gray-100 dark:bg-gray-800 dark:hover:bg-gray-700 dark:text-white"
          >
            <div class="inline-flex items-center">
              <EyeSolid class="me-2 w-4 h-4 text-gray-500" />
              Lihat semua
            </div>
          </DropdownItem>
        </Dropdown>
      </div>
    </div>
  </div>
  <Tabs tabStyle="underline">
    <!-- <TabItem
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
      {#if items_pbf.length <= 0}
        <div class="flex flex-col items-center space-y-4">
          <InfoCircleSolid class="w-12 h-12 text-gray-500" />
          <p class="text-md text-gray-500">Data kosong.</p>
          <Button
            onclick={() => (clickCreatePBFModal = true)}
            class="font-medium"
          >
            <PlusOutline class="w-5 h-5 me-2" />Tambah Data
          </Button>
        </div>
      {:else}
        <div class="flex space-x-4 mb-4 justify-between">
          <input
            type="text"
            class="border border-gray-400 p-2 rounded w-full flex-1"
            placeholder="Cari data PBF..."
            bind:value={searchPBF}
          />
          <Button onclick={() => (clickCreatePBFModal = true)}>
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
                          deleteConfirmation = true;
                          deletePBFAlert = true;
                        }}><TrashBinSolid class="w-6 h-6" /></Button
                      >
                    </div>
                  </TableBodyCell>
                </TableBodyRow>
              {/each}
            </TableBody>
          </Table>
        </p>
      {/if}
    </TabItem> -->
    <!-- <TabItem
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
      {#if items_barang.length <= 0}
        <div class="flex flex-col items-center space-y-4">
          <InfoCircleSolid class="w-12 h-12 text-gray-500" />
          <p class="text-md text-gray-500">Data kosong.</p>
          <Button
            onclick={() => (clickCreateDataModal = true)}
            class="font-medium"
          >
            <PlusOutline class="w-5 h-5 me-2" />Tambah Data
          </Button>
        </div>
      {:else}
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
                      <Button
                        color="yellow"
                        pill={true}
                        class="!p-2"
                        onclick={() => {
                          getBarangItem(item.id_barang);
                          clickEditBarangModal = true;
                        }}><PenSolid class="w-6 h-6" /></Button
                      ><Button
                        color="red"
                        pill={true}
                        class="!p-2"
                        onclick={() => {
                          selectedBarangId = item.id_barang;
                          selectedBarang = item.nama_barang;
                          deleteConfirmation = true;
                          deleteBarangAlert = true;
                        }}><TrashBinSolid class="w-6 h-6" /></Button
                      >
                    </div>
                  </TableBodyCell>
                </TableBodyRow>
              {/each}
            </TableBody>
          </Table>
        </p>
      {/if}
    </TabItem> -->
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
      {#if items_stok.length <= 0 || items_stok == null}
        <div class="flex flex-col items-center space-y-4">
          <InfoCircleSolid class="w-12 h-12 text-gray-500" />
          <p class="text-md text-gray-500">Data kosong.</p>
          <Button
            onclick={() => (clickCreateStokModal = true)}
            class="font-medium"
          >
            <PlusOutline class="w-5 h-5 me-2" />Tambah Data
          </Button>
        </div>
      {:else}
        <div class="flex space-x-4 mb-4 justify-between">
          <input
            type="text"
            class="border border-gray-400 p-2 rounded w-full flex-1"
            placeholder="Cari data stok..."
            bind:value={searchTermStok}
          />
          <Button onclick={() => (clickCreateStokModal = true)}>
            <PlusOutline class="w-5 h-5 me-2" />Tambah Data
          </Button>
        </div>

        <Table innerDivClass="left-0 my-2" hoverable={true}>
          <TableHead>
            <TableHeadCell>No</TableHeadCell>
            <TableHeadCell>Tgl Pengisian</TableHeadCell>
            <TableHeadCell>Nama Obat</TableHeadCell>
            <TableHeadCell>PBF</TableHeadCell>
            <TableHeadCell>No Batch</TableHeadCell>
            <TableHeadCell>Harga Beli</TableHeadCell>
            <TableHeadCell>Harga Jual</TableHeadCell>
            <TableHeadCell>Tanggal Expired</TableHeadCell>
            <TableHeadCell>Jumlah Stok</TableHeadCell>
            <TableHeadCell>
              <span class="sr-only">Aksi</span>
            </TableHeadCell>
          </TableHead>
          <TableBody tableBodyClass="divide-y">
            {#each items_stok.filter((item) => item.nama_barang
                .toLowerCase()
                .includes(searchTermStok.toLowerCase())) as item, index}
              <TableBodyRow>
                <TableBodyCell>{index + 1}</TableBodyCell>
                <TableBodyCell>{item.tanggal}</TableBodyCell>
                <TableBodyCell>{item.nama_barang}</TableBodyCell>
                <TableBodyCell>{item.nama_pbf}</TableBodyCell>
                <TableBodyCell>{item.no_batch}</TableBodyCell>
                <TableBodyCell
                  >Rp. {item.harga_beli_per_satuan}</TableBodyCell
                >
                <TableBodyCell
                  >Rp. {item.harga_jual_per_satuan}</TableBodyCell
                >
                <TableBodyCell>{item.tanggal_expired}</TableBodyCell>
                <TableBodyCell>{item.jumlah_stok}</TableBodyCell>
                <TableBodyCell>
                  <div class="flex space-x-4">
                    <Button
                      color="yellow"
                      pill={true}
                      class="!p-2"
                      onclick={() => {
                        selectedStokId = item.id_stok;
                        selectedBarangId = item.id_barang;
                        nama_barang = item.nama_barang;
                        selectedPBFId = item.id_pbf;
                        nama_pbf = item.nama_pbf;
                        nomor_batch = item.no_batch;
                        harga_beli_per_satuan = item.harga_beli_per_satuan;
                        harga_jual_per_satuan = item.harga_jual_per_satuan;
                        jumlah_stok = item.jumlah_stok;
                        clickEditStokModal = true;
                      }}
                    >
                      <PenSolid class="w-6 h-6" />
                    </Button>
                    <Button
                      color="red"
                      pill={true}
                      class="!p-2"
                      onclick={() => {
                        selectedStokId = item.id_stok;
                        selectedBarang = item.nama_barang;
                        deleteConfirmation = true;
                        deleteStokAlert = true;
                      }}><TrashBinSolid class="w-6 h-6" /></Button
                    >
                  </div>
                </TableBodyCell>
              </TableBodyRow>
            {/each}
          </TableBody>
        </Table>
      {/if}
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
      {#if expw_items_stok.length <= 0}
        <div class="flex flex-col items-center space-y-4">
          <InfoCircleSolid class="w-12 h-12 text-gray-500" />
          <p class="text-md text-gray-500">
            Tidak ada data stok yang memiliki tanggal expired &lt; 1 bulan.
          </p>
        </div>
      {:else}
        <input
          type="text"
          class="mb-4 border border-gray-400 p-2 rounded w-full flex-1"
          placeholder="Cari data stok..."
          bind:value={searchStockAlert}
        />

        <Table innerDivClass="left-0 my-2" hoverable={true}>
          <TableHead>
            <TableHeadCell>No</TableHeadCell>
            <TableHeadCell>Nama Obat</TableHeadCell>
            <TableHeadCell>PBF</TableHeadCell>
            <TableHeadCell>No Batch</TableHeadCell>
            <TableHeadCell>Harga Beli</TableHeadCell>
            <TableHeadCell>Harga Jual</TableHeadCell>
            <TableHeadCell>Tanggal Expired</TableHeadCell>
            <TableHeadCell>Jumlah Stok</TableHeadCell>
          </TableHead>
          <TableBody tableBodyClass="divide-y">
            {#each expw_items_stok.filter((item) => item.nama_barang
                .toLowerCase()
                .includes(searchStockAlert.toLowerCase())) as item, index}
              <TableBodyRow>
                <TableBodyCell>{index + 1}</TableBodyCell>
                <TableBodyCell>{item.nama_barang}</TableBodyCell>
                <TableBodyCell>{item.nama_pbf}</TableBodyCell>
                <TableBodyCell>{item.no_batch}</TableBodyCell>
                <TableBodyCell
                  >Rp. {item.harga_beli_per_satuan.toLocaleString("id-ID")}</TableBodyCell
                >
                <TableBodyCell
                  >Rp. {item.harga_jual_per_satuan.toLocaleString("id-ID")}</TableBodyCell
                >
                <TableBodyCell
                  >{formatTanggal(item.tanggal_expired)}</TableBodyCell
                >
                <TableBodyCell>{item.jumlah_stok}</TableBodyCell>
              </TableBodyRow>
            {/each}
          </TableBody>
        </Table>
      {/if}
    </TabItem>
  </Tabs>
</main>

<style lang="postcss">
  :global(html) {
    background-color: theme(colors.gray.100);
  }
</style>
