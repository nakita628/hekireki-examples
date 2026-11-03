package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LicenseValidationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LicenseValidationEntity>(LicenseValidationTable)

    var licenseId by LicenseValidationTable.licenseId
    var license by LicenseEntity referencedOn LicenseValidationTable.licenseId
    var ipAddress by LicenseValidationTable.ipAddress
    var fingerprint by LicenseValidationTable.fingerprint
    var validatedAt by LicenseValidationTable.validatedAt
    var userAgent by LicenseValidationTable.userAgent
}
