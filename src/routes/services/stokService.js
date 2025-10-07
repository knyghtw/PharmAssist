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

      const normNamaBarang = nama_barang
        ? nama_barang.trim().toUpperCase()
        : "";
      console.log("normNamaBarang: " + normNamaBarang);
      const normNamaPBF = nama_pbf ? nama_pbf.trim().toUpperCase() : "";
      console.log("normNamaPBF: " + normNamaPBF);

      if ((!id_barang || id_barang.length === 0) && normNamaBarang.length > 0) {
        console.log("id_barang is null");
        const exactBarang = await db.select(
          "SELECT * FROM barang WHERE UPPER(nama_barang) = ? LIMIT 1",
          [normNamaBarang]
        );
        if (exactBarang) {
          id_barang = exactBarang.id_barang;
        } else {
          const candidatesBarang = await db.select(
            "SELECT id_barang, nama_barang FROM barang WHERE UPPER(nama_barang) LIKE ? LIMIT 5",
            [normNamaBarang + "%"]
          );

          if (candidatesBarang.length === 0) {
            const created = await barangService.createItem(normNamaBarang);
            if (created && created.data.lastInsertId) {
              const row = await db.get(
                "SELECT id_barang FROM barang WHERE nama_barang = ?",
                [normNamaBarang]
              );
              if (row) {
                id_barang = row.id_barang;
              } else {
                console.log("Error: row barang is null");
                return {
                  success: false,
                  message: "Unexpected error row is null",
                  data: candidatesBarang,
                };
              }
              // id_barang = row ? row.id_barang : null;
            } else {
              console.log("Error adding new barang");
              return {
                success: false,
                message: "Unexpected error adding new barang",
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

      if (
        (!id_pbf || id_pbf.toString().length === 0) &&
        normNamaPBF.length > 0
      ) {
        console.log("id_pbf is null");
        const exactPBF = await db.select(
          "SELECT id_pbf, nama_pbf FROM pbf WHERE UPPER(nama_pbf) = ? LIMIT 1",
          [normNamaPBF]
        );
        if (exactPBF) {
          id_pbf = exactPBF.id_pbf;
        } else {
          const candidatesPBF = await db.all(
            "SELECT id_pbf, nama_pbf FROM pbf WHERE UPPER(nama_pbf) LIKE ? LIMIT 5",
            [normNamaPBF + "%"]
          );

          if (candidatesPBF.length === 0) {
            const createdPBF = await pbfService.createItem(normNamaPBF);
            if (createdPBF && createdPBF.id) {
              id_pbf = createdPBF.id;
            } else {
              const row = await db.get(
                "SELECT id_pbf FROM pbf WHERE UPPER(nama_pbf) = ? LIMIT 1",
                [normNamaPBF]
              );
              id_pbf = row ? row.id_pbf : null;
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
      return { success: false, message: error.message || "Terjadi kesalahan" };
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
