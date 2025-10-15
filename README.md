# PharmAssist

PharmAssist merupakan aplikasi desktop yang dapat mencatat serta mengelola stok obat di apotek dengan fitur pengingat tanggal kedaluwarsa sehingga para apoteker dapat mengelola stok obat yang tersedia dengan mudah. Aplikasi ini dirancang menggunakan Tauri, Svelte, dan SQLite sebagai *database* lokal.

![halaman stok_obat](https://github.com/knyghtw/PharmAssist/blob/main/media/stokobat.png)

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
1. Buka aplikasi `PharmAssist`.
2. Tambahkan data PBF dan barang sebelum mencatat stok.
3. Klik pada *tab* stok obat untuk memasukkan stok obat baru.

## Informasi untuk Pengembang
### Rekomendasi Setup IDE
[VS Code](https://code.visualstudio.com/) + [Svelte](https://marketplace.visualstudio.com/items?itemName=svelte.svelte-vscode) + [Tauri](https://marketplace.visualstudio.com/items?itemName=tauri-apps.tauri-vscode) + [rust-analyzer](https://marketplace.visualstudio.com/items?itemName=rust-lang.rust-analyzer).
