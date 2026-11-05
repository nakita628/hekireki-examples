package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConversationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConversationEntity>(ConversationTable)

    var projectId by ConversationTable.projectId
    var project by ProjectEntity referencedOn ConversationTable.projectId
    var organizationId by ConversationTable.organizationId
    var organization by OrganizationEntity referencedOn ConversationTable.organizationId
    var userId by ConversationTable.userId
    var user by UserEntity referencedOn ConversationTable.userId
    var title by ConversationTable.title
    var createdAt by ConversationTable.createdAt
    var updatedAt by ConversationTable.updatedAt
    val messages by ChatMessageEntity referrersOn ChatMessageTable.conversationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConversationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
