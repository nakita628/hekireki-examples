package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationLicenseTable : IdTable<String>("\"OrganizationLicense\"") {
    override val id = text("organizationId").entityId()
    val licenseKey = text("licenseKey").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val validatedAt = pgTimestamp("validatedAt", 3).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationLicense_pkey\"")
}
