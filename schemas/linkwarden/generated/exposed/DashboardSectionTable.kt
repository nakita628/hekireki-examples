package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DashboardSectionTable : IdTable<Int>("\"DashboardSection\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DashboardSection_userId_fkey\"",
        )
    val collectionId =
        optReference(
            "collectionId",
            CollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DashboardSection_collectionId_fkey\"",
        )
    val type = pgEnum("type", "\"DashboardSectionType\"", DashboardSectionType.entries, DashboardSectionType::dbName)
    val order = integer("order")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DashboardSection_pkey\"")

    init {
        uniqueIndex("\"DashboardSection_userId_collectionId_key\"", userId, collectionId)
    }
}
