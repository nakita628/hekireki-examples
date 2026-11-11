package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMMCPToolsetTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMMCPToolsetTableEntity>(LiteLLMMCPToolsetTableTable)

    var toolsetName by LiteLLMMCPToolsetTableTable.toolsetName
    var description by LiteLLMMCPToolsetTableTable.description
    var tools by LiteLLMMCPToolsetTableTable.tools
    var createdAt by LiteLLMMCPToolsetTableTable.createdAt
    var createdBy by LiteLLMMCPToolsetTableTable.createdBy
    var updatedAt by LiteLLMMCPToolsetTableTable.updatedAt
    var updatedBy by LiteLLMMCPToolsetTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMMCPToolsetTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
