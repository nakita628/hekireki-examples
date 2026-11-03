package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserBaseOrderTable : IdTable<String>("user_base_order") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "user_base_order_user_id_fkey",
        )
    val baseId =
        reference(
            "base_id",
            BaseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "user_base_order_base_id_fkey",
        )
    val order = double("order")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "user_base_order_pkey")

    init {
        index("user_base_order_user_id_order_idx", false, userId, order)
        index("user_base_order_base_id_idx", false, baseId)
        uniqueIndex("user_base_order_user_id_base_id_key", userId, baseId)
    }
}
