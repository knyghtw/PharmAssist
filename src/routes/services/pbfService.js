import Database from "@tauri-apps/plugin-sql";

const DB_PATH = "sqlite:test.db";

export default class pbfService {
  static async getDB() {
    return await Database.load(DB_PATH);
  }

  static async getItems() {
    try {
      const db = await this.getDB();
      const result = await db.select("SELECT * FROM pbf");
      return result;
    } catch (error) {
      console.error("Error fetching items:", error);
      throw error;
    }
  }

  static async createItem(nama_pbf) {
    try {
      const db = await this.getDB();

      const existingPBF = await db.select(
        "SELECT * FROM pbf WHERE UPPER(nama_pbf) = $1",
        [nama_pbf.toUpperCase()]
      );

      if (existingPBF.length > 0) {
        throw new Error(`PBF dengan nama ${nama_pbf} sudah ada`);
      }

      const result = await db.execute(
        "INSERT INTO pbf (nama_pbf) VALUES ($1)",
        [nama_pbf.toUpperCase()]
      );

      return {
        success: true,
        message: "Data PBF berhasil ditambahkan",
        data: result,
      };
    } catch (error) {
      console.error("Error adding item:", error);
      throw error;
    }
  }

  static async updatePBF(nama_pbf, id_pbf) {
    try {
      const db = await this.getDB();
      await db.execute("UPDATE pbf SET nama_pbf = $1 WHERE id_pbf = $2", [
        nama_pbf.toUpperCase(),
        id_pbf,
      ]);

      return {
        success: true,
        message: "Data PBF berhasil diubah",
      };
    } catch (error) {
      console.error("Error updating item:", error);
      throw error;
    }
  }

  static async deleteItem(nama_pbf) {
    try {
      const db = await this.getDB();
      await db.execute("DELETE FROM pbf WHERE nama_pbf = $1", [nama_pbf]);
      return {
        success: true,
        message: "Data PBF berhasil dihapus",
      };
    } catch (error) {
      console.error("Error deleting item:", error);
      throw error;
    }
  }

  static async resetPBF() {
    try {
      const db = await this.getDB();
      await db.execute("DELETE FROM pbf");
      return {
        success: true,
        message: "Data PBF berhasil dihapus",
      };
    } catch (error) {
      console.error("Error:", error);
      throw error;
    }
  }
  
}
