# PharmAssist

PharmAssist merupakan aplikasi desktop yang dapat mencatat serta mengelola stok obat di apotek dengan fitur pengingat tanggal kedaluwarsa sehingga para apoteker dapat mengelola stok obat yang tersedia dengan mudah. Aplikasi ini dirancang menggunakan Tauri, Svelte, dan SQLite sebagai *database* lokal.

![halaman stok_obat](https://github.com/knyghtw/PharmAssist/blob/main/media/stokobat.webp)

## Fitur Aplikasi
- Manajemen Data Stok Obat
  - Pencatatan stok per batch beserta tanggal kedaluwarsa
  - Fitur *Autosuggestions* dan *Autofill* membantu mempercepat proses pencatatan stok
- Notifikasi Pengingat Obat yang Mendekati Tanggal Kedaluwarsa
  - *Pop-up* notifikasi pada saat aplikasi dibuka jika ada obat yang mendekati tanggal kedaluwarsa
  - Tombol "Lonceng" untuk melihat riwayat notifikasi
- Reset *Database*
  - Fitur reset data untuk mengembalikan database ke kondisi awal 
- Ramah Performa dan Tempat Penyimpanan
  - Data disimpan secara lokal menggunakan SQLite untuk akses data yang cepat
  - Desain ringan agar dapat berjalan lancar pada mesin dengan spesifikasi rendah

## Cara Instalasi
*File installer* dapat diunduh pada halaman [Releases](https://github.com/knyghtw/PharmAssist/releases)

## Cara Penggunaan
### Bagian I: Instalasi
1. Unduh *file installer* pada halaman [Releases](https://github.com/knyghtw/PharmAssist/releases)
2. Buka *file installer* untuk melakukan instalasi aplikasi.

### Bagian II: Mencatat Data Stok Obat Baru
1. Buka aplikasi `PharmAssist`.
2. Klik tombol tambah data.
3. Isi semua kolom yang tersedia. Centang kotak persen di sebelah kolom Harga Jual jika ingin menghitung harga jual berdasarkan persentase dari harga beli.
4. Klik simpan

### Bagian III: Merubah Data Stok Obat
1. Klik salah satu item pada tabel stok obat.
2. Tentukan batch mana yang ingin dirubah, lalu klik tombol edit di sebelah kanan.
3. Ubah data yang terdapat pada form.
4. Klik ubah.

### Bagian IV: Merubah Data Stok Obat
1. Klik salah satu item pada tabel stok obat.
2. Tentukan batch mana yang ingin dirubah, lalu klik tombol edit di sebelah kanan.
3. Klik hapus.

## Informasi untuk Pengembang
### Rekomendasi Setup IDE
[VS Code](https://code.visualstudio.com/) + [Svelte](https://marketplace.visualstudio.com/items?itemName=svelte.svelte-vscode) + [Tauri](https://marketplace.visualstudio.com/items?itemName=tauri-apps.tauri-vscode) + [rust-analyzer](https://marketplace.visualstudio.com/items?itemName=rust-lang.rust-analyzer).
