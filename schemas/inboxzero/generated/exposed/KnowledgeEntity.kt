package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class KnowledgeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, KnowledgeEntity>(KnowledgeTable)

    var createdAt by KnowledgeTable.createdAt
    var updatedAt by KnowledgeTable.updatedAt
    var title by KnowledgeTable.title
    var content by KnowledgeTable.content
    var emailAccountId by KnowledgeTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn KnowledgeTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(KnowledgeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
