package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TagsPostsTable : CompositeIdTable("\"TagsPosts\"") {
    val postId =
        reference(
            "postId",
            PostTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagsPosts_postId_fkey\"",
        )
    val tagId =
        reference(
            "tagId",
            TagsTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagsPosts_tagId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(postId, tagId, name = "\"TagsPosts_pkey\"")

    init {
        addIdColumn(postId)
        addIdColumn(tagId)
        uniqueIndex("\"TagsPosts_postId_tagId_key\"", postId, tagId)
    }
}
