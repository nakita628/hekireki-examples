package blinko.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> = emptyList()
    val tables: List<Table> =
        listOf(
            AccountsTable,
            AttachmentsTable,
            ConfigTable,
            NotesTable,
            CommentsTable,
            TagTable,
            TagsToNoteTable,
            NoteReferenceTable,
            FollowsTable,
            NotificationsTable,
            CacheTable,
            PluginTable,
            ConversationTable,
            MessageTable,
            NoteHistoryTable,
            NoteInternalShareTable,
            SessionTable,
            AiProvidersTable,
            AiModelsTable,
            AiScheduledTaskTable,
            McpServersTable,
            FontsTable,
        )
}
