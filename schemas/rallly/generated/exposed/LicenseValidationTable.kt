package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LicenseValidationTable : IdTable<String>("license_validations") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val licenseId =
        reference(
            "license_id",
            LicenseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "license_validations_license_id_fkey",
        )
    val ipAddress = text("ip_address").nullable()
    val fingerprint = text("fingerprint").nullable()
    val validatedAt = pgTimestamp("validated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userAgent = text("user_agent").nullable()
    override val primaryKey = PrimaryKey(id, name = "license_validations_pkey")
}
