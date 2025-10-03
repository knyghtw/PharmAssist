import Database from "@tauri-apps/plugin-sql";
import barangService from "./barangService";
import pbfService from "./pbfService";

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

  static async getExpiryWarnItems() {
    try {
      const db = await this.getDB();
      const result = await db.select(
        "SELECT s.*, b.nama_barang, p.nama_pbf FROM stok_obat s JOIN barang b ON s.id_barang = b.id_barang JOIN pbf p ON s.id_pbf = p.id_pbf WHERE s.tanggal_expired <= date('now', '+1 month')"
      );
      return result;
    } catch (error) {
      console.error("Error fetching items:", error);
      throw error;
    }
  }
// TODO:
// Fix createItem
  static async createItem(
    id_barang,
    id_pbf,
    nama_barang,
    nama_pbf,
    no_batch,
    harga_beli_per_satuan,
    harga_jual_per_satuan,
    tanggal_expired,
    jumlah_stok
  ) {
    try {
      const db = await this.getDB();

      // const existingData = await db.select(
      //   "SELECT id_barang, no_batch FROM stok_obat WHERE id_barang = $1 AND no_batch = $2",
      //   [id_barang, no_batch]
      // );

      // if (existingData.length > 0) {
      //   throw new Error(
      //     `Stok obat dengan id_barang "${id_barang}" dan no_batch "${no_batch}" sudah ada`
      //   );
      // }

      if (id_barang.length == 0 || id_barang == null) {
        // TODO: Search keyword from nama_barang in barang, get the id
        // If not found, create new and get the id
      }

      if (id_pbf.length == 0 || id_pbf == null) {
        // TODO: Search keyword from nama_pbf in pbf, get the id
        // If not found, create new and get the id
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

  static async updateItem(
    id_stok,
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
        "SELECT id_stok FROM stok_obat WHERE id_stok = $1",
        [id_stok]
      );
      if (existingData.length === 0) {
        throw new Error(`Stok obat dengan ID "${id_stok}" tidak ditemukan`);
      }
      const result = await db.execute(
        "UPDATE stok_obat SET id_barang = $2, id_pbf = $3, no_batch = $4, harga_beli_per_satuan = $5, harga_jual_per_satuan = $6, tanggal_expired = $7, jumlah_stok = $8 WHERE id_stok = $1",
        [
          id_stok,
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
        message: "Data stok obat berhasil diubah",
        data: result,
      };
    } catch (error) {
      console.error(error);
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
