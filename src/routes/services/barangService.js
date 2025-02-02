import Database from "@tauri-apps/plugin-sql";

const DB_PATH = "sqlite:test.db";

export default class barangService {
  static async getDB() {
    return await Database.load(DB_PATH);
  }

  static async getItems() {
    try {
      const db = await this.getDB();
      const result = await db.select("SELECT * FROM barang");
      return result;
    } catch (error) {
      console.error("Error fetching items:", error);
      throw error;
    }
  }

  static async createItem(nama_barang, satuan) {
    try {
      const db = await Database.load("sqlite:test.db");
      const existingData = await db.select(
        "SELECT nama_barang FROM barang WHERE nama_barang = $1",
        [nama_barang]
      );

      if (existingData.length > 0) {
        throw new Error(`PBF dengan nama "${nama_barang}" sudah ada`);
      }

      const result = await db.execute(
        "INSERT INTO barang (nama_barang, satuan) VALUES ($1, $2)",
        [nama_barang, satuan]
      );
      return {
        success: true,
        message: "Data barang berhasil ditambahkan",
        data: result,
      };
    } catch (error) {
      console.error(error);
      throw error;
    }
  }

  static async resetBarang() {
    try {
      const db = await this.getDB();
      await db.execute("DELETE FROM barang");
      return {
        success: true,
        message: "Data barang berhasil dihapus",
      };
    } catch (error) {
      console.error("Error:", error);
      throw error;
    }
  }
}
