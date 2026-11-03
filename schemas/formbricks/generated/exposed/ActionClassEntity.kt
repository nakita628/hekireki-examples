package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ActionClassEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ActionClassEntity>(ActionClassTable)

    var createdAt by ActionClassTable.createdAt
    var updatedAt by ActionClassTable.updatedAt
    var name by ActionClassTable.name
    var description by ActionClassTable.description
    var type by ActionClassTable.type
    var key by ActionClassTable.key
    var noCodeConfig by ActionClassTable.noCodeConfig
    var workspace by WorkspaceEntity referencedOn ActionClassTable.workspaceId
    var workspaceId by ActionClassTable.workspaceId
    val surveyTriggers by SurveyTriggerEntity referrersOn SurveyTriggerTable.actionClassId
    val creativeTriggers by CreativeTriggerEntity referrersOn CreativeTriggerTable.actionClassId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ActionClassTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
