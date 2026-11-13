package rallly.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiUsageTable : CompositeIdTable("api_usage") {
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "api_usage_space_id_fkey",
        )
    val resource = pgEnum("resource", "\"api_usage_resource\"", ApiUsageResource.entries, ApiUsageResource::dbName).entityId()
    val period = pgDate("period").entityId()
    val count = integer("count").default(0)
    val nearLimitWarnedAt = pgTimestamp("near_limit_warned_at", 3).nullable()
    val atLimitWarnedAt = pgTimestamp("at_limit_warned_at", 3).nullable()
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(spaceId, resource, period, name = "api_usage_pkey")

    init {
        addIdColumn(spaceId)
    }
}
