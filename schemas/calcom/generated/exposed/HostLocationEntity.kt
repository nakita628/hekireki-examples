package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class HostLocationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, HostLocationEntity>(HostLocationTable)

    var userId by HostLocationTable.userId
    var eventTypeId by HostLocationTable.eventTypeId
    var host by HostEntity referencedOn HostLocationTable
    var type by HostLocationTable.type
    var credentialId by HostLocationTable.credentialId
    var credential by CredentialEntity optionalReferencedOn HostLocationTable.credentialId
    var link by HostLocationTable.link
    var address by HostLocationTable.address
    var phoneNumber by HostLocationTable.phoneNumber
    var createdAt by HostLocationTable.createdAt
    var updatedAt by HostLocationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(HostLocationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
