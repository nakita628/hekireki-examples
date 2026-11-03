package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OnlineUpgradeStateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OnlineUpgradeStateEntity>(OnlineUpgradeStateTable)

    var targetImage by OnlineUpgradeStateTable.targetImage
    var phase by OnlineUpgradeStateTable.phase
    var completedPhases by OnlineUpgradeStateTable.completedPhases
    var surveyCursor by OnlineUpgradeStateTable.surveyCursor
    var surveyCount by OnlineUpgradeStateTable.surveyCount
    var baselineCompletedAt by OnlineUpgradeStateTable.baselineCompletedAt
    var forwardOnlyAt by OnlineUpgradeStateTable.forwardOnlyAt
    var queuePausedAt by OnlineUpgradeStateTable.queuePausedAt
    var queuePauseDeadlineAt by OnlineUpgradeStateTable.queuePauseDeadlineAt
    var queueResumedAt by OnlineUpgradeStateTable.queueResumedAt
    var activatedAt by OnlineUpgradeStateTable.activatedAt
    var updatedAt by OnlineUpgradeStateTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OnlineUpgradeStateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
