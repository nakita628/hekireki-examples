package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PinResourceTable : IdTable<String>("pin_resource") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = text("type")
    val resourceId = text("resource_id")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val order = double("order")
    override val primaryKey = PrimaryKey(id, name = "pin_resource_pkey")

    init {
        index("pin_resource_order_idx", false, order)
        uniqueIndex("pin_resource_created_by_resource_id_key", createdBy, resourceId)
    }
}
