package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class VisitorGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VisitorGroupEntity>(VisitorGroupTable)

    var name by VisitorGroupTable.name
    var emails by VisitorGroupTable.emails
    var teamId by VisitorGroupTable.teamId
    var team by TeamEntity referencedOn VisitorGroupTable.teamId
    val links by LinkVisitorGroupEntity referrersOn LinkVisitorGroupTable.visitorGroupId
    var createdAt by VisitorGroupTable.createdAt
    var updatedAt by VisitorGroupTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(VisitorGroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
