package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraWorkflowSnapshotTable : CompositeIdTable("mastra_workflow_snapshot") {
    val workflowName = text("workflow_name").entityId()
    val runId = text("run_id").entityId()
    val resourceId = text("resourceId").nullable()
    val snapshot = text("snapshot")
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }

    init {
        index(
            "mastra_workflow_snapshot_name_createdat_idx",
            false,
            functions = listOf(indexExpression("\"workflow_name\""), indexExpression("\"createdAt\" DESC")),
        )
        uniqueIndex("public_mastra_workflow_snapshot_workflow_name_run_id_key", workflowName, runId)
    }
}
