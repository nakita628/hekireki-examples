package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TableViewPresetTable : IdTable<String>("table_view_presets") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "table_view_presets_project_id_fkey",
        )
    val name = text("name")
    val tableName1 = text("table_name")
    val createdBy =
        optReference(
            "created_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "table_view_presets_created_by_fkey",
        )
    val updatedBy =
        optReference(
            "updated_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "table_view_presets_updated_by_fkey",
        )
    val filters = jsonb<String>("filters", { it }, { it })
    val columnOrder = jsonb<String>("column_order", { it }, { it })
    val columnVisibility = jsonb<String>("column_visibility", { it }, { it })
    val searchQuery = text("search_query").nullable()
    val orderBy = jsonb<String>("order_by", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "table_view_presets_pkey")

    init {
        uniqueIndex("table_view_presets_project_id_table_name_name_key", projectId, tableName1, name)
    }
}
