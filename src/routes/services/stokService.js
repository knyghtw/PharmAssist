import Database from "@tauri-apps/plugin-sql";
import barangService from "./barangService";
import pbfService from "./pbfService";
import { data } from "autoprefixer";

const DB_PATH = "sqlite:test.db";

export default class stokService {
  static async getDB() {
    return await Database.load(DB_PATH);
  }

  static async getItems() {
    try {
      const db = await this.getDB();
      const result = await db.select(
        "SELECT b.id_barang, b.nama_barang, p.id_pbf, p.nama_pbf, SUM(s.jumlah_stok) AS total_stok FROM stok_obat s JOIN barang b ON s.id_barang = b.id_barang JOIN pbf p ON s.id_pbf = p.id_pbf GROUP BY b.id_barang, p.id_pbf, b.nama_barang, p.nama_pbf ORDER BY b.nama_barang"
      );
      return result;
    } catch (error) {
      console.error("Error fetching items:", error);
      throw error;
    }
  }

  static async getExactItem() {
    try {
      const db = await this.getDB();
      const result = await db.select(
        "SELECT b.id_barang, b.nama_barang, p.id_pbf, p.nama_pbf, s.harga_beli_per_satuan, s.harga_jual_per_satuan FROM stok_obat s JOIN barang b ON s.id_barang = b.id_barang JOIN pbf p ON s.id_pbf = p.id_pbf WHERE b.id_barang = 6 AND p.id_pbf = 3 GROUP BY b.id_barang, p.id_pbf, b.nama_barang, p.nama_pbf LIMIT 1"
      );
      return result;
    } catch (error) {
      console.error("Error fetching items:", error);
      throw error;
    }
  }

  static async getDetails(id_barang, id_pbf) {
    try {
      const db = await this.getDB();
      const result = await db.select(
        "SELECT id_stok, no_batch, harga_beli_per_satuan, harga_jual_per_satuan, tanggal_expired, jumlah_stok FROM stok_obat WHERE id_barang = ? AND id_pbf = ? ORDER BY tanggal_expired ASC, id_stok ASC",
        [id_barang, id_pbf]
      );      
      return result;
    } catch (error) {
      console.error("Error fetching item details:", error);
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

  static async createItem(
    id_barang,
    id_pbf,
    nama_barang,
    nama_pbf,
    no_batch,
    harga_beli_per_satuan,
    harga_jual_per_satuan,
    tanggal_expired,
    jumlah_stok,
    is_new_barang,
    is_new_pbf
  ) {
    try {
      const db = await this.getDB();

      const normNamaBarang = nama_barang ? nama_barang.toUpperCase() : "";
      const normNamaPBF = nama_pbf ? nama_pbf.toUpperCase() : "";
      let currentIdBarang = id_barang;
      let currentIdPBF = id_pbf;

      // Barang candidate check
      if (
        (!currentIdBarang || currentIdBarang.length === 0) &&
        normNamaBarang.length > 0
      ) {
        const exactBarang = await db.select(
          "SELECT * FROM barang WHERE nama_barang = ? LIMIT 1",
          [normNamaBarang]
        );
        if (exactBarang.length === 1) {
          currentIdBarang = exactBarang[0].id_barang;
        } else {
          const candidatesBarang = await db.select(
            "SELECT * FROM barang WHERE nama_barang LIKE ? LIMIT 5",
            [normNamaBarang + "%"]
          );

          if (
            candidatesBarang.length === 0 ||
            (candidatesBarang.length > 1 && is_new_barang)
          ) {
            const createdBarang = await db.execute(
              "INSERT INTO barang (nama_barang) VALUES ($1)",
              [normNamaBarang]
            );

            const rowBarang = await db.select(
              "SELECT id_barang FROM barang WHERE nama_barang = $1 LIMIT 1",
              [normNamaBarang]
            );
            if (rowBarang && rowBarang[0].id_barang) {
              currentIdBarang = rowBarang[0].id_barang;
            } else {
              return {
                success: false,
                message: "Unexpected error rowBarang is null",
                data: candidatesBarang,
              };
            }
          } else {
            return {
              success: false,
              message: "confirm_barang",
              data: candidatesBarang,
            };
          }
        }
      }

      // PBF Candidate Check
      if (
        (!currentIdPBF || currentIdPBF.toString().length === 0) &&
        normNamaPBF.length > 0
      ) {
        const exactPBF = await db.select(
          "SELECT id_pbf, nama_pbf FROM pbf WHERE UPPER(nama_pbf) = ? LIMIT 1",
          [normNamaPBF]
        );
        if (exactPBF.length === 1) {
          currentIdPBF = exactPBF[0].id_pbf;
        } else {
          const candidatesPBF = await db.select(
            "SELECT id_pbf, nama_pbf FROM pbf WHERE nama_pbf LIKE ? LIMIT 5",
            [normNamaPBF + "%"]
          );

          if (
            candidatesPBF.length === 0 ||
            (candidatesPBF.length > 1 && is_new_pbf)
          ) {
            const createdPBF = await db.execute(
              "INSERT INTO pbf (nama_pbf) VALUES ($1)",
              [normNamaPBF]
            );
            const rowPBF = await db.select(
              "SELECT id_pbf FROM pbf WHERE nama_pbf LIKE $1 LIMIT 1",
              [normNamaPBF]
            );
            if (rowPBF && rowPBF[0].id_pbf) {
              currentIdPBF = rowPBF[0].id_pbf;
            } else {
              return {
                success: false,
                message: "Unexpected error rowPBF is null",
                data: candidatesPBF,
              };
            }
          } else {
            return {
              success: false,
              message: "confirm_pbf",
              data: candidatesPBF,
            };
          }
        }
      }

      const result = await db.execute(
        "INSERT INTO stok_obat (id_barang, id_pbf, no_batch, harga_beli_per_satuan, harga_jual_per_satuan, tanggal_expired, jumlah_stok) VALUES (?, ?, ?, ?, ?, ?, ?)",
        [
          currentIdBarang,
          currentIdPBF,
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
      return { success: false, message: error };
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
