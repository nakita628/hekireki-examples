package typebot.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CollaboratorsOnTypebotsTable : CompositeIdTable("\"CollaboratorsOnTypebots\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CollaboratorsOnTypebots_userId_fkey\"",
        )
    val typebotId =
        reference(
            "typebotId",
            TypebotTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CollaboratorsOnTypebots_typebotId_fkey\"",
        )
    val type = pgEnum("type", "\"CollaborationType\"", CollaborationType.entries, CollaborationType::dbName)

    init {
        addIdColumn(userId)
        addIdColumn(typebotId)
        uniqueIndex("\"CollaboratorsOnTypebots_userId_typebotId_key\"", userId, typebotId)
    }
}
