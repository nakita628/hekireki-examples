package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AutomationJobRunTable : IdTable<String>("\"AutomationJobRun\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status =
        pgEnum("status", "\"AutomationJobRunStatus\"", AutomationJobRunStatus.entries, AutomationJobRunStatus::dbName).default(
            AutomationJobRunStatus.PENDING,
        )
    val scheduledFor = pgTimestamp("scheduledFor", 3)
    val processedAt = pgTimestamp("processedAt", 3).nullable()
    val outboundMessage = text("outboundMessage").nullable()
    val providerMessageId = text("providerMessageId").nullable()
    val error = text("error").nullable()
    val automationJobId =
        reference(
            "automationJobId",
            AutomationJobTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AutomationJobRun_automationJobId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"AutomationJobRun_pkey\"")

    init {
        index("\"AutomationJobRun_automationJobId_createdAt_idx\"", false, automationJobId, createdAt)
        index("\"AutomationJobRun_status_createdAt_idx\"", false, status, createdAt)
        uniqueIndex("\"AutomationJobRun_automationJobId_scheduledFor_key\"", automationJobId, scheduledFor)
    }
}
