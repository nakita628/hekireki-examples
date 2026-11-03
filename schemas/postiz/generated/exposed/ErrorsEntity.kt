package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ErrorsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ErrorsEntity>(ErrorsTable)

    var message by ErrorsTable.message
    var platform by ErrorsTable.platform
    var organizationId by ErrorsTable.organizationId
    var createdAt by ErrorsTable.createdAt
    var updatedAt by ErrorsTable.updatedAt
    var postId by ErrorsTable.postId
    var body by ErrorsTable.body
    var organization by OrganizationEntity referencedOn ErrorsTable.organizationId
    var post by PostEntity referencedOn ErrorsTable.postId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ErrorsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
