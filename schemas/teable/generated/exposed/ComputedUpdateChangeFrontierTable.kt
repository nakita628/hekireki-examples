package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object ComputedUpdateChangeFrontierTable : CompositeIdTable("computed_update_change_frontier") {
    val scopeId = text("scope_id").entityId()
    val kind = text("kind").entityId()
    val tableId = text("table_id").entityId()
    val recordId = text("record_id").entityId()
    val fieldId = text("field_id").entityId()
    override val primaryKey = PrimaryKey(scopeId, kind, tableId, recordId, fieldId, name = "computed_update_change_frontier_pkey")
}
