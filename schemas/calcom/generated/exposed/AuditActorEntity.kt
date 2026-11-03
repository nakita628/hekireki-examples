package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class AuditActorEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, AuditActorEntity>(AuditActorTable)

    var type by AuditActorTable.type
    var userUuid by AuditActorTable.userUuid
    var attendeeId by AuditActorTable.attendeeId
    var credentialId by AuditActorTable.credentialId
    var email by AuditActorTable.email
    var phone by AuditActorTable.phone
    var name by AuditActorTable.name
    var createdAt by AuditActorTable.createdAt
    val bookingAudits by BookingAuditEntity referrersOn BookingAuditTable.actorId
}
