package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SlackIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SlackIntegrationEntity>(SlackIntegrationTable)

    var projectId by SlackIntegrationTable.projectId
    var project by ProjectEntity referencedOn SlackIntegrationTable.projectId
    var teamId by SlackIntegrationTable.teamId
    var teamName by SlackIntegrationTable.teamName
    var botToken by SlackIntegrationTable.botToken
    var botUserId by SlackIntegrationTable.botUserId
    var installerUserId by SlackIntegrationTable.installerUserId
    var installerSlackUserId by SlackIntegrationTable.installerSlackUserId
    var createdAt by SlackIntegrationTable.createdAt
    var updatedAt by SlackIntegrationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SlackIntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
