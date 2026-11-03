package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConversationViewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConversationViewEntity>(ConversationViewTable)

    var conversationId by ConversationViewTable.conversationId
    var conversation by ConversationEntity referencedOn ConversationViewTable.conversationId
    var viewId by ConversationViewTable.viewId
    var view by ViewEntity referencedOn ConversationViewTable.viewId
    var createdAt by ConversationViewTable.createdAt
}
