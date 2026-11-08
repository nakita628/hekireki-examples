package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class VerificationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VerificationEntity>(VerificationTable)

    var identifier by VerificationTable.identifier
    var value by VerificationTable.value
    var expiresAt by VerificationTable.expiresAt
    var createdAt by VerificationTable.createdAt
    var updatedAt by VerificationTable.updatedAt
}
