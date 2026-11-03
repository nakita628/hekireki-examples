package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ContactEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ContactEntity>(ContactTable)

    var createdAt by ContactTable.createdAt
    var updatedAt by ContactTable.updatedAt
    var workspace by WorkspaceEntity referencedOn ContactTable.workspaceId
    var workspaceId by ContactTable.workspaceId
    val responses by ResponseEntity optionalReferrersOn ResponseTable.contactId
    val attributes by ContactAttributeEntity referrersOn ContactAttributeTable.contactId
    val displays by DisplayEntity optionalReferrersOn DisplayTable.contactId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ContactTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
