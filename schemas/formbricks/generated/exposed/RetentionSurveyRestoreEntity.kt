package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RetentionSurveyRestoreEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RetentionSurveyRestoreEntity>(RetentionSurveyRestoreTable)

    val survey by SurveyEntity referencedOn RetentionSurveyRestoreTable.id
    var restoredAt by RetentionSurveyRestoreTable.restoredAt
}
