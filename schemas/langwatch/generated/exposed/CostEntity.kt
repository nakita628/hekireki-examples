package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CostEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CostEntity>(CostTable)

    var projectId by CostTable.projectId
    var costType by CostTable.costType
    var costName by CostTable.costName
    var referenceType by CostTable.referenceType
    var referenceId by CostTable.referenceId
    var amount by CostTable.amount
    var currency by CostTable.currency
    var createdAt by CostTable.createdAt
    var updatedAt by CostTable.updatedAt
    var extraInfo by CostTable.extraInfo

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CostTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
