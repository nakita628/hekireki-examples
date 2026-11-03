package papermark.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserDataroomTable : CompositeIdTable("\"UserDataroom\"") {
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserDataroom_userId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserDataroom_teamId_fkey\"",
        )
    val dataroomId =
        reference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserDataroom_dataroomId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(userId, dataroomId, name = "\"UserDataroom_pkey\"")

    init {
        addIdColumn(userId)
        addIdColumn(dataroomId)
        index("\"UserDataroom_userId_idx\"", false, userId)
        index("\"UserDataroom_teamId_idx\"", false, teamId)
        index("\"UserDataroom_dataroomId_idx\"", false, dataroomId)
    }
}
