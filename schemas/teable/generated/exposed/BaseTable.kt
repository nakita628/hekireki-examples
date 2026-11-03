package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BaseTable : IdTable<String>("base") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "base_space_id_fkey",
        )
    val name = text("name")
    val order = double("order")
    val icon = text("icon").nullable()
    val schemaPass = text("schema_pass").nullable()
    val v2Enabled = bool("v2_enabled").default(false)
    val provisionState =
        pgEnum("provision_state", "\"ProvisionState\"", ProvisionState.entries, ProvisionState::dbName).default(ProvisionState.READY)
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "base_pkey")

    init {
        index("base_order_idx", false, order)
        index("base_space_id_idx", false, spaceId)
    }
}
