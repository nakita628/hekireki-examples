package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class NewsletterEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, NewsletterEntity>(NewsletterTable)

    var createdAt by NewsletterTable.createdAt
    var updatedAt by NewsletterTable.updatedAt
    var email by NewsletterTable.email
    var name by NewsletterTable.name
    var status by NewsletterTable.status
    var patternAnalyzed by NewsletterTable.patternAnalyzed
    var lastAnalyzedAt by NewsletterTable.lastAnalyzedAt
    var emailAccountId by NewsletterTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn NewsletterTable.emailAccountId
    var categoryId by NewsletterTable.categoryId
    var category by CategoryEntity optionalReferencedOn NewsletterTable.categoryId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(NewsletterTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
