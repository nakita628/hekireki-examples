package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ClassificationFeedbackEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ClassificationFeedbackEntity>(ClassificationFeedbackTable)

    var createdAt by ClassificationFeedbackTable.createdAt
    var sender by ClassificationFeedbackTable.sender
    var eventType by ClassificationFeedbackTable.eventType
    var ruleId by ClassificationFeedbackTable.ruleId
    var rule by RuleEntity referencedOn ClassificationFeedbackTable.ruleId
    var emailAccountId by ClassificationFeedbackTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ClassificationFeedbackTable.emailAccountId
    var threadId by ClassificationFeedbackTable.threadId
    var messageId by ClassificationFeedbackTable.messageId
}
