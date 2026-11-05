package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LoopsEmailSourceTable : IdTable<String>("\"LoopsEmailSource\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val emailMessageId = text("emailMessageId").nullable().uniqueIndex("\"LoopsEmailSource_emailMessageId_key\"")
    val loopId = text("loopId").nullable()
    val loopName = text("loopName").nullable()
    val campaignId = text("campaignId").nullable()
    val campaignName = text("campaignName").nullable()
    val sourceType = text("sourceType").nullable()
    val eventTime = pgTimestamp("eventTime", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LoopsEmailSource_pkey\"")

    init {
        index("\"LoopsEmailSource_loopId_idx\"", false, loopId)
        index("\"LoopsEmailSource_campaignId_idx\"", false, campaignId)
        uniqueIndex(
            "\"LoopsEmailSource_loopId_unversioned_key\"",
            loopId,
            filterCondition = sqlCondition("(\"emailMessageId\" IS NULL AND \"loopId\" IS NOT NULL)"),
        )
        uniqueIndex(
            "\"LoopsEmailSource_campaignId_unversioned_key\"",
            campaignId,
            filterCondition = sqlCondition("(\"emailMessageId\" IS NULL AND \"campaignId\" IS NOT NULL)"),
        )
    }
}
