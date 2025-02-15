// Learn more about Tauri commands at https://tauri.app/develop/calling-rust/
use tauri_plugin_sql::{Migration, MigrationKind};

#[tauri::command]
fn greet(name: &str) -> String {
    format!("Hello, {}! You've been greeted from Rust!", name)
}

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    let migrations = vec![Migration {
        version: 1,
        description: "create_new_tables",
        sql: "CREATE TABLE IF NOT EXISTS barang (
                    id_barang INTEGER PRIMARY KEY,
                    nama_barang VARCHAR UNIQUE,
                    satuan VARCHAR
                );
                CREATE TABLE IF NOT EXISTS pbf (
                    id_pbf INTEGER PRIMARY KEY,
                    nama_pbf VARCHAR UNIQUE
                );
                CREATE TABLE IF NOT EXISTS stok_obat (
                    id_stok INTEGER PRIMARY KEY,
                    id_barang INTEGER UNIQUE,
                    id_pbf INTEGER UNIQUE,
                    no_batch VARCHAR UNIQUE,
                    harga_beli_per_satuan DECIMAL,
                    harga_jual_per_satuan DECIMAL,
                    tanggal_expired DATE,
                    jumlah_stok INTEGER,
                    tanggal DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (id_barang) REFERENCES barang(id_barang),
                    FOREIGN KEY (id_pbf) REFERENCES pbf(id_pbf)
                );",
        kind: MigrationKind::Up,
    }];

    tauri::Builder::default()
        .plugin(
            tauri_plugin_sql::Builder::default()
                .add_migrations("sqlite:test.db", migrations)
                .build(),
        )
        .plugin(tauri_plugin_notification::init())
        .plugin(tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![greet])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
