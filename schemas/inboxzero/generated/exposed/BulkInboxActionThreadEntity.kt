package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class BulkInboxActionThreadEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<BulkInboxActionThreadEntity>(BulkInboxActionThreadTable)

    val jobId by BulkInboxActionThreadTable.jobId
    val threadId by BulkInboxActionThreadTable.threadId
    val job by BulkInboxActionJobEntity referencedOn BulkInboxActionThreadTable.jobId
}
