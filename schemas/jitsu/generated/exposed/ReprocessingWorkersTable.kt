package jitsu.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ReprocessingWorkersTable : CompositeIdTable("reprocessing_workers") {
    val jobId =
        reference(
            "job_id",
            ReprocessingJobsTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.NO_ACTION,
            fkName = "reprocessing_workers_job_id_fkey",
        )
    val workerIndex = integer("worker_index").entityId()
    val status = varchar("status", 32)
    val startedAt = pgTimestamptz("started_at", 6).nullable()
    val updatedAt = pgTimestamptz("updated_at", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamptz("completed_at", 6).nullable()
    val currentFile = text("current_file").nullable()
    val currentLine = long("current_line").nullable().default(0L)
    val currentTs = text("current_ts").nullable()
    val assignedFiles = integer("assigned_files").nullable().default(0)
    val processedFiles = integer("processed_files").nullable().default(0)
    val totalLines = long("total_lines").nullable().default(0L)
    val successCount = long("success_count").nullable().default(0L)
    val errorCount = long("error_count").nullable().default(0L)
    val skippedCount = long("skipped_count").nullable().default(0L)
    val totalBytes = long("total_bytes").nullable().default(0L)
    val processedBytes = long("processed_bytes").nullable().default(0L)
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(jobId, workerIndex, name = "reprocessing_workers_pkey")

    init {
        addIdColumn(jobId)
        index("reprocessing_workers_job_id_idx", false, jobId)
        index("reprocessing_workers_status_idx", false, jobId, status)
    }
}
