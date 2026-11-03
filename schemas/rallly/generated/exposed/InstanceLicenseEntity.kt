package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InstanceLicenseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InstanceLicenseEntity>(InstanceLicenseTable)

    var licenseKey by InstanceLicenseTable.licenseKey
    var version by InstanceLicenseTable.version
    var type by InstanceLicenseTable.type
    var seats by InstanceLicenseTable.seats
    var issuedAt by InstanceLicenseTable.issuedAt
    var expiresAt by InstanceLicenseTable.expiresAt
    var licenseeEmail by InstanceLicenseTable.licenseeEmail
    var licenseeName by InstanceLicenseTable.licenseeName
    var whiteLabelAddon by InstanceLicenseTable.whiteLabelAddon
    var status by InstanceLicenseTable.status
}
