package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LicenseTable : IdTable<String>("licenses") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val licenseKey = text("license_key").uniqueIndex("licenses_license_key_key")
    val idempotencyKey = text("idempotency_key").nullable().uniqueIndex("licenses_idempotency_key_key")
    val version = integer("version").nullable()
    val type = pgEnum("type", "\"LicenseType\"", LicenseType.entries, LicenseType::dbName)
    val seats = integer("seats").nullable()
    val issuedAt = pgTimestamp("issued_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    val licenseeEmail = text("licensee_email").nullable()
    val licenseeName = text("licensee_name").nullable()
    val whiteLabelAddon = bool("white_label_addon").default(false)
    val status = pgEnum("status", "\"LicenseStatus\"", LicenseStatus.entries, LicenseStatus::dbName).default(LicenseStatus.ACTIVE)
    override val primaryKey = PrimaryKey(id, name = "licenses_pkey")
}
