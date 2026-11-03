package formbricks.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OnlineUpgradeStateTable : IdTable<String>("\"OnlineUpgradeState\"") {
    override val id = text("release").entityId()
    val targetImage = text("targetImage")
    val phase = text("phase")
    val completedPhases = array("completedPhases", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val surveyCursor = text("surveyCursor").nullable()
    val surveyCount = long("surveyCount").default(0L)
    val baselineCompletedAt = pgTimestamp("baselineCompletedAt", 3).nullable()
    val forwardOnlyAt = pgTimestamp("forwardOnlyAt", 3).nullable()
    val queuePausedAt = pgTimestamp("queuePausedAt", 3).nullable()
    val queuePauseDeadlineAt = pgTimestamp("queuePauseDeadlineAt", 3).nullable()
    val queueResumedAt = pgTimestamp("queueResumedAt", 3).nullable()
    val activatedAt = pgTimestamp("activatedAt", 3).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OnlineUpgradeState_pkey\"")
}
