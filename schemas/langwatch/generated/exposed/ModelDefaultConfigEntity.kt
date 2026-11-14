package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ModelDefaultConfigEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ModelDefaultConfigEntity>(ModelDefaultConfigTable)

    var config by ModelDefaultConfigTable.config
    var authorId by ModelDefaultConfigTable.authorId
    var createdAt by ModelDefaultConfigTable.createdAt
    var updatedAt by ModelDefaultConfigTable.updatedAt
    var organizationId by ModelDefaultConfigTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ModelDefaultConfigTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
