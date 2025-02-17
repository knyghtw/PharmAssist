import Database from "@tauri-apps/plugin-sql";

const DB_PATH = "sqlite:test.db";

export default class stokService {
  static async getDB() {
    return await Database.load(DB_PATH);
  }

  static async getItems() {
    try {
      const db = await this.getDB();
      const result = await db.select(
        "SELECT s.*, b.nama_barang, p.nama_pbf FROM stok_obat s JOIN barang b ON s.id_barang = b.id_barang JOIN pbf p ON s.id_pbf = p.id_pbf"
      );
      return result;
    } catch (error) {
      console.error("Error fetching items:", error);
      throw error;
    }
  }

  static async createItem(
    id_barang,
    id_pbf,
    no_batch,
    harga_beli_per_satuan,
    harga_jual_per_satuan,
    tanggal_expired,
    jumlah_stok
  ) {
    try {
      const db = await this.getDB();

      const existingData = await db.select(
        "SELECT id_barang, no_batch FROM stok_obat WHERE id_barang = $1 AND no_batch = $2",
        [id_barang, no_batch]
      );

      if (existingData.length > 0) {
        throw new Error(
          `Stok obat dengan id_barang "${id_barang}" dan no_batch "${no_batch}" sudah ada`
        );
      }

      const result = await db.execute(
        "INSERT INTO stok_obat (id_barang, id_pbf, no_batch, harga_beli_per_satuan, harga_jual_per_satuan, tanggal_expired, jumlah_stok) VALUES ($1, $2, $3, $4, $5, $6, $7)",
        [
          id_barang,
          id_pbf,
          no_batch,
          harga_beli_per_satuan,
          harga_jual_per_satuan,
          tanggal_expired,
          jumlah_stok,
        ]
      );

      return {
        success: true,
        message: "Data stok obat berhasil ditambahkan",
        data: result,
      };
    } catch (error) {
      console.error("Error adding item:", error);
      throw error;
    }
  }

  static async deleteItem(id_stok) {
    try {
      const db = await this.getDB();
      await db.execute("DELETE FROM stok_obat WHERE id_stok = $1", [id_stok]);
      return {
        success: true,
        message: "Data stok obat berhasil dihapus",
      };
    } catch (error) {
      console.error("Error:", error);
      throw error;
    }
  }

  static async resetStok() {
    try {
      const db = await this.getDB();
      await db.execute("DELETE FROM stok_obat");
      return {
        success: true,
        message: "Data stok obat berhasil dihapus",
      };
    } catch (error) {
      console.error("Error:", error);
      throw error;
    }
  }
}
