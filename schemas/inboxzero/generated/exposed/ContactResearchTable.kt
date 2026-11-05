package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ContactResearchTable : IdTable<String>("\"ContactResearch\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email")
    val researchStartedAt = pgTimestamp("researchStartedAt", 3)
    val found = bool("found")
    val role = text("role").nullable()
    val confidence = text("confidence").nullable()
    val company = jsonb<String>("company", { it }, { it }).nullable()
    val sources = array("sources", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "\"ContactResearch_pkey\"")

    init {
        index(
            "\"ContactResearch_latest_idx\"",
            false,
            functions =
                listOf(
                    indexExpression("\"email\""),
                    indexExpression("\"researchStartedAt\" DESC"),
                    indexExpression("\"createdAt\" DESC"),
                    indexExpression("\"id\" DESC"),
                ),
        )
    }
}
