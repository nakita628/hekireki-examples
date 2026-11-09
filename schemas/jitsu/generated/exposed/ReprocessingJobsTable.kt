package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object ReprocessingJobsTable : IdTable<String>("reprocessing_jobs") {
    override val id = varchar("id", 64).clientDefault { CUID.randomCUID1().toString() }.entityId()
    val config = jsonb<String>("config", { it }, { it })
    val status = varchar("status", 32)
    val createdAt = pgTimestamptz("created_at", 6)
    val startedAt = pgTimestamptz("started_at", 6).nullable()
    val completedAt = pgTimestamptz("completed_at", 6).nullable()
    val k8sJobName = varchar("k8s_job_name", 255).nullable()
    val totalFiles = integer("total_files").nullable().default(0)
    val totalWorkers = integer("total_workers").nullable().default(0)
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(id, name = "reprocessing_jobs_pkey")

    init {
        index("reprocessing_jobs_created_at_idx", false, functions = listOf(indexExpression("\"created_at\" DESC")))
    }
}
