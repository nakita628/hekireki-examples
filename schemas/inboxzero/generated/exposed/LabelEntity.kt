package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LabelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LabelEntity>(LabelTable)

    var createdAt by LabelTable.createdAt
    var updatedAt by LabelTable.updatedAt
    var gmailLabelId by LabelTable.gmailLabelId
    var name by LabelTable.name
    var description by LabelTable.description
    var enabled by LabelTable.enabled
    var emailAccountId by LabelTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn LabelTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LabelTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
