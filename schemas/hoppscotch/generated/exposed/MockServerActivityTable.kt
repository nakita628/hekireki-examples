package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MockServerActivityTable : IdTable<String>("\"MockServerActivity\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val mockServerID =
        reference(
            "mockServerID",
            MockServerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MockServerActivity_mockServerID_fkey\"",
        )
    val action = pgEnum("action", "\"MockServerAction\"", MockServerAction.entries, MockServerAction::dbName)
    val performedBy = text("performedBy").nullable()
    val performedAt = pgTimestamptz("performedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"MockServerActivity_pkey\"")

    init {
        index("\"MockServerActivity_mockServerID_idx\"", false, mockServerID)
    }
}
