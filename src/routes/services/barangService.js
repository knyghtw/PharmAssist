import Database from "@tauri-apps/plugin-sql";

const DB_PATH = "sqlite:test.db";

export default class barangService {
  static async getDB() {
    return await Database.load(DB_PATH);
  }

  static async getItem(id_barang) {
    try {
      const db = await this.getDB();
      const result = await db.select(
        "SELECT * FROM barang WHERE id_barang = $1",
        [id_barang]
      );
      if (result.length === 0) {
        throw new Error(`Barang dengan ID "${id_barang}" tidak ditemukan`);
      }
      return result[0];
    } catch (error) {
      console.error(error);
      throw error;
    }
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

  static async createItem(nama_barang) {
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
        "INSERT INTO barang (nama_barang) VALUES ($1)",
        [nama_barang]
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

  static async updateItem(id_barang, nama_barang) {
    try {
      const db = await this.getDB();
      const dataCheck = await db.select(
        "SELECT id_barang FROM barang WHERE id_barang = $1",
        [id_barang]
      );
      if ((dataCheck.length = 0)) {
        throw new Error(`Barang tidak ada dalam sistem`);
      }
      const result = await db.execute(
        "UPDATE barang SET nama_barang = $1 WHERE id_barang = $2",
        [nama_barang, id_barang]
      );
      return {
        success: true,
        message: "Data barang berhasil diubah",
        data: result,
      };
    } catch (error) {
      console.error(error);
      throw error;
    }
  }

  static async deleteItem(id_barang) {
    try {
      const db = await this.getDB();
      const dataCheck = await db.select(
        "SELECT id_barang FROM barang WHERE id_barang = $1",
        [id_barang]
      );
      if ((dataCheck.length = 0)) {
        throw new Error(`Barang tidak ada dalam sistem`);
      }
      const result = await db.execute(
        "DELETE FROM barang WHERE id_barang = $1",
        [id_barang]
      );
      return {
        success: true,
        message: "Data barang berhasil dihapus",
      };
    } catch (error) {
      console.error("Error:", error);
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
