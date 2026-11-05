package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SnippetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SnippetEntity>(SnippetTable)

    var createdAt by SnippetTable.createdAt
    var updatedAt by SnippetTable.updatedAt
    var shortcut by SnippetTable.shortcut
    var content by SnippetTable.content
    var emailAccountId by SnippetTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn SnippetTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SnippetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
