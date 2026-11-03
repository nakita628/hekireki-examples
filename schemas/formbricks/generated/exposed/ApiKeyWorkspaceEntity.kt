package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ApiKeyWorkspaceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ApiKeyWorkspaceEntity>(ApiKeyWorkspaceTable)

    var createdAt by ApiKeyWorkspaceTable.createdAt
    var updatedAt by ApiKeyWorkspaceTable.updatedAt
    var apiKeyId by ApiKeyWorkspaceTable.apiKeyId
    var apiKey by ApiKeyEntity referencedOn ApiKeyWorkspaceTable.apiKeyId
    var workspace by WorkspaceEntity referencedOn ApiKeyWorkspaceTable.workspaceId
    var workspaceId by ApiKeyWorkspaceTable.workspaceId
    var permission by ApiKeyWorkspaceTable.permission

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ApiKeyWorkspaceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
