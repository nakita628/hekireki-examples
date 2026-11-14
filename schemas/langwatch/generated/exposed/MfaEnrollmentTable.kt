package langwatch.models

import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MfaEnrollmentTable : IdTable<String>("\"MfaEnrollment\"") {
    override val id = text("userId").entityId()
    val enrollmentId = text("enrollmentId").nullable()
    val method = text("method").nullable()
    val state = text("state")
    val enrolledAt = pgTimestamp("enrolledAt", 3).nullable()
    val confirmedAt = pgTimestamp("confirmedAt", 3).nullable()
    val expiredAt = pgTimestamp("expiredAt", 3).nullable()
    val disabledAt = pgTimestamp("disabledAt", 3).nullable()
    val disabledVia = text("disabledVia").nullable()
    val backupCodeCount = integer("backupCodeCount").default(0)
    val consumedBackupCodeIndexes =
        array("consumedBackupCodeIndexes", IntegerColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val failedCount = integer("failedCount").default(0)
    val occurredAt = pgTimestamp("occurredAt", 3)
    val lastEventId = text("lastEventId")
    val acceptedAt = pgTimestamp("acceptedAt", 3)
    val projectionVersion = text("projectionVersion")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"MfaEnrollment_pkey\"")

    init {
        index("\"MfaEnrollment_state_idx\"", false, state)
    }
}
