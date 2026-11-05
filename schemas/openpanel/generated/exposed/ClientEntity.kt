package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class ClientEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ClientEntity>(ClientTable)

    var name by ClientTable.name
    var secret by ClientTable.secret
    var type by ClientTable.type
    var projectId by ClientTable.projectId
    var project by ProjectEntity optionalReferencedOn ClientTable.projectId
    var organization by OrganizationEntity referencedOn ClientTable.organizationId
    var organizationId by ClientTable.organizationId
    var ignoreCorsAndSecret by ClientTable.ignoreCorsAndSecret
    var createdAt by ClientTable.createdAt
    var updatedAt by ClientTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ClientTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
