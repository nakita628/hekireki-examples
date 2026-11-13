package inboxzero.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object BulkInboxActionThreadTable : CompositeIdTable("\"BulkInboxActionThread\"") {
    val jobId =
        reference(
            "jobId",
            BulkInboxActionJobTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BulkInboxActionThread_jobId_fkey\"",
        )
    val threadId = text("threadId").entityId()
    override val primaryKey = PrimaryKey(jobId, threadId, name = "\"BulkInboxActionThread_pkey\"")

    init {
        addIdColumn(jobId)
    }
}
