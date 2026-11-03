package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserLastVisitTable : IdTable<String>("user_last_visit") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId = text("user_id")
    val resourceType = text("resource_type")
    val resourceId = text("resource_id")
    val parentResourceId = text("parent_resource_id")
    val lastVisitTime =
        pgTimestamp("last_visit_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "user_last_visit_pkey")

    init {
        index("user_last_visit_user_id_resource_type_parent_resource_id_idx", false, userId, resourceType, parentResourceId)
        uniqueIndex("user_last_visit_user_id_resource_type_resource_id_key", userId, resourceType, resourceId)
    }
}
