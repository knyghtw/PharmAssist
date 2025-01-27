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
        description: "create_initial_tables",
        sql: "CREATE TABLE IF NOT EXISTS barang (
                id_barang INT PRIMARY KEY,
                nama_obat VARCHAR,
                satuan VARCHAR,
                total_stok INT
            );
            CREATE TABLE IF NOT EXISTS pbf (
                id_pbf INT PRIMARY KEY,
                nama_pbf VARCHAR
            );
            CREATE TABLE IF NOT EXISTS stok_obat (
                id_stok INT PRIMARY KEY,
                id_barang INT,
                id_pbf INT,
                no_batch VARCHAR,
                harga_beli_per_satuan DECIMAL,
                harga_jual_per_satuan DECIMAL,
                tanggal_expired DATE,
                jumlah_stok INT,
                FOREIGN KEY (id_barang) REFERENCES barang(id_barang),
                FOREIGN KEY (id_pbf) REFERENCES pbf(id_pbf)
            );",
            kind: MigrationKind::Up,
        },
        Migration {
            version: 2,
            description: "add_unique_constraints",
            sql: "CREATE UNIQUE INDEX idx_nama_obat ON barang(nama_obat);
                 CREATE UNIQUE INDEX idx_nama_pbf ON pbf(nama_pbf);
                 CREATE UNIQUE INDEX idx_batch_barang_pbf ON stok_obat(no_batch, id_barang, id_pbf);",
            kind: MigrationKind::Up,
        },        
    ];

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
