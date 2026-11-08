package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LicenseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LicenseEntity>(LicenseTable)

    var key by LicenseTable.key
    var userId by LicenseTable.userId
    var user by UserEntity optionalReferencedOn LicenseTable.userId
    var validFrom by LicenseTable.validFrom
    var validUntil by LicenseTable.validUntil
    var maxUsers by LicenseTable.maxUsers
    var features by LicenseTable.features
    var activatedAt by LicenseTable.activatedAt
    var lastCheckedAt by LicenseTable.lastCheckedAt
    var createdAt by LicenseTable.createdAt
}
