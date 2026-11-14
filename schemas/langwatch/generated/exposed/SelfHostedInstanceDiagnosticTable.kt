package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SelfHostedInstanceDiagnosticTable : IdTable<String>("\"SelfHostedInstanceDiagnostic\"") {
    override val id = text("id").entityId()
    val instanceId = text("instanceId")
    val stepId = text("stepId")
    val stepDescription = text("stepDescription").nullable()
    val release = text("release").nullable()
    val attempt = integer("attempt")
    val trigger = text("trigger")
    val byteLength = integer("byteLength")
    val objectKey = text("objectKey")
    val receivedAt = pgTimestamp("receivedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3)
    val slackAnnouncedAt = pgTimestamp("slackAnnouncedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"SelfHostedInstanceDiagnostic_pkey\"")

    init {
        index("\"SelfHostedInstanceDiagnostic_instanceId_receivedAt_idx\"", false, instanceId, receivedAt)
    }
}
