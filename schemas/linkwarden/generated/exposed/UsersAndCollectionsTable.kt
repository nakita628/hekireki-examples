package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UsersAndCollectionsTable : CompositeIdTable("\"UsersAndCollections\"") {
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UsersAndCollections_userId_fkey\"",
        )
    val collectionId =
        reference(
            "collectionId",
            CollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UsersAndCollections_collectionId_fkey\"",
        )
    val canCreate = bool("canCreate")
    val canUpdate = bool("canUpdate")
    val canDelete = bool("canDelete")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(userId, collectionId, name = "\"UsersAndCollections_pkey\"")

    init {
        addIdColumn(userId)
        addIdColumn(collectionId)
        index("\"UsersAndCollections_userId_idx\"", false, userId)
    }
}
