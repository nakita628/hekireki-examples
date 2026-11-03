package teable.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TableMetaTable : IdTable<String>("table_meta") {
    override val id = text("id").entityId()
    val baseId =
        reference(
            "base_id",
            BaseTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "table_meta_base_id_fkey",
        )
    val name = text("name")
    val description = text("description").nullable()
    val icon = text("icon").nullable()
    val dbTableName = text("db_table_name")
    val dbViewName = text("db_view_name").nullable()
    val searchIndex = jsonb<String>("search_index", { it }, { it }).nullable()
    val searchIndexPolicy = jsonb<String>("search_index_policy", { it }, { it }).nullable()
    val queryIndexPolicy = jsonb<String>("query_index_policy", { it }, { it }).nullable()
    val provisionState =
        pgEnum("provision_state", "\"ProvisionState\"", ProvisionState.entries, ProvisionState::dbName).default(ProvisionState.READY)
    val version = integer("version")
    val order = double("order")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "table_meta_pkey")

    init {
        index("table_meta_order_idx", false, order)
        index("table_meta_db_table_name_idx", false, dbTableName)
        index("table_meta_base_id_deleted_time_idx", false, baseId, deletedTime)
    }
}
