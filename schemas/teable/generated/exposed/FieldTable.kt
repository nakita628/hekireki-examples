package teable.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FieldTable : IdTable<String>("field") {
    override val id = text("id").entityId()
    val name = text("name")
    val description = text("description").nullable()
    val options1 = text("options").nullable()
    val meta = text("meta").nullable()
    val aiConfig = text("ai_config").nullable()
    val type = text("type")
    val cellValueType = text("cell_value_type")
    val isMultipleCellValue = bool("is_multiple_cell_value").nullable()
    val dbFieldType = text("db_field_type")
    val dbFieldName = text("db_field_name")
    val provisionState =
        pgEnum("provision_state", "\"ProvisionState\"", ProvisionState.entries, ProvisionState::dbName).default(ProvisionState.READY)
    val notNull = bool("not_null").nullable()
    val unique = bool("unique").nullable()
    val isPrimary = bool("is_primary").nullable()
    val isComputed = bool("is_computed").nullable()
    val isLookup = bool("is_lookup").nullable()
    val isConditionalLookup = bool("is_conditional_lookup").nullable()
    val isPending = bool("is_pending").nullable()
    val hasError = bool("has_error").nullable()
    val lookupLinkedFieldId = text("lookup_linked_field_id").nullable()
    val lookupOptions = text("lookup_options").nullable()
    val tableId =
        reference(
            "table_id",
            TableMetaTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "field_table_id_fkey",
        )
    val order = double("order")
    val version = integer("version")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "field_pkey")

    init {
        index("field_lookup_linked_field_id_idx", false, lookupLinkedFieldId)
        index("field_table_id_deleted_time_idx", false, tableId, deletedTime)
    }
}
