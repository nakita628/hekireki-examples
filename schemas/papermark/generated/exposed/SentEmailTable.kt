package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SentEmailTable : IdTable<String>("\"SentEmail\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = pgEnum("type", "\"EmailType\"", EmailType.entries, EmailType::dbName)
    val recipient = text("recipient")
    val marketing = bool("marketing").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SentEmail_teamId_fkey\"",
        )
    val domainSlug = text("domainSlug").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"SentEmail_pkey\"")

    init {
        index("\"SentEmail_teamId_idx\"", false, teamId)
    }
}
