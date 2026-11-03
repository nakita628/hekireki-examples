package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LicenseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LicenseEntity>(LicenseTable)

    var licenseKey by LicenseTable.licenseKey
    var idempotencyKey by LicenseTable.idempotencyKey
    var version by LicenseTable.version
    var type by LicenseTable.type
    var seats by LicenseTable.seats
    var issuedAt by LicenseTable.issuedAt
    var expiresAt by LicenseTable.expiresAt
    var licenseeEmail by LicenseTable.licenseeEmail
    var licenseeName by LicenseTable.licenseeName
    var whiteLabelAddon by LicenseTable.whiteLabelAddon
    var status by LicenseTable.status
    val validations by LicenseValidationEntity referrersOn LicenseValidationTable.licenseId
}
