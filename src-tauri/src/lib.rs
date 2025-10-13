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
                    nama_barang VARCHAR UNIQUE NOT NULL                    
                );
                CREATE TABLE IF NOT EXISTS pbf (
                    id_pbf INTEGER PRIMARY KEY,
                    nama_pbf VARCHAR UNIQUE NOT NULL
                );
                CREATE TABLE IF NOT EXISTS stok_obat (
                    id_stok INTEGER PRIMARY KEY,
                    id_barang INTEGER NOT NULL,
                    id_pbf INTEGER NOT NULL,
                    no_batch VARCHAR NOT NULL,
                    harga_beli_per_satuan DECIMAL NOT NULL,
                    harga_jual_per_satuan DECIMAL NOT NULL,
                    tanggal_expired DATE NOT NULL,
                    jumlah_stok INTEGER NOT NULL,
                    tanggal_input DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
                    FOREIGN KEY (id_barang) REFERENCES barang(id_barang),
                    FOREIGN KEY (id_pbf) REFERENCES pbf(id_pbf),
                    CONSTRAINT unique_stok UNIQUE (id_barang, id_pbf, no_batch)
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
