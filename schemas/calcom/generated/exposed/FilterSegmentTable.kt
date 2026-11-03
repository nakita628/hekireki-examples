package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object FilterSegmentTable : IdTable<Int>("\"FilterSegment\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name")
    val tableIdentifier = text("tableIdentifier")
    val scope = pgEnum("scope", "\"FilterSegmentScope\"", FilterSegmentScope.entries, FilterSegmentScope::dbName)
    val activeFilters = jsonb<String>("activeFilters", { it }, { it }).nullable()
    val sorting = jsonb<String>("sorting", { it }, { it }).nullable()
    val columnVisibility = jsonb<String>("columnVisibility", { it }, { it }).nullable()
    val columnSizing = jsonb<String>("columnSizing", { it }, { it }).nullable()
    val perPage = integer("perPage")
    val searchTerm = text("searchTerm").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"FilterSegment_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"FilterSegment_teamId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"FilterSegment_pkey\"")

    init {
        index("\"FilterSegment_scope_userId_tableIdentifier_idx\"", false, scope, userId, tableIdentifier)
        index("\"FilterSegment_scope_teamId_tableIdentifier_idx\"", false, scope, teamId, tableIdentifier)
    }
}
