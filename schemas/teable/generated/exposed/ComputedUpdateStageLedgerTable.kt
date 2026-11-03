package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object ComputedUpdateStageLedgerTable : CompositeIdTable("computed_update_stage_ledger") {
    val scopeId = text("scope_id").entityId()
    val kind = text("kind").entityId()
    val tableId = text("table_id").entityId()
    val recordId = text("record_id").entityId()
    val seq = long("seq").default(0L)
    override val primaryKey = PrimaryKey(scopeId, kind, tableId, recordId, name = "computed_update_stage_ledger_pkey")

    init {
        index("computed_update_stage_ledger_scope_id_kind_seq_idx", false, scopeId, kind, seq)
    }
}
