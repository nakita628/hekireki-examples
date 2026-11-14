package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class SlackConnectionClaimEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SlackConnectionClaimEntity>(SlackConnectionClaimTable)

    val connectionId by SlackConnectionClaimTable.connectionId
    val claimantId by SlackConnectionClaimTable.claimantId
    var claimantLabel by SlackConnectionClaimTable.claimantLabel
    var organizationId by SlackConnectionClaimTable.organizationId
    var projectId by SlackConnectionClaimTable.projectId
    var createdAt by SlackConnectionClaimTable.createdAt
    var updatedAt by SlackConnectionClaimTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SlackConnectionClaimTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
