package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SignaturesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SignaturesEntity>(SignaturesTable)

    var organizationId by SignaturesTable.organizationId
    var content by SignaturesTable.content
    var autoAdd by SignaturesTable.autoAdd
    var createdAt by SignaturesTable.createdAt
    var updatedAt by SignaturesTable.updatedAt
    var deletedAt by SignaturesTable.deletedAt
    var organization by OrganizationEntity referencedOn SignaturesTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SignaturesTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
