package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GroupEntity>(GroupTable)

    var createdAt by GroupTable.createdAt
    var updatedAt by GroupTable.updatedAt
    var name by GroupTable.name
    var prompt by GroupTable.prompt
    val items by GroupItemEntity optionalReferrersOn GroupItemTable.groupId
    val rule by RuleEntity optionalBackReferencedOn RuleTable.groupId
    var emailAccountId by GroupTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn GroupTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
