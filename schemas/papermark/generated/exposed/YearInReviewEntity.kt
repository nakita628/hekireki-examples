package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class YearInReviewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, YearInReviewEntity>(YearInReviewTable)

    var teamId by YearInReviewTable.teamId
    var status by YearInReviewTable.status
    var attempts by YearInReviewTable.attempts
    var lastAttempted by YearInReviewTable.lastAttempted
    var error by YearInReviewTable.error
    var stats by YearInReviewTable.stats
    var createdAt by YearInReviewTable.createdAt
    var updatedAt by YearInReviewTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(YearInReviewTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
