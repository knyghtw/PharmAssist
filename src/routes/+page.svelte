<script>
  import { fly } from "svelte/transition";

  import {
    Button,
    Datepicker,
    Dropdown,
    DropdownItem,
    DropdownGroup,
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
    Checkbox,
    DropdownDivider,
    Helper,
  } from "flowbite-svelte";

  import {
    isPermissionGranted,
    requestPermission,
    sendNotification,
  } from "@tauri-apps/plugin-notification";

  import {
    AdjustmentsHorizontalSolid,
    AngleDownOutline,
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
    CloseCircleOutline,
    AngleUpOutline,
  } from "flowbite-svelte-icons";

  import { slide } from "svelte/transition";

  import Database from "@tauri-apps/plugin-sql";
  import pbfService from "./services/pbfService";
  import barangService from "./services/barangService";
  import stokService from "./services/stokService";
  import { event } from "@tauri-apps/api";

  let inputRef;

  let notificationGranted = false;
  let dropdownPBFOpen = $state(false);
  let clickCreateStokModal = $state(false);
  let clickEditStokModal = $state(false);
  let isPBF = $state(false);
  let isDataObat = $state(false);
  let isStokObat = $state(true);
  let isTglExp = $state(false);
  let isStockAlert = $state(false);
  let isNewBarang = $state(false);
  let isNewPBF = $state(false);
  let isPercentage = $state(false);
  let isExtended = $state(false);
  let isBtnDisabled = $state(true);
  let deleteConfirmation = $state(false);
  let resetConfirmation = $state(false);
  let resetPBFAlert = $state(false);
  let resetBarangAlert = $state(false);
  let resetStokAlert = $state(false);
  let addDataAction = $state(false);
  let editDataAction = $state(false);
  let deleteDataAction = $state(false);
  let resetDataAction = $state(false);
  let actionSuccess = $state(false);
  let actionFailed = $state(false);
  let requireBarangConfirmation = $state(false);
  let requirePBFConfirmation = $state(false);
  let showSuggestionsBarang = $state(false);
  let showSuggestionsPBF = $state(false);
  let newNotification = $state(false);
  let hasClickedNotification = $state(false);
  let showDetail = $state(false);
  let validNamaBarang = $state(true);
  let validNamaPBF = $state(true);
  let validNoBatch = $state(true);
  let validHargaBeli = $state(true);
  let validHargaJual = $state(true);
  let validTglExp = $state(true);
  let validStok = $state(true);

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
  let errorMessage = $state("");
  let openRow = $state(-1);
  let namaBarangCheck = $state("default");
  let namaPBFCheck = $state("default");
  let noBatchCheck = $state("default");
  let hargaBeliCheck = $state("default");
  let hargaJualCheck = $state("default");
  let tglExpCheck = $state("default");
  let stokCheck = $state("default");

  let items_barang = $state([
    {
      id_barang: 0,
      nama_barang: "",
    },
  ]);

  let items_stok = $state([
    {
      id_stok: 0,
      id_barang: null,
      nama_barang: "",
      no_batch: "",
      harga_beli_per_satuan: 0,
      harga_jual_per_satuan: 0,
      tanggal_expired: "",
      total_stok: null,
    },
  ]);

  let items_stok_detail = $state([
    {
      id_stok: 0,
      no_batch: "",
      harga_beli_per_satuan: 0,
      harga_jual_per_satuan: 0,
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
      harga_beli_per_satuan: 0,
      harga_jual_per_satuan: 0,
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

  let nomor_batch = $state("");
  let harga_beli_per_satuan = $state(0);
  let harga_jual_per_satuan = $state(0);
  let tanggal_expired = $state(null);
  let jumlah_stok = $state(0);

  async function getBarang() {
    items_barang = await barangService.getItems();
    suggestionsBarang = [];
    for (let i = 0; i < items_barang.length; i++) {
      suggestionsBarang.push(items_barang[i].nama_barang);
    }
  }

  async function getPBF() {
    items_pbf = await pbfService.getItems();
    suggestionsPBF = [];
    for (let i = 0; i < items_pbf.length; i++) {
      suggestionsPBF.push(items_pbf[i].nama_pbf);
    }
  }

  async function getStok() {
    items_stok = await stokService.getItems();
  }

  async function getStokDetail(id_barang, id_pbf) {
    items_stok_detail = await stokService.getDetails(id_barang, id_pbf);
  }

  async function getExactItemByName(nama_barang, nama_pbf) {
    const exactitem = await stokService.getExactItemByName(
      nama_barang,
      nama_pbf
    );
    if (exactitem.length == 1) {
      harga_beli_per_satuan = exactitem[0].harga_beli_per_satuan;
      harga_jual_per_satuan = exactitem[0].harga_jual_per_satuan;
    }
  }

  async function getExpiryWarnItems() {
    expw_items_stok = await stokService.getExpiryWarnItems();
    if (expw_items_stok.length > 0 && !hasClickedNotification) {
      newNotification = true;
    } else {
      newNotification = false;
    }
  }

  async function getItems() {
    try {
      await getBarang();
      await getPBF();
      await getStok();
      await getExpiryWarnItems();
    } catch (error) {
      actionFailed = true;
      errorMessage = error;
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

  async function clearInput() {
    nama_barang = "";
    nama_pbf = "";
    selectedBarangId = 0;
    selectedPBFId = 0;
    nomor_batch = "";
    harga_beli_per_satuan = 0;
    harga_jual_per_satuan = 0;
    tanggal_expired = null;
    jumlah_stok = 0;
    isPercentage = false;
    validNamaPBF = true;
    validNoBatch = true;
    validHargaBeli = true;
    validHargaJual = true;
    validTglExp = true;
    validStok = true;
  }

  function checkInput(input_name) {
    switch (input_name) {
      case "nama_barang":
        if (!nama_barang) {
          validNamaBarang = false;
          namaBarangCheck = "red";
        } else {
          validNamaBarang = true;
          namaBarangCheck = "default";
        }
        break;
      case "nama_pbf":
        if (!nama_pbf) {
          validNamaPBF = false;
          namaPBFCheck = "red";
        } else {
          validNamaPBF = true;
          namaPBFCheck = "default";
        }
        break;
      case "nomor_batch":
        if (!nomor_batch) {
          validNoBatch = false;
          noBatchCheck = "red";
        } else {
          validNoBatch = true;
          noBatchCheck = "default";
        }
        break;
      case "harga_beli":
        if (!harga_beli_per_satuan || harga_beli_per_satuan < 1) {
          validHargaBeli = false;
          hargaBeliCheck = "red";
        } else {
          validHargaBeli = true;
          hargaBeliCheck = "default";
        }
        break;
      case "harga_jual":
        if (
          !harga_jual_per_satuan ||
          harga_jual_per_satuan < 1 ||
          (harga_beli_per_satuan > harga_jual_per_satuan && !isPercentage) ||
          (harga_jual_per_satuan > 100 && isPercentage)
        ) {
          validHargaJual = false;
          hargaJualCheck = "red";
        } else {
          validHargaJual = true;
          hargaJualCheck = "default";
        }
        break;
      case "tgl_exp":
        if (!tanggal_expired) {
          validTglExp = false;
          tglExpCheck = "red";
        } else {
          validTglExp = true;
          tglExpCheck = "default";
        }
        break;
      case "jumlah_stok":
        if (!jumlah_stok || jumlah_stok < 1) {
          validStok = false;
          stokCheck = "red";
        } else {
          validStok = true;
          stokCheck = "default";
        }
        break;
      default:
        break;
    }
    if (
      nama_barang &&
      nama_pbf &&
      nomor_batch &&
      harga_beli_per_satuan &&
      +harga_beli_per_satuan > 1 &&
      harga_jual_per_satuan &&
      +harga_jual_per_satuan > 1 &&
      ((+harga_beli_per_satuan < +harga_jual_per_satuan && !isPercentage) ||
        (+harga_beli_per_satuan > +harga_jual_per_satuan && isPercentage)) &&
      tanggal_expired &&
      +jumlah_stok > 1
    ) {
      console.log("input false");
      isBtnDisabled = false;
    } else {
      console.log("input good");
      isBtnDisabled = true;
    }
  }

  async function setStokObat() {
    try {
      if (isPercentage) {
        const percentage = harga_jual_per_satuan;
        harga_jual_per_satuan = Math.floor(
          harga_beli_per_satuan + harga_beli_per_satuan * (percentage / 100)
        );
      }
      let trimmed_barang = nama_barang.trimEnd();
      nama_barang = trimmed_barang;
      let trimmed_pbf = nama_pbf.trimEnd();
      nama_pbf = trimmed_pbf;
      const result = await stokService.createItem(
        selectedBarangId,
        selectedPBFId,
        nama_barang,
        nama_pbf,
        nomor_batch,
        harga_beli_per_satuan,
        harga_jual_per_satuan,
        tanggal_expired.toISOString().slice(0, 10),
        jumlah_stok,
        isNewBarang,
        isNewPBF
      );

      if (result.success == true) {
        actionSuccess = true;
        addDataAction = true;
        await clearInput();
        await getItems();
        isBtnDisabled = true;
      } else if (result.message == "confirm_barang") {
        items_barang = result.data;
        requireBarangConfirmation = true;
      } else if (result.message == "confirm_pbf") {
        items_pbf = result.data;
        requirePBFConfirmation = true;
      } else if (result.success == false) {
        actionFailed = true;
        errorMessage = result.message;
      }
      openRow = -1;
    } catch (error) {
      actionFailed = true;
      errorMessage = error;
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
    if (isPercentage) {
      const percentage = harga_jual_per_satuan;
      harga_jual_per_satuan = Math.floor(
        harga_beli_per_satuan + harga_beli_per_satuan * (percentage / 100)
      );
    }
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
      await getStokDetail(selectedBarangId, selectedPBFId);
      await clearInput();
      isBtnDisabled = true;
    } catch (error) {
      actionFailed = true;
      errorMessage = error;
    }
  }

  async function deleteStok() {
    try {
      const result = await stokService.deleteItem(selectedStokId);
      actionSuccess = true;
      deleteDataAction = true;
      await getItems();
      await clearInput();
    } catch (error) {
      actionFailed = true;
      errorMessage = error;
    }
  }

  async function resetPBF() {
    try {
      const result = await pbfService.resetPBF();
      actionSuccess = true;
      resetDataAction = true;
      await getItems();
    } catch (error) {
      actionFailed = true;
      errorMessage = error;
    }
  }

  async function resetStok() {
    try {
      const result = await stokService.resetStok();
      actionSuccess = true;
      resetDataAction = true;
      await getItems();
    } catch (error) {
      actionFailed = true;
      errorMessage = error;
    }
  }

  async function resetBarang() {
    try {
      const result = await barangService.resetBarang();
      actionSuccess = true;
      resetDataAction = true;
      await getItems();
    } catch (error) {
      actionFailed = true;
      errorMessage = error;
    }
  }

  const formatTanggal = (isoDate) => {
    const date = new Date(isoDate);
    return date.toLocaleDateString("id-ID", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
    });
  };

  const toggleRow = (i) => {
    openRow = openRow === i ? null : i;
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
  <Modal bind:open={actionFailed} size="xs" autoclose outsideclose>
    <div class="text-center">
      <CloseCircleOutline
        class="mx-auto mb-1 text-red-400 w-12 h-12 dark:text-red-200"
      />
      <h3 class="mb-1 text-lg font-semibold text-gray-500 dark:text-gray-400">
        Error!
      </h3>
      <h4 class="mb-4 text-gray-500 dark:text-gray-400">
        {errorMessage}
      </h4>
      <Button
        color="red"
        class="me-2"
        onclick={() => {
          actionFailed = false;
        }}>Tutup</Button
      >
    </div>
  </Modal>
  <Modal bind:open={requireBarangConfirmation} size="xs" autoclose outsideclose>
    <div class="text-center">
      <ExclamationCircleOutline
        class="mx-auto mb-4 text-yellow-400 w-12 h-12 dark:text-yellow-200"
      />

      <h3 class="mb-1 text-lg font-semibold text-gray-500 dark:text-gray-400">
        Peringatan
      </h3>
      <h4 class="mb-2 text-gray-500 dark:text-gray-400">
        Beberapa item mirip {nama_barang}. Silakan pilih item yang sesuai, atau
        buat data baru.
      </h4>
      <Table innerDivClass="left-0 my-2" hoverable={true}>
        <TableHead>
          <TableHeadCell>No</TableHeadCell>
          <TableHeadCell>Nama Obat</TableHeadCell>
          <TableHeadCell>
            <span class="sr-only">Aksi</span>
          </TableHeadCell>
        </TableHead>
        <TableBody tableBodyClass="divide-y">
          {#each items_barang as item, index}
            <TableBodyRow>
              <TableBodyCell>{index + 1}</TableBodyCell>
              <TableBodyCell>{item.nama_barang}</TableBodyCell>
              <TableBodyCell>
                <Button
                  color="blue"
                  class="p-2!"
                  onclick={() => {
                    selectedBarangId = item.id_barang;
                    setStokObat();
                    requireBarangConfirmation = false;
                  }}
                >
                  Pilih
                </Button>
              </TableBodyCell>
            </TableBodyRow>
          {/each}
        </TableBody>
      </Table>
      <div class="flex space-x-4">
        <Button
          color="alternative"
          class="me-2"
          onclick={() => {
            isNewBarang = true;
            setStokObat();
            requireBarangConfirmation = false;
          }}>Buat Baru</Button
        >
        <Button
          color="yellow"
          onclick={() => {
            requireBarangConfirmation = false;
          }}>Batal</Button
        >
      </div>
    </div>
  </Modal>
  <Modal bind:open={requirePBFConfirmation} size="xs" autoclose outsideclose>
    <div class="text-center">
      <ExclamationCircleOutline
        class="mx-auto mb-4 text-yellow-400 w-12 h-12 dark:text-yellow-200"
      />
      <h3 class="mb-1 text-lg font-semibold text-gray-500 dark:text-gray-400">
        Peringatan
      </h3>
      <h4 class="mb-2 text-gray-500 dark:text-gray-400">
        Beberapa item PBF mirip {nama_pbf}. Silakan pilih item yang sesuai, atau
        buat data baru.
      </h4>
      <Table innerDivClass="left-0 my-2" hoverable={true}>
        <TableHead>
          <TableHeadCell>No</TableHeadCell>
          <TableHeadCell>Nama PBF</TableHeadCell>
          <TableHeadCell>
            <span class="sr-only">Aksi</span>
          </TableHeadCell>
        </TableHead>
        <TableBody tableBodyClass="divide-y">
          {#each items_pbf as item, index}
            <TableBodyRow>
              <TableBodyCell>{index + 1}</TableBodyCell>
              <TableBodyCell>{item.nama_pbf}</TableBodyCell>
              <TableBodyCell>
                <Button
                  color="blue"
                  pill={true}
                  class="p-2!"
                  onclick={() => {
                    selectedPBFId = item.id_pbf;
                    setStokObat();
                    requirePBFConfirmation = false;
                  }}
                >
                  Pilih
                </Button>
              </TableBodyCell>
            </TableBodyRow>
          {/each}
        </TableBody>
      </Table>
      <div class="flex space-x-4">
        <Button
          color="alternative"
          class="me-2"
          onclick={() => {
            isNewPBF = true;
            setStokObat();
            requirePBFConfirmation = false;
          }}>Buat Baru</Button
        >
        <Button
          color="yellow"
          onclick={() => {
            requirePBFConfirmation = false;
          }}>Batal</Button
        >
      </div>
    </div>
  </Modal>
  <Modal bind:open={deleteConfirmation} size="xs" autoclose outsideclose>
    <div class="text-center">
      <ExclamationCircleOutline
        class="mx-auto mb-4 text-gray-400 w-12 h-12 dark:text-gray-200"
      />

      <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
        Apakah anda yakin ingin menghapus Stok
        {selectedBarang}?
      </h3>
      <Button
        color="red"
        class="me-2"
        onclick={() => {
          deleteStok();
          deleteConfirmation = false;
          openRow = -1;
        }}>Ya</Button
      >
      <Button
        color="alternative"
        onclick={() => {
          deleteConfirmation = false;
        }}>Tidak</Button
      >
    </div>
  </Modal>
  <Modal bind:open={clickEditStokModal} autoclose={false} outsideclose={false}>
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
        Edit Data Stok Obat
      </h3>
      <hr />
      <Label class="space-y-2">
        <span class="text-gray-900">PBF</span>
        <Input
          bind:value={nama_pbf}
          data={suggestionsPBF}
          type="text"
          name="nama_pbf"
          placeholder="PT ABC"
          class="font-normal"
          bind:color={namaPBFCheck}
          onfocusout={() => {
            let normalizebrg = nama_barang.trimEnd();
            let normalizepbf = nama_pbf.trimEnd();
            nama_barang = normalizebrg;
            nama_pbf = normalizepbf;
            getExactItemByName(normalizebrg, normalizepbf);
          }}
          oninput={() => {
            checkInput("nama_pbf");
          }}
          required
        />
        {#if !validNamaPBF}
          <Helper class="mt-2" color="red">Nama PBF wajib diisi.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Nama Obat</span>
        <Input
          bind:value={nama_barang}
          data={suggestionsBarang}
          type="text"
          name="nama_barang"
          placeholder="Paracetamol"
          class="font-normal"
          bind:color={namaBarangCheck}
          onfocusout={() => {
            let normalizebrg = nama_barang.trimEnd();
            let normalizepbf = nama_pbf.trimEnd();
            nama_barang = normalizebrg;
            nama_pbf = normalizepbf;
            getExactItemByName(normalizebrg, normalizepbf);
          }}
          oninput={() => {
            checkInput("nama_barang");
          }}
          required
        />
        {#if !validNamaBarang}
          <Helper class="mt-2" color="red">Nama barang wajib diisi.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Nomor Batch</span>
        <Input
          bind:value={nomor_batch}
          type="text"
          name="nomor_batch"
          placeholder="ABCDEF12345"
          class="font-normal"
          bind:color={noBatchCheck}
          oninput={() => {
            checkInput("nomor_batch");
          }}
          required
        />
        {#if !validNoBatch}
          <Helper class="mt-2" color="red">Nomor batch wajib diisi.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Tanggal Expired</span>
        <Datepicker
          bind:value={tanggal_expired}
          bind:color={tglExpCheck}
          onselect={() => {
            checkInput("tgl_exp");
          }}          
          required
        />
        {#if !validTglExp}
          <Helper class="mt-2" color="red">Tanggal expired wajib diisi.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Jumlah Stok</span>
        <Input
          bind:value={jumlah_stok}
          type="number"
          name="jumlah_stok"
          placeholder="50"
          class="font-normal"
          bind:color={stokCheck}
          oninput={() => {
            checkInput("jumlah_stok");
          }}
          required
        />
        {#if !validStok}
          <Helper class="mt-2" color="red">Jumlah stok wajib diisi.</Helper>
        {:else if !validStok && jumlah_stok < 1}
          <Helper class="mt-2" color="red">Jumlah stok tidak valid.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Harga Beli</span>
        <Input
          bind:value={harga_beli_per_satuan}
          type="number"
          name="harga_beli_per_satuan"
          placeholder="10000"
          class="font-normal"
          bind:color={hargaBeliCheck}
          oninput={() => {
            checkInput("harga_beli");
          }}
          required
        />
        {#if !validHargaBeli && !harga_beli_per_satuan}
          <Helper class="mt-2" color="red">Harga beli wajib diisi.</Helper>
        {:else if !validHargaBeli && harga_beli_per_satuan < 1}
          <Helper class="mt-2" color="red">Harga beli tidak valid.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Harga Jual</span>
        <div class="flex space-x-1">
          <Input
            bind:value={harga_jual_per_satuan}
            type="number"
            name="harga_jual_per_satuan"
            placeholder="11000"
            class="font-normal flex-1"
            bind:color={hargaJualCheck}
            oninput={() => {
              checkInput("harga_jual");
            }}
            required
          />
          <div class="flex flex-none">
            <Checkbox
              bind:checked={isPercentage}
              onchange={() => {
                checkInput("harga_jual");
              }}>%</Checkbox
            >
          </div>
        </div>
        {#if !validHargaJual && !harga_jual_per_satuan}
          <Helper class="mt-2" color="red">Harga jual wajib diisi.</Helper>
        {:else if !validHargaJual && harga_jual_per_satuan < 1}
          <Helper class="mt-2" color="red">Harga jual tidak valid.</Helper>
        {:else if !validHargaJual && harga_beli_per_satuan > harga_jual_per_satuan && !isPercentage}
          <Helper class="mt-2" color="red"
            >Harga jual tidak boleh lebih kecil dari harga beli.</Helper
          >
        {:else if !validHargaJual && harga_jual_per_satuan > 100 && isPercentage}
          <Helper class="mt-2" color="red"
            >Harga jual (% dari harga beli) tidak valid.</Helper
          >
        {/if}
      </Label>
      <hr />
      <div class="flex flex-row justify-between space-x-4">
        <Button class="flex flex-1" type="submit" disabled={isBtnDisabled}
          >Ubah</Button
        >
        <Button
          class="flex flex-1"
          type="button"
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
  <Modal
    bind:open={clickCreateStokModal}
    autoclose={false}
    outsideclose={false}
  >
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
        <span class="text-gray-900">PBF</span>
        <Input
          bind:value={nama_pbf}
          data={suggestionsPBF}
          type="text"
          name="nama_pbf"
          placeholder="PT ABC"
          class="font-normal"
          bind:color={namaPBFCheck}
          onfocusout={() => {
            if (nama_pbf) {
              let normalizebrg = nama_barang.trimEnd();
              let normalizepbf = nama_pbf.trimEnd();
              nama_barang = normalizebrg;
              nama_pbf = normalizepbf;
              getExactItemByName(normalizebrg, normalizepbf);
            }
          }}
          oninput={() => {
            checkInput("nama_pbf");
          }}
          required
        />
        {#if !validNamaPBF}
          <Helper class="mt-2" color="red">Nama PBF wajib diisi.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Nama Obat</span>
        <Input
          bind:value={nama_barang}
          data={suggestionsBarang}
          type="text"
          name="nama_barang"
          placeholder="Paracetamol"
          class="font-normal"
          bind:color={namaBarangCheck}
          onfocusout={() => {
            if (nama_barang) {
              let normalizebrg = nama_barang.trimEnd();
              let normalizepbf = nama_pbf.trimEnd();
              nama_barang = normalizebrg;
              nama_pbf = normalizepbf;
              getExactItemByName(normalizebrg, normalizepbf);
            }
          }}
          oninput={() => {
            checkInput("nama_barang");
          }}
          required
        />
        {#if !validNamaBarang}
          <Helper class="mt-2" color="red">Nama barang wajib diisi.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Nomor Batch</span>
        <Input
          bind:value={nomor_batch}
          type="text"
          name="nomor_batch"
          placeholder="ABCDEF12345"
          class="font-normal"
          bind:color={noBatchCheck}
          oninput={() => {
            checkInput("nomor_batch");
          }}
          required
        />
        {#if !validNoBatch}
          <Helper class="mt-2" color="red">Nomor batch wajib diisi.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Tanggal Expired</span>
        <Datepicker
          bind:value={tanggal_expired}
          bind:color={tglExpCheck}
          oninput={() => {
            checkInput("tgl_exp");
          }}
          required
        />
        {#if !validTglExp}
          <Helper class="mt-2" color="red">Tanggal expired wajib diisi.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Jumlah Stok</span>
        <Input
          bind:value={jumlah_stok}
          type="number"
          name="jumlah_stok"
          placeholder="50"
          class="font-normal"
          bind:color={stokCheck}
          oninput={() => {
            checkInput("jumlah_stok");
          }}
          required
        />
        {#if !validStok}
          <Helper class="mt-2" color="red">Jumlah stok wajib diisi.</Helper>
        {:else if !validStok && jumlah_stok < 1}
          <Helper class="mt-2" color="red">Jumlah stok tidak valid.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Harga Beli</span>
        <Input
          bind:value={harga_beli_per_satuan}
          type="number"
          name="harga_beli_per_satuan"
          placeholder="10000"
          class="font-normal"
          bind:color={hargaBeliCheck}
          oninput={() => {
            checkInput("harga_beli");
          }}
          required
        />
        {#if !validHargaBeli && !harga_beli_per_satuan}
          <Helper class="mt-2" color="red">Harga beli wajib diisi.</Helper>
        {:else if !validHargaBeli && harga_beli_per_satuan < 1}
          <Helper class="mt-2" color="red">Harga beli tidak valid.</Helper>
        {/if}
      </Label>
      <Label class="space-y-2">
        <span class="text-gray-900">Harga Jual</span>
        <div class="flex space-x-1">
          <Input
            bind:value={harga_jual_per_satuan}
            type="number"
            name="harga_jual_per_satuan"
            placeholder="11000"
            class="font-normal flex-1"
            bind:color={hargaJualCheck}
            oninput={() => {
              checkInput("harga_jual");
            }}
            required
          />
          <div class="flex flex-none">
            <Checkbox
              bind:checked={isPercentage}
              onchange={() => {
                checkInput("harga_jual");
              }}>%</Checkbox
            >
          </div>
        </div>
        {#if !validHargaJual && !harga_jual_per_satuan}
          <Helper class="mt-2" color="red">Harga jual wajib diisi.</Helper>
        {:else if !validHargaJual && harga_jual_per_satuan < 1}
          <Helper class="mt-2" color="red">Harga jual tidak valid.</Helper>
        {:else if !validHargaJual && harga_beli_per_satuan > harga_jual_per_satuan && !isPercentage}
          <Helper class="mt-2" color="red"
            >Harga jual tidak boleh lebih kecil dari harga beli.</Helper
          >
        {:else if !validHargaJual && harga_jual_per_satuan > 100 && isPercentage}
          <Helper class="mt-2" color="red"
            >Harga jual (% dari harga beli) tidak valid.</Helper
          >
        {/if}
      </Label>
      <hr />
      <div class="flex flex-row justify-between space-x-4">
        <Button class="flex flex-1" type="submit" disabled={isBtnDisabled}
          >Simpan</Button
        >
        <Button
          class="flex flex-1"
          type="button"
          color="alternative"
          onclick={() => {
            clickCreateStokModal = false;
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
        <DropdownHeader>
          <span class="font-bold">Pengaturan</span>
        </DropdownHeader>
        <DropdownGroup class="text-left">
          <DropdownItem
            class="hover:cursor-pointer w-full text-left"
            onclick={() => {
              resetConfirmation = true;
              resetPBFAlert = true;
            }}
          >
            Reset PBF
          </DropdownItem>
          <DropdownItem
            class="hover:cursor-pointer w-full text-left"
            onclick={() => {
              resetConfirmation = true;
              resetBarangAlert = true;
            }}
          >
            Reset Data Obat
          </DropdownItem>
          <DropdownItem
            class="hover:cursor-pointer w-full text-left"
            onclick={() => {
              resetConfirmation = true;
              resetStokAlert = true;
            }}
          >
            Reset Stok Obat
          </DropdownItem>
        </DropdownGroup>
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
          <DropdownHeader>
            <span class="text-center py-2 font-bold">Notifikasi</span>
          </DropdownHeader>
          {#if expw_items_stok.length == 0}
            <DropdownGroup>
              <DropdownItem>Tidak ada notifikasi</DropdownItem>
            </DropdownGroup>
          {:else}
            <DropdownGroup>
              {#each expw_items_stok
                .filter((item) => item.tanggal_expired)
                .slice(0, 3) as item}
                <DropdownItem
                  onclick={() => {
                    isStokObat = false;
                    isTglExp = true;
                    isStockAlert = false;
                    searchStockAlert = item.nama_barang;
                  }}
                  class="hover:cursor-pointer text-left"
                >
                  <div class="font-bold">{item.nama_barang}</div>
                  <div class="text-sm mb-1.5">
                    Tgl Expired: <div
                      class="text-sm font-bold text-primary-600"
                    >
                      {formatTanggal(item.tanggal_expired)}
                    </div>
                  </div>
                </DropdownItem>
              {/each}
              <DropdownDivider />
              <DropdownItem
                onclick={() => {
                  isPBF = false;
                  isDataObat = false;
                  isStokObat = false;
                  isTglExp = true;
                  isStockAlert = false;
                }}
                class="text-center w-full h-full hover:cursor-pointer"
                >Lihat semua</DropdownItem
              >
            </DropdownGroup>
          {/if}
        </Dropdown>
      </div>
    </div>
  </div>
  <Tabs tabStyle="underline">
    <TabItem
      open={isStokObat}
      title="Stok Obat"
      on:click={() => {
        isStokObat = true;
        isTglExp = false;
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

        <Table innerDivClass="left-0 my-2" hoverable={true} color="custom">
          <TableHead>
            <TableHeadCell>No</TableHeadCell>
            <TableHeadCell>Nama Obat</TableHeadCell>
            <TableHeadCell>PBF</TableHeadCell>
            <TableHeadCell>Total Stok</TableHeadCell>
            <TableHeadCell>
              <span class="sr-only">Aksi</span>
            </TableHeadCell>
          </TableHead>
          <TableBody tableBodyClass="divide-y">
            {#each items_stok.filter((item) => item.nama_barang
                .toLowerCase()
                .includes(searchTermStok.toLowerCase())) as item, index}
              <TableBodyRow
                onclick={() => {
                  toggleRow(index);
                  getStokDetail(item.id_barang, item.id_pbf);
                  isExtended = !isExtended;
                }}
              >
                <TableBodyCell>{index + 1}</TableBodyCell>
                <TableBodyCell>{item.nama_barang}</TableBodyCell>
                <TableBodyCell>{item.nama_pbf}</TableBodyCell>
                <TableBodyCell>{item.total_stok}</TableBodyCell>
                <TableBodyCell>
                  {#if isExtended && openRow === index}
                    <AngleUpOutline />
                  {:else}
                    <AngleDownOutline />
                  {/if}
                </TableBodyCell>
              </TableBodyRow>
              {#if openRow === index}
                <TableBodyRow>
                  <TableBodyCell colspan={5} class="p-0">
                    <div
                      class="px-2 py-3"
                      transition:slide={{ duration: 150, axis: "y" }}
                    >
                      <Table hoverable={true} color="blue">
                        <TableHead>
                          <TableHeadCell>No</TableHeadCell>
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
                          {#each items_stok_detail as item_detail, i}
                            <TableBodyRow>
                              <TableBodyCell>{i + 1}</TableBodyCell>
                              <TableBodyCell
                                >{item_detail.no_batch}</TableBodyCell
                              >
                              <TableBodyCell
                                >Rp. {item_detail.harga_beli_per_satuan.toLocaleString(
                                  "id-ID"
                                )}</TableBodyCell
                              >
                              <TableBodyCell
                                >Rp. {item_detail.harga_jual_per_satuan.toLocaleString(
                                  "id-ID"
                                )}</TableBodyCell
                              >
                              <TableBodyCell
                                >{item_detail.tanggal_expired}</TableBodyCell
                              >
                              <TableBodyCell
                                >{item_detail.jumlah_stok}</TableBodyCell
                              >
                              <TableBodyCell>
                                <div class="flex space-x-4">
                                  <Button
                                    color="primary"
                                    pill={true}
                                    class="p-2!"
                                    onclick={() => {
                                      selectedStokId = item_detail.id_stok;
                                      selectedBarangId = item.id_barang;
                                      nama_barang = item.nama_barang;
                                      selectedPBFId = item.id_pbf;
                                      nama_pbf = item.nama_pbf;
                                      nomor_batch = item_detail.no_batch;
                                      harga_beli_per_satuan =
                                        item_detail.harga_beli_per_satuan;
                                      harga_jual_per_satuan =
                                        item_detail.harga_jual_per_satuan;
                                      tanggal_expired = new Date(
                                        item_detail.tanggal_expired
                                      );
                                      jumlah_stok = item_detail.jumlah_stok;
                                      clickEditStokModal = true;
                                    }}
                                  >
                                    <PenSolid class="w-6 h-6" />
                                  </Button>
                                  <Button
                                    color="red"
                                    pill={true}
                                    class="p-2!"
                                    onclick={() => {
                                      selectedStokId = item_detail.id_stok;
                                      selectedBarang = item.nama_barang;
                                      deleteConfirmation = true;
                                    }}><TrashBinSolid class="w-6 h-6" /></Button
                                  >
                                </div>
                              </TableBodyCell>
                            </TableBodyRow>
                          {/each}
                        </TableBody>
                      </Table>
                    </div>
                  </TableBodyCell>
                </TableBodyRow>
              {/if}
            {/each}
          </TableBody>
        </Table>
      {/if}
    </TabItem>
    <TabItem
      open={isTglExp}
      title="Tanggal Expired"
      on:click={() => {
        isStokObat = false;
        isTglExp = true;
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
        <Input
          type="text"
          class="mb-4 border border-gray-400 p-2 rounded w-full flex-1"
          placeholder="Cari data stok..."
          bind:value={searchStockAlert}
          clearable
        />

        <Table innerDivClass="left-0 my-2" hoverable={true} color="orange">
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
                  >Rp. {item.harga_beli_per_satuan.toLocaleString(
                    "id-ID"
                  )}</TableBodyCell
                >
                <TableBodyCell
                  >Rp. {item.harga_jual_per_satuan.toLocaleString(
                    "id-ID"
                  )}</TableBodyCell
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
    background-color: var(--color-gray-100);
  }
</style>
