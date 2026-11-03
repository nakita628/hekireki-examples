package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class StarEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, StarEntity>(StarTable)

    var stars by StarTable.stars
    var totalStars by StarTable.totalStars
    var forks by StarTable.forks
    var totalForks by StarTable.totalForks
    var login by StarTable.login
    var date by StarTable.date
    var createdAt by StarTable.createdAt
    var updatedAt by StarTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(StarTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
