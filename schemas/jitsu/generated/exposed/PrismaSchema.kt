package jitsu.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"WorkspaceRole\" AS ENUM ('owner', 'editor', 'analyst')",
            "CREATE TYPE \"reverse_sync_mode\" AS ENUM ('upsert', 'mirror')",
            "CREATE TYPE \"reverse_sync_extraction\" AS ENUM ('cursor', 'full')",
        )
    val tables: List<Table> =
        listOf(
            EmailLogTable,
            UserPreferencesTable,
            UserProfileTable,
            UserPasswordTable,
            WorkspaceTable,
            WorkspaceOptionsTable,
            WorkspaceAccessTable,
            WorkspaceUserPropertiesTable,
            UserApiTokenTable,
            OAuthClientTable,
            KvStoreEntryTable,
            OAuthAccessTokenTable,
            InvitationTokenTable,
            ConfigurationObjectTable,
            ConfigurationObjectLinkTable,
            ConnectorPackageTable,
            AuditLogTable,
            GlobalPropsTable,
            SourceSpecTable,
            SourceCheckTable,
            SourceCatalogTable,
            ReverseSyncControlTable,
            ReverseSyncTargetOwnerTable,
            SourceStateTable,
            SourceTaskTable,
            TaskLogTable,
            ProfileBuilderTable,
            ProfileBuilderFunctionTable,
            ProfileBuilderStateTable,
            ProfileBuilderState2Table,
            StatusChangeTable,
            NotificationStateTable,
            NotificationTable,
            OidcProviderTable,
            OidcLoginGroupTable,
            ReprocessingJobsTable,
            ReprocessingWorkersTable,
            FunctionsServerTable,
            RateLimitCounterTable,
        )
}
