package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PlaygroundConversationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlaygroundConversationEntity>(PlaygroundConversationTable)

    var chatId by PlaygroundConversationTable.chatId
    var title by PlaygroundConversationTable.title
    var agentSlug by PlaygroundConversationTable.agentSlug
    var runId by PlaygroundConversationTable.runId
    var run by TaskRunEntity optionalReferencedOn PlaygroundConversationTable.runId
    var clientData by PlaygroundConversationTable.clientData
    var messages by PlaygroundConversationTable.messages
    var lastEventId by PlaygroundConversationTable.lastEventId
    var project by ProjectEntity referencedOn PlaygroundConversationTable.projectId
    var projectId by PlaygroundConversationTable.projectId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn PlaygroundConversationTable.runtimeEnvironmentId
    var runtimeEnvironmentId by PlaygroundConversationTable.runtimeEnvironmentId
    var userId by PlaygroundConversationTable.userId
    var createdAt by PlaygroundConversationTable.createdAt
    var updatedAt by PlaygroundConversationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PlaygroundConversationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
