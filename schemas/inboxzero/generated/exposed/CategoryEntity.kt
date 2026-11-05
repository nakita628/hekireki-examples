package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CategoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CategoryEntity>(CategoryTable)

    var createdAt by CategoryTable.createdAt
    var updatedAt by CategoryTable.updatedAt
    var name by CategoryTable.name
    var description by CategoryTable.description
    var emailAccountId by CategoryTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn CategoryTable.emailAccountId
    val emailSenders by NewsletterEntity optionalReferrersOn NewsletterTable.categoryId
    var rules by RuleEntity via CategoryToRuleTable

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CategoryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
