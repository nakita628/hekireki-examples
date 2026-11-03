package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ThemeTemplateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ThemeTemplateEntity>(ThemeTemplateTable)

    var createdAt by ThemeTemplateTable.createdAt
    var updatedAt by ThemeTemplateTable.updatedAt
    var name by ThemeTemplateTable.name
    var theme by ThemeTemplateTable.theme
    var workspace by WorkspaceEntity referencedOn ThemeTemplateTable.workspaceId
    var workspaceId by ThemeTemplateTable.workspaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ThemeTemplateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
