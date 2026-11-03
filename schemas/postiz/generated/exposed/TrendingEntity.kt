package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TrendingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TrendingEntity>(TrendingTable)

    var trendingList by TrendingTable.trendingList
    var language by TrendingTable.language
    var hash by TrendingTable.hash
    var date by TrendingTable.date
    var createdAt by TrendingTable.createdAt
    var updatedAt by TrendingTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TrendingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
