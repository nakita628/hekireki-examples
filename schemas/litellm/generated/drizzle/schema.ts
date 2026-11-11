import {
  bigint,
  bigserial,
  boolean,
  customType,
  doublePrecision,
  foreignKey,
  index,
  integer,
  jsonb,
  pgEnum,
  pgTable,
  primaryKey,
  serial,
  text,
  timestamp,
  unique,
  uniqueIndex,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import cuid from 'cuid'

const bytea = customType<{ data: Uint8Array }>({
  dataType: () => 'bytea',
})

const utcNow = (() => {
  let now: Date | undefined
  return () => {
    if (now === undefined) {
      now = new Date()
      queueMicrotask(() => {
        now = undefined
      })
    }
    return now
  }
})()

export const jobStatusEnum = pgEnum('JobStatus', ['ACTIVE', 'INACTIVE'])

export const accountingDeliveryStateEnum = pgEnum('AccountingDeliveryState', [
  'pending',
  'applied',
  'parked',
])

export const liteLLMBudgetTable = pgTable('LiteLLM_BudgetTable', {
  budget_id: text('budget_id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  max_budget: doublePrecision('max_budget'),
  soft_budget: doublePrecision('soft_budget'),
  max_parallel_requests: integer('max_parallel_requests'),
  tpm_limit: bigint('tpm_limit', { mode: 'bigint' }),
  rpm_limit: bigint('rpm_limit', { mode: 'bigint' }),
  tpd_limit: bigint('tpd_limit', { mode: 'bigint' }),
  model_max_budget: jsonb('model_max_budget'),
  model_tpm_limit: jsonb('model_tpm_limit'),
  model_rpm_limit: jsonb('model_rpm_limit'),
  budget_duration: text('budget_duration'),
  budget_reset_at: timestamp('budget_reset_at', { precision: 3 }),
  allowed_models: text('allowed_models').array().default([]),
  temp_budget_increase: doublePrecision('temp_budget_increase'),
  temp_budget_expiry: timestamp('temp_budget_expiry', { precision: 3 }),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  created_by: text('created_by').notNull(),
  updated_at: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  updated_by: text('updated_by').notNull(),
})

export const liteLLMCredentialsTable = pgTable(
  'LiteLLM_CredentialsTable',
  {
    credential_id: text('credential_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    credential_name: text('credential_name').notNull(),
    display_name: text('display_name'),
    credential_values: jsonb('credential_values').notNull(),
    credential_info: jsonb('credential_info'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by').notNull(),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by').notNull(),
  },
  (table) => [
    uniqueIndex('LiteLLM_CredentialsTable_credential_name_key').on(table.credential_name),
  ],
)

export const liteLLMProxyModelTable = pgTable('LiteLLM_ProxyModelTable', {
  model_id: text('model_id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  model_name: text('model_name').notNull(),
  litellm_params: jsonb('litellm_params').notNull(),
  model_info: jsonb('model_info'),
  blocked: boolean('blocked').notNull().default(false),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  created_by: text('created_by').notNull(),
  updated_at: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  updated_by: text('updated_by').notNull(),
})

export const liteLLMAgentsTable = pgTable(
  'LiteLLM_AgentsTable',
  {
    agent_id: text('agent_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    agent_name: text('agent_name').notNull(),
    litellm_params: jsonb('litellm_params'),
    agent_card_params: jsonb('agent_card_params').notNull(),
    static_headers: jsonb('static_headers').default({}),
    extra_headers: text('extra_headers').array().default([]),
    kill_switch: jsonb('kill_switch'),
    agent_access_groups: text('agent_access_groups').array().default([]),
    access_group_ids: text('access_group_ids').array().default([]),
    object_permission_id: text('object_permission_id'),
    spend: doublePrecision('spend').notNull().default(0),
    identity_managed: boolean('identity_managed').notNull().default(false),
    enabled: boolean('enabled').notNull().default(true),
    execution_mode: text('execution_mode').notNull().default('autonomous'),
    tpm_limit: integer('tpm_limit'),
    rpm_limit: integer('rpm_limit'),
    session_tpm_limit: integer('session_tpm_limit'),
    session_rpm_limit: integer('session_rpm_limit'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by').notNull(),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by').notNull(),
  },
  (table) => [
    uniqueIndex('LiteLLM_AgentsTable_agent_name_key').on(table.agent_name),
    foreignKey({
      name: 'LiteLLM_AgentsTable_object_permission_id_fkey',
      columns: [table.object_permission_id],
      foreignColumns: [liteLLMObjectPermissionTable.object_permission_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMAgentIdentity = pgTable(
  'LiteLLM_AgentIdentity',
  {
    agent_id: text('agent_id').primaryKey(),
    active: boolean('active').notNull().default(true),
    provider: text('provider').notNull(),
    issuer: text('issuer').notNull(),
    tenant_id: text('tenant_id').notNull(),
    client_id: text('client_id').notNull(),
    service_principal_id: text('service_principal_id'),
    required_roles: text('required_roles').array().default([]),
    required_scopes: text('required_scopes').array().default(['user_impersonation']),
    revision: text('revision')
      .notNull()
      .$defaultFn(() => crypto.randomUUID()),
    last_authenticated_at: timestamp('last_authenticated_at', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('LiteLLM_AgentIdentity_provider_tenant_id_client_id_key').on(
      table.provider,
      table.tenant_id,
      table.client_id,
    ),
    uniqueIndex('LiteLLM_AgentIdentity_issuer_service_principal_id_key').on(
      table.issuer,
      table.service_principal_id,
    ),
    foreignKey({
      name: 'LiteLLM_AgentIdentity_agent_id_fkey',
      columns: [table.agent_id],
      foreignColumns: [liteLLMAgentsTable.agent_id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const liteLLMRetiredAgentIdentity = pgTable(
  'LiteLLM_RetiredAgentIdentity',
  {
    binding_id: text('binding_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    agent_id: text('agent_id'),
    provider: text('provider').notNull(),
    issuer: text('issuer').notNull(),
    tenant_id: text('tenant_id').notNull(),
    client_id: text('client_id').notNull(),
  },
  (table) => [
    uniqueIndex('LiteLLM_RetiredAgentIdentity_provider_tenant_id_client_id_key').on(
      table.provider,
      table.tenant_id,
      table.client_id,
    ),
    foreignKey({
      name: 'LiteLLM_RetiredAgentIdentity_agent_id_fkey',
      columns: [table.agent_id],
      foreignColumns: [liteLLMAgentsTable.agent_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMRetiredAgent = pgTable('LiteLLM_RetiredAgent', {
  original_agent_id: text('original_agent_id').primaryKey(),
  retired_at: timestamp('retired_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const liteLLMVerifiedSubject = pgTable(
  'LiteLLM_VerifiedSubject',
  {
    subject_id: text('subject_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    issuer: text('issuer').notNull(),
    tenant_id: text('tenant_id').notNull(),
    oid: text('oid').notNull(),
    kind: text('kind').notNull().default('human'),
    user_id: text('user_id'),
    verified_via: text('verified_via').notNull().default('sso_interactive'),
    verified_at: timestamp('verified_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('LiteLLM_VerifiedSubject_user_id_idx').on(table.user_id),
    uniqueIndex('LiteLLM_VerifiedSubject_issuer_tenant_id_oid_key').on(
      table.issuer,
      table.tenant_id,
      table.oid,
    ),
    foreignKey({
      name: 'LiteLLM_VerifiedSubject_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [liteLLMUserTable.user_id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const liteLLMOrganizationTable = pgTable(
  'LiteLLM_OrganizationTable',
  {
    organization_id: text('organization_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organization_alias: text('organization_alias').notNull(),
    budget_id: text('budget_id').notNull(),
    metadata: jsonb('metadata').notNull().default({}),
    models: text('models').array(),
    spend: doublePrecision('spend').notNull().default(0),
    model_spend: jsonb('model_spend').notNull().default({}),
    object_permission_id: text('object_permission_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by').notNull(),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'LiteLLM_OrganizationTable_budget_id_fkey',
      columns: [table.budget_id],
      foreignColumns: [liteLLMBudgetTable.budget_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_OrganizationTable_object_permission_id_fkey',
      columns: [table.object_permission_id],
      foreignColumns: [liteLLMObjectPermissionTable.object_permission_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMModelTable = pgTable('LiteLLM_ModelTable', {
  id: serial('id').primaryKey(),
  model_aliases: jsonb('aliases'),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  created_by: text('created_by').notNull(),
  updated_at: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  updated_by: text('updated_by').notNull(),
})

export const liteLLMTeamTable = pgTable(
  'LiteLLM_TeamTable',
  {
    team_id: text('team_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    team_alias: text('team_alias'),
    organization_id: text('organization_id'),
    object_permission_id: text('object_permission_id'),
    admins: text('admins').array(),
    members: text('members').array(),
    members_with_roles: jsonb('members_with_roles').notNull().default({}),
    metadata: jsonb('metadata').notNull().default({}),
    max_budget: doublePrecision('max_budget'),
    soft_budget: doublePrecision('soft_budget'),
    spend: doublePrecision('spend').notNull().default(0),
    models: text('models').array(),
    max_parallel_requests: integer('max_parallel_requests'),
    tpm_limit: bigint('tpm_limit', { mode: 'bigint' }),
    rpm_limit: bigint('rpm_limit', { mode: 'bigint' }),
    tpd_limit: bigint('tpd_limit', { mode: 'bigint' }),
    budget_duration: text('budget_duration'),
    budget_reset_at: timestamp('budget_reset_at', { precision: 3 }),
    blocked: boolean('blocked').notNull().default(false),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    model_spend: jsonb('model_spend').notNull().default({}),
    model_max_budget: jsonb('model_max_budget').notNull().default({}),
    router_settings: jsonb('router_settings').default({}),
    team_member_permissions: text('team_member_permissions').array().default([]),
    access_group_ids: text('access_group_ids').array().default([]),
    policies: text('policies').array().default([]),
    default_team_member_models: text('default_team_member_models').array().default([]),
    budget_limits: jsonb('budget_limits'),
    model_id: integer('model_id'),
    allow_team_guardrail_config: boolean('allow_team_guardrail_config').notNull().default(false),
  },
  (table) => [
    uniqueIndex('LiteLLM_TeamTable_model_id_key').on(table.model_id),
    index('LiteLLM_TeamTable_organization_id_idx').on(table.organization_id),
    index('LiteLLM_TeamTable_team_alias_idx').on(table.team_alias),
    index('LiteLLM_TeamTable_created_at_idx').on(table.created_at),
    foreignKey({
      name: 'LiteLLM_TeamTable_organization_id_fkey',
      columns: [table.organization_id],
      foreignColumns: [liteLLMOrganizationTable.organization_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_TeamTable_model_id_fkey',
      columns: [table.model_id],
      foreignColumns: [liteLLMModelTable.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_TeamTable_object_permission_id_fkey',
      columns: [table.object_permission_id],
      foreignColumns: [liteLLMObjectPermissionTable.object_permission_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMProjectTable = pgTable(
  'LiteLLM_ProjectTable',
  {
    project_id: text('project_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    project_alias: text('project_alias'),
    description: text('description'),
    team_id: text('team_id'),
    budget_id: text('budget_id'),
    metadata: jsonb('metadata').notNull().default({}),
    models: text('models').array(),
    spend: doublePrecision('spend').notNull().default(0),
    model_spend: jsonb('model_spend').notNull().default({}),
    model_rpm_limit: jsonb('model_rpm_limit').notNull().default({}),
    model_tpm_limit: jsonb('model_tpm_limit').notNull().default({}),
    blocked: boolean('blocked').notNull().default(false),
    object_permission_id: text('object_permission_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by').notNull(),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'LiteLLM_ProjectTable_team_id_fkey',
      columns: [table.team_id],
      foreignColumns: [liteLLMTeamTable.team_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_ProjectTable_budget_id_fkey',
      columns: [table.budget_id],
      foreignColumns: [liteLLMBudgetTable.budget_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_ProjectTable_object_permission_id_fkey',
      columns: [table.object_permission_id],
      foreignColumns: [liteLLMObjectPermissionTable.object_permission_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMDeletedTeamTable = pgTable(
  'LiteLLM_DeletedTeamTable',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    team_id: text('team_id').notNull(),
    team_alias: text('team_alias'),
    organization_id: text('organization_id'),
    object_permission_id: text('object_permission_id'),
    admins: text('admins').array(),
    members: text('members').array(),
    members_with_roles: jsonb('members_with_roles').notNull().default({}),
    metadata: jsonb('metadata').notNull().default({}),
    max_budget: doublePrecision('max_budget'),
    soft_budget: doublePrecision('soft_budget'),
    spend: doublePrecision('spend').notNull().default(0),
    models: text('models').array(),
    max_parallel_requests: integer('max_parallel_requests'),
    tpm_limit: bigint('tpm_limit', { mode: 'bigint' }),
    rpm_limit: bigint('rpm_limit', { mode: 'bigint' }),
    tpd_limit: bigint('tpd_limit', { mode: 'bigint' }),
    budget_duration: text('budget_duration'),
    budget_reset_at: timestamp('budget_reset_at', { precision: 3 }),
    blocked: boolean('blocked').notNull().default(false),
    model_spend: jsonb('model_spend').notNull().default({}),
    model_max_budget: jsonb('model_max_budget').notNull().default({}),
    router_settings: jsonb('router_settings').default({}),
    team_member_permissions: text('team_member_permissions').array().default([]),
    access_group_ids: text('access_group_ids').array().default([]),
    policies: text('policies').array().default([]),
    model_id: integer('model_id'),
    allow_team_guardrail_config: boolean('allow_team_guardrail_config').notNull().default(false),
    created_at: timestamp('created_at', { precision: 3 }),
    updated_at: timestamp('updated_at', { precision: 3 }),
    deleted_at: timestamp('deleted_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    deleted_by: text('deleted_by'),
    deleted_by_api_key: text('deleted_by_api_key'),
    litellm_changed_by: text('litellm_changed_by'),
  },
  (table) => [
    index('LiteLLM_DeletedTeamTable_team_id_idx').on(table.team_id),
    index('LiteLLM_DeletedTeamTable_deleted_at_idx').on(table.deleted_at),
    index('LiteLLM_DeletedTeamTable_organization_id_idx').on(table.organization_id),
    index('LiteLLM_DeletedTeamTable_team_alias_idx').on(table.team_alias),
    index('LiteLLM_DeletedTeamTable_created_at_idx').on(table.created_at),
  ],
)

export const liteLLMUserTable = pgTable(
  'LiteLLM_UserTable',
  {
    user_id: text('user_id').primaryKey(),
    user_alias: text('user_alias'),
    team_id: text('team_id'),
    sso_user_id: text('sso_user_id'),
    organization_id: text('organization_id'),
    object_permission_id: text('object_permission_id'),
    password: text('password'),
    password_reset_required: boolean('password_reset_required'),
    last_breach_check_at: timestamp('last_breach_check_at', { precision: 3 }),
    teams: text('teams').array().default([]),
    user_role: text('user_role'),
    max_budget: doublePrecision('max_budget'),
    spend: doublePrecision('spend').notNull().default(0),
    user_email: text('user_email'),
    models: text('models').array(),
    metadata: jsonb('metadata').notNull().default({}),
    max_parallel_requests: integer('max_parallel_requests'),
    tpm_limit: bigint('tpm_limit', { mode: 'bigint' }),
    rpm_limit: bigint('rpm_limit', { mode: 'bigint' }),
    budget_duration: text('budget_duration'),
    budget_reset_at: timestamp('budget_reset_at', { precision: 3 }),
    allowed_cache_controls: text('allowed_cache_controls').array().default([]),
    policies: text('policies').array().default([]),
    model_spend: jsonb('model_spend').notNull().default({}),
    model_max_budget: jsonb('model_max_budget').notNull().default({}),
    created_at: timestamp('created_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('LiteLLM_UserTable_sso_user_id_key').on(table.sso_user_id),
    foreignKey({
      name: 'LiteLLM_UserTable_organization_id_fkey',
      columns: [table.organization_id],
      foreignColumns: [liteLLMOrganizationTable.organization_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_UserTable_object_permission_id_fkey',
      columns: [table.object_permission_id],
      foreignColumns: [liteLLMObjectPermissionTable.object_permission_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMObjectPermissionTable = pgTable('LiteLLM_ObjectPermissionTable', {
  object_permission_id: text('object_permission_id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  mcp_servers: text('mcp_servers').array().default([]),
  mcp_access_groups: text('mcp_access_groups').array().default([]),
  mcp_tool_permissions: jsonb('mcp_tool_permissions'),
  vector_stores: text('vector_stores').array().default([]),
  agents: text('agents').array().default([]),
  agent_access_groups: text('agent_access_groups').array().default([]),
  models: text('models').array().default([]),
  blocked_tools: text('blocked_tools').array().default([]),
  mcp_toolsets: text('mcp_toolsets').array().default([]),
  search_tools: text('search_tools').array().default([]),
  mcp_tool_search_enabled: boolean('mcp_tool_search_enabled'),
  skills: text('skills').array().default([]),
})

export const liteLLMMCPServerTable = pgTable(
  'LiteLLM_MCPServerTable',
  {
    server_id: text('server_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    server_name: text('server_name'),
    alias: text('alias'),
    description: text('description'),
    instructions: text('instructions'),
    url: text('url'),
    spec_path: text('spec_path'),
    transport: text('transport').notNull().default('sse'),
    auth_type: text('auth_type'),
    credentials: jsonb('credentials').default({}),
    created_at: timestamp('created_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
    mcp_info: jsonb('mcp_info').default({}),
    mcp_access_groups: text('mcp_access_groups').array(),
    allowed_tools: text('allowed_tools').array().default([]),
    tool_name_to_display_name: jsonb('tool_name_to_display_name').default({}),
    tool_name_to_description: jsonb('tool_name_to_description').default({}),
    pinned_tools: jsonb('pinned_tools').default({}),
    extra_headers: text('extra_headers').array().default([]),
    static_headers: jsonb('static_headers').default({}),
    env_vars: jsonb('env_vars').default([]),
    status: text('status').default('unknown'),
    last_health_check: timestamp('last_health_check', { precision: 3 }),
    health_check_error: text('health_check_error'),
    command: text('command'),
    args: text('args').array().default([]),
    env: jsonb('env').default({}),
    issuer: text('issuer'),
    authorization_url: text('authorization_url'),
    token_url: text('token_url'),
    registration_url: text('registration_url'),
    oauth2_flow: text('oauth2_flow'),
    token_exchange_endpoint: text('token_exchange_endpoint'),
    audience: text('audience'),
    subject_token_type: text('subject_token_type'),
    token_exchange_profile: text('token_exchange_profile'),
    allow_all_keys: boolean('allow_all_keys').notNull().default(false),
    available_on_public_internet: boolean('available_on_public_internet').notNull().default(true),
    delegate_auth_to_upstream: boolean('delegate_auth_to_upstream').notNull().default(false),
    oauth_passthrough: boolean('oauth_passthrough').notNull().default(false),
    dcr_bridge: boolean('dcr_bridge'),
    per_server_oauth_discovery: boolean('per_server_oauth_discovery').notNull().default(false),
    is_byok: boolean('is_byok').notNull().default(false),
    byok_description: text('byok_description').array().default([]),
    byok_api_key_help_url: text('byok_api_key_help_url'),
    source_url: text('source_url'),
    timeout: doublePrecision('timeout'),
    max_concurrent_requests: integer('max_concurrent_requests'),
    rpm: integer('rpm'),
    approval_status: text('approval_status').default('active'),
    submitted_by: text('submitted_by'),
    submitted_at: timestamp('submitted_at', { precision: 3 }),
    reviewed_at: timestamp('reviewed_at', { precision: 3 }),
    review_notes: text('review_notes'),
  },
  (table) => [index('LiteLLM_MCPServerTable_approval_status_idx').on(table.approval_status)],
)

export const liteLLMMCPToolsetTable = pgTable(
  'LiteLLM_MCPToolsetTable',
  {
    toolset_id: text('toolset_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    toolset_name: text('toolset_name').notNull(),
    description: text('description'),
    tools: jsonb('tools').notNull().default([]),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [uniqueIndex('LiteLLM_MCPToolsetTable_toolset_name_key').on(table.toolset_name)],
)

export const liteLLMMCPUserCredentials = pgTable(
  'LiteLLM_MCPUserCredentials',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    user_id: text('user_id').notNull(),
    server_id: text('server_id').notNull(),
    credential_b64: text('credential_b64').notNull(),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('LiteLLM_MCPUserCredentials_user_id_server_id_key').on(
      table.user_id,
      table.server_id,
    ),
  ],
)

export const liteLLMUserProviderCredentials = pgTable(
  'LiteLLM_UserProviderCredentials',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    user_id: text('user_id').notNull(),
    credential_name: text('credential_name').notNull(),
    provider: text('provider').notNull(),
    credential_b64: text('credential_b64').notNull(),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_UserProviderCredentials_credential_name_idx').on(table.credential_name),
    uniqueIndex('LiteLLM_UserProviderCredentials_user_id_credential_name_key').on(
      table.user_id,
      table.credential_name,
    ),
  ],
)

export const liteLLMMCPUserEnvVars = pgTable(
  'LiteLLM_MCPUserEnvVars',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    user_id: text('user_id').notNull(),
    server_id: text('server_id').notNull(),
    values_b64: text('values_b64').notNull(),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_MCPUserEnvVars_user_id_idx').on(table.user_id),
    index('LiteLLM_MCPUserEnvVars_server_id_idx').on(table.server_id),
    uniqueIndex('LiteLLM_MCPUserEnvVars_user_id_server_id_key').on(table.user_id, table.server_id),
  ],
)

export const liteLLMMCPServerOAuthClient = pgTable('LiteLLM_MCPServerOAuthClient', {
  server_id: text('server_id').primaryKey(),
  credentials: jsonb('credentials'),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updated_at: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const liteLLMSSOIdentityAssertion = pgTable('LiteLLM_SSOIdentityAssertion', {
  user_id: text('user_id').primaryKey(),
  assertion_b64: text('assertion_b64').notNull(),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updated_at: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
})

export const liteLLMVerificationToken = pgTable(
  'LiteLLM_VerificationToken',
  {
    token: text('token').primaryKey(),
    key_name: text('key_name'),
    key_alias: text('key_alias'),
    soft_budget_cooldown: boolean('soft_budget_cooldown').notNull().default(false),
    spend: doublePrecision('spend').notNull().default(0),
    total_spend: doublePrecision('total_spend').notNull().default(0),
    expires: timestamp('expires', { precision: 3 }),
    models: text('models').array(),
    aliases: jsonb('aliases').notNull().default({}),
    config: jsonb('config').notNull().default({}),
    router_settings: jsonb('router_settings').default({}),
    user_id: text('user_id'),
    team_id: text('team_id'),
    agent_id: text('agent_id'),
    project_id: text('project_id'),
    permissions: jsonb('permissions').notNull().default({}),
    max_parallel_requests: integer('max_parallel_requests'),
    metadata: jsonb('metadata').notNull().default({}),
    blocked: boolean('blocked'),
    tpm_limit: bigint('tpm_limit', { mode: 'bigint' }),
    rpm_limit: bigint('rpm_limit', { mode: 'bigint' }),
    tpd_limit: bigint('tpd_limit', { mode: 'bigint' }),
    max_budget: doublePrecision('max_budget'),
    budget_duration: text('budget_duration'),
    budget_reset_at: timestamp('budget_reset_at', { precision: 3 }),
    allowed_cache_controls: text('allowed_cache_controls').array().default([]),
    allowed_routes: text('allowed_routes').array().default([]),
    key_type: text('key_type'),
    policies: text('policies').array().default([]),
    access_group_ids: text('access_group_ids').array().default([]),
    model_spend: jsonb('model_spend').notNull().default({}),
    model_max_budget: jsonb('model_max_budget').notNull().default({}),
    budget_fallbacks: jsonb('budget_fallbacks').notNull().default({}),
    budget_id: text('budget_id'),
    organization_id: text('organization_id'),
    object_permission_id: text('object_permission_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
    settings_updated_at: timestamp('settings_updated_at', { precision: 3 }),
    last_active: timestamp('last_active', { precision: 3 }),
    rotation_count: integer('rotation_count').default(0),
    auto_rotate: boolean('auto_rotate').default(false),
    rotation_interval: text('rotation_interval'),
    last_rotation_at: timestamp('last_rotation_at', { precision: 3 }),
    key_rotation_at: timestamp('key_rotation_at', { precision: 3 }),
    budget_limits: jsonb('budget_limits'),
  },
  (table) => [
    index('LiteLLM_VerificationToken_user_id_team_id_idx').on(table.user_id, table.team_id),
    index('LiteLLM_VerificationToken_team_id_idx').on(table.team_id),
    index('LiteLLM_VerificationToken_budget_reset_at_expires_idx').on(
      table.budget_reset_at,
      table.expires,
    ),
    foreignKey({
      name: 'LiteLLM_VerificationToken_budget_id_fkey',
      columns: [table.budget_id],
      foreignColumns: [liteLLMBudgetTable.budget_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_VerificationToken_organization_id_fkey',
      columns: [table.organization_id],
      foreignColumns: [liteLLMOrganizationTable.organization_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_VerificationToken_project_id_fkey',
      columns: [table.project_id],
      foreignColumns: [liteLLMProjectTable.project_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_VerificationToken_object_permission_id_fkey',
      columns: [table.object_permission_id],
      foreignColumns: [liteLLMObjectPermissionTable.object_permission_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMJWTKeyMapping = pgTable(
  'LiteLLM_JWTKeyMapping',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    jwt_issuer: text('jwt_issuer').notNull().default(''),
    jwt_claim_name: text('jwt_claim_name').notNull(),
    jwt_claim_value: text('jwt_claim_value').notNull(),
    token: text('token').notNull(),
    description: text('description'),
    is_active: boolean('is_active').notNull().default(true),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    index('LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_v_idx').on(
      table.jwt_issuer,
      table.jwt_claim_name,
      table.jwt_claim_value,
      table.is_active,
    ),
    uniqueIndex('LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_v_key').on(
      table.jwt_issuer,
      table.jwt_claim_name,
      table.jwt_claim_value,
    ),
    foreignKey({
      name: 'LiteLLM_JWTKeyMapping_token_fkey',
      columns: [table.token],
      foreignColumns: [liteLLMVerificationToken.token],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const liteLLMDeprecatedVerificationToken = pgTable(
  'LiteLLM_DeprecatedVerificationToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    token: text('token').notNull(),
    active_token_id: text('active_token_id').notNull(),
    revoke_at: timestamp('revoke_at', { precision: 3 }).notNull(),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('LiteLLM_DeprecatedVerificationToken_token_revoke_at_idx').on(
      table.token,
      table.revoke_at,
    ),
    index('LiteLLM_DeprecatedVerificationToken_revoke_at_idx').on(table.revoke_at),
    uniqueIndex('LiteLLM_DeprecatedVerificationToken_token_key').on(table.token),
  ],
)

export const liteLLMDeletedVerificationToken = pgTable(
  'LiteLLM_DeletedVerificationToken',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    token: text('token').notNull(),
    key_name: text('key_name'),
    key_alias: text('key_alias'),
    soft_budget_cooldown: boolean('soft_budget_cooldown').notNull().default(false),
    spend: doublePrecision('spend').notNull().default(0),
    total_spend: doublePrecision('total_spend').notNull().default(0),
    expires: timestamp('expires', { precision: 3 }),
    models: text('models').array(),
    aliases: jsonb('aliases').notNull().default({}),
    config: jsonb('config').notNull().default({}),
    user_id: text('user_id'),
    team_id: text('team_id'),
    agent_id: text('agent_id'),
    project_id: text('project_id'),
    permissions: jsonb('permissions').notNull().default({}),
    max_parallel_requests: integer('max_parallel_requests'),
    metadata: jsonb('metadata').notNull().default({}),
    blocked: boolean('blocked'),
    tpm_limit: bigint('tpm_limit', { mode: 'bigint' }),
    rpm_limit: bigint('rpm_limit', { mode: 'bigint' }),
    tpd_limit: bigint('tpd_limit', { mode: 'bigint' }),
    max_budget: doublePrecision('max_budget'),
    budget_duration: text('budget_duration'),
    budget_reset_at: timestamp('budget_reset_at', { precision: 3 }),
    allowed_cache_controls: text('allowed_cache_controls').array().default([]),
    allowed_routes: text('allowed_routes').array().default([]),
    key_type: text('key_type'),
    policies: text('policies').array().default([]),
    access_group_ids: text('access_group_ids').array().default([]),
    model_spend: jsonb('model_spend').notNull().default({}),
    model_max_budget: jsonb('model_max_budget').notNull().default({}),
    budget_fallbacks: jsonb('budget_fallbacks').notNull().default({}),
    router_settings: jsonb('router_settings').default({}),
    budget_id: text('budget_id'),
    organization_id: text('organization_id'),
    object_permission_id: text('object_permission_id'),
    created_at: timestamp('created_at', { precision: 3 }),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 }),
    updated_by: text('updated_by'),
    settings_updated_at: timestamp('settings_updated_at', { precision: 3 }),
    last_active: timestamp('last_active', { precision: 3 }),
    rotation_count: integer('rotation_count').default(0),
    auto_rotate: boolean('auto_rotate').default(false),
    rotation_interval: text('rotation_interval'),
    last_rotation_at: timestamp('last_rotation_at', { precision: 3 }),
    key_rotation_at: timestamp('key_rotation_at', { precision: 3 }),
    deleted_at: timestamp('deleted_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    deleted_by: text('deleted_by'),
    deleted_by_api_key: text('deleted_by_api_key'),
    litellm_changed_by: text('litellm_changed_by'),
  },
  (table) => [
    index('LiteLLM_DeletedVerificationToken_token_idx').on(table.token),
    index('LiteLLM_DeletedVerificationToken_deleted_at_idx').on(table.deleted_at),
    index('LiteLLM_DeletedVerificationToken_user_id_idx').on(table.user_id),
    index('LiteLLM_DeletedVerificationToken_team_id_idx').on(table.team_id),
    index('LiteLLM_DeletedVerificationToken_organization_id_idx').on(table.organization_id),
    index('LiteLLM_DeletedVerificationToken_key_alias_idx').on(table.key_alias),
    index('LiteLLM_DeletedVerificationToken_created_at_idx').on(table.created_at),
  ],
)

export const liteLLMEndUserTable = pgTable(
  'LiteLLM_EndUserTable',
  {
    user_id: text('user_id').primaryKey(),
    alias: text('alias'),
    spend: doublePrecision('spend').notNull().default(0),
    allowed_model_region: text('allowed_model_region'),
    default_model: text('default_model'),
    models: text('models').array().default([]),
    budget_id: text('budget_id'),
    object_permission_id: text('object_permission_id'),
    blocked: boolean('blocked').notNull().default(false),
  },
  (table) => [
    foreignKey({
      name: 'LiteLLM_EndUserTable_budget_id_fkey',
      columns: [table.budget_id],
      foreignColumns: [liteLLMBudgetTable.budget_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_EndUserTable_object_permission_id_fkey',
      columns: [table.object_permission_id],
      foreignColumns: [liteLLMObjectPermissionTable.object_permission_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMModelAccessGroupBudgetTable = pgTable(
  'LiteLLM_ModelAccessGroupBudgetTable',
  {
    access_group_name: text('access_group_name').primaryKey(),
    spend: doublePrecision('spend').notNull().default(0),
    budget_id: text('budget_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    foreignKey({
      name: 'LiteLLM_ModelAccessGroupBudgetTable_budget_id_fkey',
      columns: [table.budget_id],
      foreignColumns: [liteLLMBudgetTable.budget_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMTagTable = pgTable(
  'LiteLLM_TagTable',
  {
    tag_name: text('tag_name').primaryKey(),
    description: text('description'),
    models: text('models').array(),
    model_info: jsonb('model_info'),
    spend: doublePrecision('spend').notNull().default(0),
    budget_id: text('budget_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    foreignKey({
      name: 'LiteLLM_TagTable_budget_id_fkey',
      columns: [table.budget_id],
      foreignColumns: [liteLLMBudgetTable.budget_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMConfig = pgTable('LiteLLM_Config', {
  param_name: text('param_name').primaryKey(),
  param_value: jsonb('param_value'),
  last_run_at: timestamp('last_run_at', { precision: 3 }),
  reload_revision: bigint('reload_revision', { mode: 'bigint' })
    .notNull()
    .default(sql`0`),
})

export const liteLLMSpendLogs = pgTable(
  'LiteLLM_SpendLogs',
  {
    request_id: text('request_id').primaryKey(),
    call_type: text('call_type').notNull(),
    api_key: text('api_key').notNull().default(''),
    spend: doublePrecision('spend').notNull().default(0),
    total_tokens: integer('total_tokens').notNull().default(0),
    prompt_tokens: integer('prompt_tokens').notNull().default(0),
    completion_tokens: integer('completion_tokens').notNull().default(0),
    startTime: timestamp('startTime', { precision: 3 }).notNull(),
    endTime: timestamp('endTime', { precision: 3 }).notNull(),
    request_duration_ms: integer('request_duration_ms'),
    completionStartTime: timestamp('completionStartTime', { precision: 3 }),
    model: text('model').notNull().default(''),
    model_id: text('model_id').default(''),
    model_group: text('model_group').default(''),
    custom_llm_provider: text('custom_llm_provider').default(''),
    api_base: text('api_base').default(''),
    user: text('user').default(''),
    metadata: jsonb('metadata').default({}),
    cache_hit: text('cache_hit').default(''),
    cache_key: text('cache_key').default(''),
    request_tags: jsonb('request_tags').default([]),
    team_id: text('team_id'),
    organization_id: text('organization_id'),
    end_user: text('end_user'),
    requester_ip_address: text('requester_ip_address'),
    messages: jsonb('messages').default({}),
    response: jsonb('response').default({}),
    session_id: text('session_id'),
    status: text('status'),
    mcp_namespaced_tool_name: text('mcp_namespaced_tool_name'),
    billing_agent_id: text('billing_agent_id'),
    agent_id: text('agent_id'),
    proxy_server_request: jsonb('proxy_server_request').default({}),
    litellm_call_id: text('litellm_call_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_SpendLogs_startTime_idx').on(table.startTime),
    index('LiteLLM_SpendLogs_startTime_request_id_idx').on(table.startTime, table.request_id),
    index('LiteLLM_SpendLogs_end_user_idx').on(table.end_user),
    index('LiteLLM_SpendLogs_session_id_idx').on(table.session_id),
    index('LiteLLM_SpendLogs_litellm_call_id_idx').on(table.litellm_call_id),
    index('LiteLLM_SpendLogs_api_key_startTime_idx').on(table.api_key, table.startTime),
  ],
)

export const liteLLMBudgetWindowSpend = pgTable(
  'LiteLLM_BudgetWindowSpend',
  {
    entity_type: text('entity_type').notNull(),
    entity_id: text('entity_id').notNull(),
    window_duration: text('window_duration').notNull(),
    window_start: timestamp('window_start', { precision: 3 }).notNull(),
    spend: doublePrecision('spend').notNull().default(0),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_BudgetWindowSpend_pkey',
      columns: [table.entity_type, table.entity_id, table.window_duration],
    }),
  ],
)

export const liteLLMErrorLogs = pgTable('LiteLLM_ErrorLogs', {
  request_id: text('request_id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  startTime: timestamp('startTime', { precision: 3 }).notNull(),
  endTime: timestamp('endTime', { precision: 3 }).notNull(),
  api_base: text('api_base').notNull().default(''),
  model_group: text('model_group').notNull().default(''),
  litellm_model_name: text('litellm_model_name').notNull().default(''),
  model_id: text('model_id').notNull().default(''),
  request_kwargs: jsonb('request_kwargs').notNull().default({}),
  exception_type: text('exception_type').notNull().default(''),
  exception_string: text('exception_string').notNull().default(''),
  status_code: text('status_code').notNull().default(''),
})

export const liteLLMUserNotifications = pgTable('LiteLLM_UserNotifications', {
  request_id: text('request_id').primaryKey(),
  user_id: text('user_id').notNull(),
  models: text('models').array(),
  justification: text('justification').notNull(),
  status: text('status').notNull(),
})

export const liteLLMTeamMembership = pgTable(
  'LiteLLM_TeamMembership',
  {
    user_id: text('user_id').notNull(),
    team_id: text('team_id').notNull(),
    spend: doublePrecision('spend').notNull().default(0),
    total_spend: doublePrecision('total_spend').notNull().default(0),
    budget_id: text('budget_id'),
  },
  (table) => [
    primaryKey({ name: 'LiteLLM_TeamMembership_pkey', columns: [table.user_id, table.team_id] }),
    foreignKey({
      name: 'LiteLLM_TeamMembership_budget_id_fkey',
      columns: [table.budget_id],
      foreignColumns: [liteLLMBudgetTable.budget_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMOrganizationMembership = pgTable(
  'LiteLLM_OrganizationMembership',
  {
    user_id: text('user_id').notNull(),
    organization_id: text('organization_id').notNull(),
    user_role: text('user_role'),
    spend: doublePrecision('spend').default(0),
    budget_id: text('budget_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_OrganizationMembership_pkey',
      columns: [table.user_id, table.organization_id],
    }),
    uniqueIndex('LiteLLM_OrganizationMembership_user_id_organization_id_key').on(
      table.user_id,
      table.organization_id,
    ),
    foreignKey({
      name: 'LiteLLM_OrganizationMembership_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [liteLLMUserTable.user_id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_OrganizationMembership_organization_id_fkey',
      columns: [table.organization_id],
      foreignColumns: [liteLLMOrganizationTable.organization_id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_OrganizationMembership_budget_id_fkey',
      columns: [table.budget_id],
      foreignColumns: [liteLLMBudgetTable.budget_id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const liteLLMInvitationLink = pgTable(
  'LiteLLM_InvitationLink',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    user_id: text('user_id').notNull(),
    is_accepted: boolean('is_accepted').notNull().default(false),
    accepted_at: timestamp('accepted_at', { precision: 3 }),
    expires_at: timestamp('expires_at', { precision: 3 }).notNull(),
    created_at: timestamp('created_at', { precision: 3 }).notNull(),
    created_by: text('created_by').notNull(),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull(),
    updated_by: text('updated_by').notNull(),
  },
  (table) => [
    foreignKey({
      name: 'LiteLLM_InvitationLink_user_id_fkey',
      columns: [table.user_id],
      foreignColumns: [liteLLMUserTable.user_id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_InvitationLink_created_by_fkey',
      columns: [table.created_by],
      foreignColumns: [liteLLMUserTable.user_id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
    foreignKey({
      name: 'LiteLLM_InvitationLink_updated_by_fkey',
      columns: [table.updated_by],
      foreignColumns: [liteLLMUserTable.user_id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const liteLLMAuditLog = pgTable('LiteLLM_AuditLog', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  updated_at: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  changed_by: text('changed_by').notNull().default(''),
  changed_by_api_key: text('changed_by_api_key').notNull().default(''),
  action: text('action').notNull(),
  table_name: text('table_name').notNull(),
  object_id: text('object_id').notNull(),
  before_value: jsonb('before_value'),
  updated_values: jsonb('updated_values'),
})

export const liteLLMDailyUserSpend = pgTable(
  'LiteLLM_DailyUserSpend',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    user_id: text('user_id'),
    date: text('date').notNull(),
    api_key: text('api_key').notNull(),
    model: text('model'),
    model_group: text('model_group'),
    custom_llm_provider: text('custom_llm_provider'),
    mcp_namespaced_tool_name: text('mcp_namespaced_tool_name'),
    endpoint: text('endpoint'),
    prompt_tokens: bigint('prompt_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    completion_tokens: bigint('completion_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_read_input_tokens: bigint('cache_read_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_creation_input_tokens: bigint('cache_creation_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_saved_tokens: bigint('compression_saved_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_savings_spend: doublePrecision('compression_savings_spend').notNull().default(0),
    prompt_caching_savings_spend: doublePrecision('prompt_caching_savings_spend')
      .notNull()
      .default(0),
    gateway_injected_caching_savings_spend: doublePrecision(
      'gateway_injected_caching_savings_spend',
    )
      .notNull()
      .default(0),
    autorouter_savings_spend: doublePrecision('autorouter_savings_spend').notNull().default(0),
    guardrail_spend: doublePrecision('guardrail_spend').notNull().default(0),
    spend: doublePrecision('spend').notNull().default(0),
    api_requests: bigint('api_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    total_response_time_ms: bigint('total_response_time_ms', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    timed_requests: bigint('timed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_DailyUserSpend_date_idx').on(table.date),
    index('LiteLLM_DailyUserSpend_user_id_date_idx').on(table.user_id, table.date),
    index('LiteLLM_DailyUserSpend_api_key_idx').on(table.api_key),
    index('LiteLLM_DailyUserSpend_model_idx').on(table.model),
    index('LiteLLM_DailyUserSpend_mcp_namespaced_tool_name_idx').on(table.mcp_namespaced_tool_name),
    index('LiteLLM_DailyUserSpend_endpoint_idx').on(table.endpoint),
    uniqueIndex('LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_ll_key').on(
      table.user_id,
      table.date,
      table.api_key,
      table.model,
      table.custom_llm_provider,
      table.mcp_namespaced_tool_name,
      table.endpoint,
    ),
  ],
)

export const liteLLMDailyGlobalSpend = pgTable(
  'LiteLLM_DailyGlobalSpend',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    date: text('date').notNull(),
    model: text('model'),
    model_group: text('model_group'),
    custom_llm_provider: text('custom_llm_provider'),
    mcp_namespaced_tool_name: text('mcp_namespaced_tool_name'),
    endpoint: text('endpoint'),
    prompt_tokens: bigint('prompt_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    completion_tokens: bigint('completion_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_read_input_tokens: bigint('cache_read_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_creation_input_tokens: bigint('cache_creation_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_saved_tokens: bigint('compression_saved_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_savings_spend: doublePrecision('compression_savings_spend').notNull().default(0),
    prompt_caching_savings_spend: doublePrecision('prompt_caching_savings_spend')
      .notNull()
      .default(0),
    gateway_injected_caching_savings_spend: doublePrecision(
      'gateway_injected_caching_savings_spend',
    )
      .notNull()
      .default(0),
    autorouter_savings_spend: doublePrecision('autorouter_savings_spend').notNull().default(0),
    guardrail_spend: doublePrecision('guardrail_spend').notNull().default(0),
    spend: doublePrecision('spend').notNull().default(0),
    api_requests: bigint('api_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    total_response_time_ms: bigint('total_response_time_ms', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    timed_requests: bigint('timed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_DailyGlobalSpend_date_idx').on(table.date),
    uniqueIndex('LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm__key').on(
      table.date,
      table.model,
      table.model_group,
      table.custom_llm_provider,
      table.mcp_namespaced_tool_name,
      table.endpoint,
    ),
  ],
)

export const liteLLMDailyOrganizationSpend = pgTable(
  'LiteLLM_DailyOrganizationSpend',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    organization_id: text('organization_id'),
    date: text('date').notNull(),
    api_key: text('api_key').notNull(),
    model: text('model'),
    model_group: text('model_group'),
    custom_llm_provider: text('custom_llm_provider'),
    mcp_namespaced_tool_name: text('mcp_namespaced_tool_name'),
    endpoint: text('endpoint'),
    prompt_tokens: bigint('prompt_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    completion_tokens: bigint('completion_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_read_input_tokens: bigint('cache_read_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_creation_input_tokens: bigint('cache_creation_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_saved_tokens: bigint('compression_saved_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_savings_spend: doublePrecision('compression_savings_spend').notNull().default(0),
    prompt_caching_savings_spend: doublePrecision('prompt_caching_savings_spend')
      .notNull()
      .default(0),
    gateway_injected_caching_savings_spend: doublePrecision(
      'gateway_injected_caching_savings_spend',
    )
      .notNull()
      .default(0),
    autorouter_savings_spend: doublePrecision('autorouter_savings_spend').notNull().default(0),
    guardrail_spend: doublePrecision('guardrail_spend').notNull().default(0),
    spend: doublePrecision('spend').notNull().default(0),
    api_requests: bigint('api_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    total_response_time_ms: bigint('total_response_time_ms', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    timed_requests: bigint('timed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_DailyOrganizationSpend_date_idx').on(table.date),
    index('LiteLLM_DailyOrganizationSpend_organization_id_date_idx').on(
      table.organization_id,
      table.date,
    ),
    index('LiteLLM_DailyOrganizationSpend_api_key_idx').on(table.api_key),
    index('LiteLLM_DailyOrganizationSpend_model_idx').on(table.model),
    index('LiteLLM_DailyOrganizationSpend_mcp_namespaced_tool_name_idx').on(
      table.mcp_namespaced_tool_name,
    ),
    index('LiteLLM_DailyOrganizationSpend_endpoint_idx').on(table.endpoint),
    uniqueIndex('LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_key').on(
      table.organization_id,
      table.date,
      table.api_key,
      table.model,
      table.custom_llm_provider,
      table.mcp_namespaced_tool_name,
      table.endpoint,
    ),
  ],
)

export const liteLLMDailyEndUserSpend = pgTable(
  'LiteLLM_DailyEndUserSpend',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    end_user_id: text('end_user_id'),
    date: text('date').notNull(),
    api_key: text('api_key').notNull(),
    model: text('model'),
    model_group: text('model_group'),
    custom_llm_provider: text('custom_llm_provider'),
    mcp_namespaced_tool_name: text('mcp_namespaced_tool_name'),
    endpoint: text('endpoint'),
    prompt_tokens: bigint('prompt_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    completion_tokens: bigint('completion_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_read_input_tokens: bigint('cache_read_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_creation_input_tokens: bigint('cache_creation_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_saved_tokens: bigint('compression_saved_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_savings_spend: doublePrecision('compression_savings_spend').notNull().default(0),
    prompt_caching_savings_spend: doublePrecision('prompt_caching_savings_spend')
      .notNull()
      .default(0),
    gateway_injected_caching_savings_spend: doublePrecision(
      'gateway_injected_caching_savings_spend',
    )
      .notNull()
      .default(0),
    autorouter_savings_spend: doublePrecision('autorouter_savings_spend').notNull().default(0),
    guardrail_spend: doublePrecision('guardrail_spend').notNull().default(0),
    spend: doublePrecision('spend').notNull().default(0),
    api_requests: bigint('api_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    total_response_time_ms: bigint('total_response_time_ms', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    timed_requests: bigint('timed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_DailyEndUserSpend_date_idx').on(table.date),
    index('LiteLLM_DailyEndUserSpend_end_user_id_date_idx').on(table.end_user_id, table.date),
    index('LiteLLM_DailyEndUserSpend_api_key_idx').on(table.api_key),
    index('LiteLLM_DailyEndUserSpend_model_idx').on(table.model),
    index('LiteLLM_DailyEndUserSpend_mcp_namespaced_tool_name_idx').on(
      table.mcp_namespaced_tool_name,
    ),
    index('LiteLLM_DailyEndUserSpend_endpoint_idx').on(table.endpoint),
    uniqueIndex('LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_cu_key').on(
      table.end_user_id,
      table.date,
      table.api_key,
      table.model,
      table.custom_llm_provider,
      table.mcp_namespaced_tool_name,
      table.endpoint,
    ),
  ],
)

export const liteLLMDailyAgentSpend = pgTable(
  'LiteLLM_DailyAgentSpend',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    agent_id: text('agent_id'),
    date: text('date').notNull(),
    api_key: text('api_key').notNull(),
    model: text('model'),
    model_group: text('model_group'),
    custom_llm_provider: text('custom_llm_provider'),
    mcp_namespaced_tool_name: text('mcp_namespaced_tool_name'),
    endpoint: text('endpoint'),
    prompt_tokens: bigint('prompt_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    completion_tokens: bigint('completion_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_read_input_tokens: bigint('cache_read_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_creation_input_tokens: bigint('cache_creation_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_saved_tokens: bigint('compression_saved_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_savings_spend: doublePrecision('compression_savings_spend').notNull().default(0),
    prompt_caching_savings_spend: doublePrecision('prompt_caching_savings_spend')
      .notNull()
      .default(0),
    gateway_injected_caching_savings_spend: doublePrecision(
      'gateway_injected_caching_savings_spend',
    )
      .notNull()
      .default(0),
    autorouter_savings_spend: doublePrecision('autorouter_savings_spend').notNull().default(0),
    guardrail_spend: doublePrecision('guardrail_spend').notNull().default(0),
    spend: doublePrecision('spend').notNull().default(0),
    api_requests: bigint('api_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    total_response_time_ms: bigint('total_response_time_ms', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    timed_requests: bigint('timed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_DailyAgentSpend_date_idx').on(table.date),
    index('LiteLLM_DailyAgentSpend_agent_id_date_idx').on(table.agent_id, table.date),
    index('LiteLLM_DailyAgentSpend_api_key_idx').on(table.api_key),
    index('LiteLLM_DailyAgentSpend_model_idx').on(table.model),
    index('LiteLLM_DailyAgentSpend_mcp_namespaced_tool_name_idx').on(
      table.mcp_namespaced_tool_name,
    ),
    index('LiteLLM_DailyAgentSpend_endpoint_idx').on(table.endpoint),
    uniqueIndex('LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom__key').on(
      table.agent_id,
      table.date,
      table.api_key,
      table.model,
      table.custom_llm_provider,
      table.mcp_namespaced_tool_name,
      table.endpoint,
    ),
  ],
)

export const liteLLMDailyTeamSpend = pgTable(
  'LiteLLM_DailyTeamSpend',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    team_id: text('team_id'),
    date: text('date').notNull(),
    api_key: text('api_key').notNull(),
    model: text('model'),
    model_group: text('model_group'),
    custom_llm_provider: text('custom_llm_provider'),
    mcp_namespaced_tool_name: text('mcp_namespaced_tool_name'),
    endpoint: text('endpoint'),
    prompt_tokens: bigint('prompt_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    completion_tokens: bigint('completion_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_read_input_tokens: bigint('cache_read_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_creation_input_tokens: bigint('cache_creation_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_saved_tokens: bigint('compression_saved_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_savings_spend: doublePrecision('compression_savings_spend').notNull().default(0),
    prompt_caching_savings_spend: doublePrecision('prompt_caching_savings_spend')
      .notNull()
      .default(0),
    gateway_injected_caching_savings_spend: doublePrecision(
      'gateway_injected_caching_savings_spend',
    )
      .notNull()
      .default(0),
    autorouter_savings_spend: doublePrecision('autorouter_savings_spend').notNull().default(0),
    guardrail_spend: doublePrecision('guardrail_spend').notNull().default(0),
    spend: doublePrecision('spend').notNull().default(0),
    api_requests: bigint('api_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    total_response_time_ms: bigint('total_response_time_ms', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    timed_requests: bigint('timed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    ptu_flat_cost: doublePrecision('ptu_flat_cost').notNull().default(0),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_DailyTeamSpend_date_idx').on(table.date),
    index('LiteLLM_DailyTeamSpend_team_id_date_idx').on(table.team_id, table.date),
    index('LiteLLM_DailyTeamSpend_api_key_idx').on(table.api_key),
    index('LiteLLM_DailyTeamSpend_model_idx').on(table.model),
    index('LiteLLM_DailyTeamSpend_mcp_namespaced_tool_name_idx').on(table.mcp_namespaced_tool_name),
    index('LiteLLM_DailyTeamSpend_endpoint_idx').on(table.endpoint),
    uniqueIndex('LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_ll_key').on(
      table.team_id,
      table.date,
      table.api_key,
      table.model,
      table.custom_llm_provider,
      table.mcp_namespaced_tool_name,
      table.endpoint,
    ),
  ],
)

export const liteLLMDailyTagSpend = pgTable(
  'LiteLLM_DailyTagSpend',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    request_id: text('request_id'),
    tag: text('tag'),
    date: text('date').notNull(),
    api_key: text('api_key').notNull(),
    model: text('model'),
    model_group: text('model_group'),
    custom_llm_provider: text('custom_llm_provider'),
    mcp_namespaced_tool_name: text('mcp_namespaced_tool_name'),
    endpoint: text('endpoint'),
    prompt_tokens: bigint('prompt_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    completion_tokens: bigint('completion_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_read_input_tokens: bigint('cache_read_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cache_creation_input_tokens: bigint('cache_creation_input_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_saved_tokens: bigint('compression_saved_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    compression_savings_spend: doublePrecision('compression_savings_spend').notNull().default(0),
    prompt_caching_savings_spend: doublePrecision('prompt_caching_savings_spend')
      .notNull()
      .default(0),
    gateway_injected_caching_savings_spend: doublePrecision(
      'gateway_injected_caching_savings_spend',
    )
      .notNull()
      .default(0),
    autorouter_savings_spend: doublePrecision('autorouter_savings_spend').notNull().default(0),
    guardrail_spend: doublePrecision('guardrail_spend').notNull().default(0),
    spend: doublePrecision('spend').notNull().default(0),
    api_requests: bigint('api_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    total_response_time_ms: bigint('total_response_time_ms', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    timed_requests: bigint('timed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_DailyTagSpend_date_idx').on(table.date),
    index('LiteLLM_DailyTagSpend_tag_date_idx').on(table.tag, table.date),
    index('LiteLLM_DailyTagSpend_api_key_idx').on(table.api_key),
    index('LiteLLM_DailyTagSpend_model_idx').on(table.model),
    index('LiteLLM_DailyTagSpend_mcp_namespaced_tool_name_idx').on(table.mcp_namespaced_tool_name),
    index('LiteLLM_DailyTagSpend_endpoint_idx').on(table.endpoint),
    uniqueIndex('LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_pro_key').on(
      table.tag,
      table.date,
      table.api_key,
      table.model,
      table.custom_llm_provider,
      table.mcp_namespaced_tool_name,
      table.endpoint,
    ),
  ],
)

export const liteLLMProxyWorkerHeartbeat = pgTable('LiteLLM_ProxyWorkerHeartbeat', {
  worker_id: text('worker_id').primaryKey(),
  hostname: text('hostname').notNull(),
  started_at: timestamp('started_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  last_heartbeat_at: timestamp('last_heartbeat_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const liteLLMCronJob = pgTable('LiteLLM_CronJob', {
  cronjob_id: text('cronjob_id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  pod_id: text('pod_id').notNull(),
  status: jobStatusEnum('status').notNull().default('INACTIVE'),
  last_updated: timestamp('last_updated', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  ttl: timestamp('ttl', { precision: 3 }).notNull(),
})

export const liteLLMManagedFileTable = pgTable(
  'LiteLLM_ManagedFileTable',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    unified_file_id: text('unified_file_id').notNull(),
    file_object: jsonb('file_object'),
    model_mappings: jsonb('model_mappings').notNull(),
    flat_model_file_ids: text('flat_model_file_ids').array().default([]),
    storage_backend: text('storage_backend'),
    storage_url: text('storage_url'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    team_id: text('team_id'),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    uniqueIndex('LiteLLM_ManagedFileTable_unified_file_id_key').on(table.unified_file_id),
    index('LiteLLM_ManagedFileTable_unified_file_id_idx').on(table.unified_file_id),
    index('LiteLLM_ManagedFileTable_flat_model_file_ids_idx').on(table.flat_model_file_ids),
    index('LiteLLM_ManagedFileTable_team_id_created_at_idx').on(table.team_id, table.created_at),
    index('LiteLLM_ManagedFileTable_created_by_created_at_idx').on(
      table.created_by,
      table.created_at,
    ),
  ],
)

export const liteLLMManagedObjectTable = pgTable(
  'LiteLLM_ManagedObjectTable',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    unified_object_id: text('unified_object_id').notNull(),
    model_object_id: text('model_object_id').notNull(),
    file_object: jsonb('file_object').notNull(),
    file_purpose: text('file_purpose').notNull(),
    status: text('status'),
    batch_processed: boolean('batch_processed').notNull().default(false),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    team_id: text('team_id'),
    org_id: text('org_id'),
    api_key: text('api_key'),
    request_tags: jsonb('request_tags').default([]),
    end_user_id: text('end_user_id'),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    uniqueIndex('LiteLLM_ManagedObjectTable_unified_object_id_key').on(table.unified_object_id),
    uniqueIndex('LiteLLM_ManagedObjectTable_model_object_id_key').on(table.model_object_id),
    index('LiteLLM_ManagedObjectTable_unified_object_id_idx').on(table.unified_object_id),
    index('LiteLLM_ManagedObjectTable_model_object_id_idx').on(table.model_object_id),
    index('LiteLLM_ManagedObjectTable_team_id_created_at_idx').on(table.team_id, table.created_at),
  ],
)

export const liteLLMManagedFileContentTable = pgTable('LiteLLM_ManagedFileContentTable', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  content: bytea('content').notNull(),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const liteLLMManagedVectorStoreTable = pgTable(
  'LiteLLM_ManagedVectorStoreTable',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    unified_resource_id: text('unified_resource_id').notNull(),
    resource_object: jsonb('resource_object'),
    model_mappings: jsonb('model_mappings').notNull(),
    flat_model_resource_ids: text('flat_model_resource_ids').array().default([]),
    storage_backend: text('storage_backend'),
    storage_url: text('storage_url'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    team_id: text('team_id'),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    uniqueIndex('LiteLLM_ManagedVectorStoreTable_unified_resource_id_key').on(
      table.unified_resource_id,
    ),
    index('LiteLLM_ManagedVectorStoreTable_unified_resource_id_idx').on(table.unified_resource_id),
    index('LiteLLM_ManagedVectorStoreTable_team_id_created_at_idx').on(
      table.team_id,
      table.created_at,
    ),
  ],
)

export const liteLLMManagedVectorStoresTable = pgTable(
  'LiteLLM_ManagedVectorStoresTable',
  {
    vector_store_id: text('vector_store_id').primaryKey(),
    custom_llm_provider: text('custom_llm_provider').notNull(),
    vector_store_name: text('vector_store_name'),
    vector_store_description: text('vector_store_description'),
    vector_store_metadata: jsonb('vector_store_metadata'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    litellm_credential_name: text('litellm_credential_name'),
    litellm_params: jsonb('litellm_params'),
    team_id: text('team_id'),
    user_id: text('user_id'),
  },
  (table) => [
    index('LiteLLM_ManagedVectorStoresTable_team_id_idx').on(table.team_id),
    index('LiteLLM_ManagedVectorStoresTable_user_id_idx').on(table.user_id),
  ],
)

export const liteLLMGuardrailsTable = pgTable(
  'LiteLLM_GuardrailsTable',
  {
    guardrail_id: text('guardrail_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    guardrail_name: text('guardrail_name').notNull(),
    litellm_params: jsonb('litellm_params').notNull(),
    guardrail_info: jsonb('guardrail_info'),
    team_id: text('team_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    status: text('status').notNull().default('active'),
    submitted_at: timestamp('submitted_at', { precision: 3 }),
    reviewed_at: timestamp('reviewed_at', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('LiteLLM_GuardrailsTable_guardrail_name_key').on(table.guardrail_name),
    index('LiteLLM_GuardrailsTable_status_idx').on(table.status),
  ],
)

export const liteLLMDailyGuardrailMetrics = pgTable(
  'LiteLLM_DailyGuardrailMetrics',
  {
    guardrail_id: text('guardrail_id').notNull(),
    date: text('date').notNull(),
    requests_evaluated: bigint('requests_evaluated', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    passed_count: bigint('passed_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    blocked_count: bigint('blocked_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    flagged_count: bigint('flagged_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    avg_score: doublePrecision('avg_score'),
    avg_latency_ms: doublePrecision('avg_latency_ms'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_DailyGuardrailMetrics_pkey',
      columns: [table.guardrail_id, table.date],
    }),
    index('LiteLLM_DailyGuardrailMetrics_date_idx').on(table.date),
    index('LiteLLM_DailyGuardrailMetrics_guardrail_id_idx').on(table.guardrail_id),
  ],
)

export const liteLLMDailyGuardrailUsageUnits = pgTable(
  'LiteLLM_DailyGuardrailUsageUnits',
  {
    guardrail_id: text('guardrail_id').notNull(),
    date: text('date').notNull(),
    team_id: text('team_id').notNull(),
    api_key: text('api_key').notNull(),
    usage_unit: text('usage_unit').notNull(),
    units: bigint('units', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    cost: doublePrecision('cost'),
    untracked_units: bigint('untracked_units', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_DailyGuardrailUsageUnits_pkey',
      columns: [table.guardrail_id, table.date, table.team_id, table.api_key, table.usage_unit],
    }),
    index('LiteLLM_DailyGuardrailUsageUnits_date_idx').on(table.date),
  ],
)

export const liteLLMDailyPolicyMetrics = pgTable(
  'LiteLLM_DailyPolicyMetrics',
  {
    policy_id: text('policy_id').notNull(),
    date: text('date').notNull(),
    requests_evaluated: bigint('requests_evaluated', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    passed_count: bigint('passed_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    blocked_count: bigint('blocked_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    flagged_count: bigint('flagged_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    avg_score: doublePrecision('avg_score'),
    avg_latency_ms: doublePrecision('avg_latency_ms'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'LiteLLM_DailyPolicyMetrics_pkey', columns: [table.policy_id, table.date] }),
    index('LiteLLM_DailyPolicyMetrics_date_idx').on(table.date),
    index('LiteLLM_DailyPolicyMetrics_policy_id_idx').on(table.policy_id),
  ],
)

export const liteLLMSpendLogGuardrailIndex = pgTable(
  'LiteLLM_SpendLogGuardrailIndex',
  {
    request_id: text('request_id').notNull(),
    guardrail_id: text('guardrail_id').notNull(),
    policy_id: text('policy_id'),
    start_time: timestamp('start_time', { precision: 3 }).notNull(),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_SpendLogGuardrailIndex_pkey',
      columns: [table.request_id, table.guardrail_id],
    }),
    index('LiteLLM_SpendLogGuardrailIndex_guardrail_id_start_time_idx').on(
      table.guardrail_id,
      table.start_time,
    ),
    index('LiteLLM_SpendLogGuardrailIndex_policy_id_start_time_idx').on(
      table.policy_id,
      table.start_time,
    ),
  ],
)

export const liteLLMSpendLogToolIndex = pgTable(
  'LiteLLM_SpendLogToolIndex',
  {
    request_id: text('request_id').notNull(),
    tool_name: text('tool_name').notNull(),
    start_time: timestamp('start_time', { precision: 3 }).notNull(),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_SpendLogToolIndex_pkey',
      columns: [table.request_id, table.tool_name],
    }),
    index('LiteLLM_SpendLogToolIndex_tool_name_start_time_idx').on(
      table.tool_name,
      table.start_time,
    ),
    index('LiteLLM_SpendLogToolIndex_start_time_idx').on(table.start_time),
  ],
)

export const liteLLMDailyToolSpend = pgTable(
  'LiteLLM_DailyToolSpend',
  {
    date: text('date').notNull(),
    tool_name: text('tool_name').notNull(),
    spend: doublePrecision('spend').notNull().default(0),
    total_tokens: bigint('total_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    request_count: bigint('request_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({ name: 'LiteLLM_DailyToolSpend_pkey', columns: [table.date, table.tool_name] }),
  ],
)

export const liteLLMDailyModelUsage = pgTable(
  'LiteLLM_DailyModelUsage',
  {
    date: text('date').notNull(),
    model_group: text('model_group').notNull(),
    model: text('model').notNull(),
    custom_llm_provider: text('custom_llm_provider').notNull(),
    task_type: text('task_type').notNull(),
    spend: doublePrecision('spend').notNull().default(0),
    prompt_tokens: bigint('prompt_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    completion_tokens: bigint('completion_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    request_count: bigint('request_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_DailyModelUsage_pkey',
      columns: [
        table.date,
        table.model_group,
        table.model,
        table.custom_llm_provider,
        table.task_type,
      ],
    }),
    index('LiteLLM_DailyModelUsage_date_idx').on(table.date),
    index('LiteLLM_DailyModelUsage_model_group_idx').on(table.model_group),
  ],
)

export const liteLLMDailyGatewayRequests = pgTable(
  'LiteLLM_DailyGatewayRequests',
  {
    date: text('date').notNull(),
    category: text('category').notNull(),
    route: text('route').notNull(),
    successful_requests: bigint('successful_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_DailyGatewayRequests_pkey',
      columns: [table.date, table.category, table.route],
    }),
    index('LiteLLM_DailyGatewayRequests_date_idx').on(table.date),
  ],
)

export const liteLLMDailyGatewayFailedRequests = pgTable(
  'LiteLLM_DailyGatewayFailedRequests',
  {
    date: text('date').notNull(),
    category: text('category').notNull(),
    route: text('route').notNull(),
    status_code: integer('status_code').notNull(),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_DailyGatewayFailedRequests_pkey',
      columns: [table.date, table.category, table.route, table.status_code],
    }),
    index('LiteLLM_DailyGatewayFailedRequests_date_idx').on(table.date),
  ],
)

export const liteLLMDailyRequestErrors = pgTable(
  'LiteLLM_DailyRequestErrors',
  {
    date: text('date').notNull(),
    api_key: text('api_key').notNull(),
    team_id: text('team_id').notNull().default(''),
    user_id: text('user_id').notNull().default(''),
    model_group: text('model_group').notNull().default(''),
    status_code: integer('status_code').notNull(),
    failed_requests: bigint('failed_requests', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_DailyRequestErrors_pkey',
      columns: [
        table.date,
        table.api_key,
        table.team_id,
        table.user_id,
        table.model_group,
        table.status_code,
      ],
    }),
    index('LiteLLM_DailyRequestErrors_date_idx').on(table.date),
  ],
)

export const liteLLMPromptTable = pgTable(
  'LiteLLM_PromptTable',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    prompt_id: text('prompt_id').notNull(),
    version: integer('version').notNull().default(1),
    environment: text('environment').notNull().default('development'),
    created_by: text('created_by'),
    litellm_params: jsonb('litellm_params').notNull(),
    prompt_info: jsonb('prompt_info'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_PromptTable_prompt_id_environment_idx').on(table.prompt_id, table.environment),
    index('LiteLLM_PromptTable_prompt_id_idx').on(table.prompt_id),
    uniqueIndex('LiteLLM_PromptTable_prompt_id_version_environment_key').on(
      table.prompt_id,
      table.version,
      table.environment,
    ),
  ],
)

export const liteLLMHealthCheckTable = pgTable(
  'LiteLLM_HealthCheckTable',
  {
    health_check_id: text('health_check_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    model_name: text('model_name').notNull(),
    model_id: text('model_id'),
    status: text('status').notNull(),
    healthy_count: integer('healthy_count').notNull().default(0),
    unhealthy_count: integer('unhealthy_count').notNull().default(0),
    error_message: text('error_message'),
    response_time_ms: doublePrecision('response_time_ms'),
    details: jsonb('details'),
    checked_by: text('checked_by'),
    checked_at: timestamp('checked_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('LiteLLM_HealthCheckTable_model_name_idx').on(table.model_name),
    index('LiteLLM_HealthCheckTable_checked_at_idx').on(table.checked_at),
    index('LiteLLM_HealthCheckTable_status_idx').on(table.status),
    index('LiteLLM_HealthCheckTable_model_id_model_name_checked_at_idx').on(
      table.model_id,
      table.model_name,
      table.checked_at,
    ),
  ],
)

export const liteLLMSearchToolsTable = pgTable(
  'LiteLLM_SearchToolsTable',
  {
    search_tool_id: text('search_tool_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    search_tool_name: text('search_tool_name').notNull(),
    litellm_params: jsonb('litellm_params').notNull(),
    search_tool_info: jsonb('search_tool_info'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('LiteLLM_SearchToolsTable_search_tool_name_key').on(table.search_tool_name),
  ],
)

export const liteLLMSSOConfig = pgTable('LiteLLM_SSOConfig', {
  id: text('id').primaryKey().default('sso_config'),
  sso_settings: jsonb('sso_settings').notNull(),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const liteLLMManagedVectorStoreIndexTable = pgTable(
  'LiteLLM_ManagedVectorStoreIndexTable',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    index_name: text('index_name').notNull(),
    litellm_params: jsonb('litellm_params').notNull(),
    index_info: jsonb('index_info'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    uniqueIndex('LiteLLM_ManagedVectorStoreIndexTable_index_name_key').on(table.index_name),
  ],
)

export const liteLLMCacheConfig = pgTable('LiteLLM_CacheConfig', {
  id: text('id').primaryKey().default('cache_config'),
  cache_settings: jsonb('cache_settings').notNull(),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const liteLLMUISettings = pgTable('LiteLLM_UISettings', {
  id: text('id').primaryKey().default('ui_settings'),
  ui_settings: jsonb('ui_settings').notNull(),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const liteLLMConfigOverrides = pgTable('LiteLLM_ConfigOverrides', {
  config_type: text('config_type').primaryKey(),
  config_value: jsonb('config_value').notNull(),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
})

export const liteLLMSkillsTable = pgTable('LiteLLM_SkillsTable', {
  skill_id: text('skill_id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  display_title: text('display_title'),
  description: text('description'),
  instructions: text('instructions'),
  source: text('source').notNull().default('custom'),
  latest_version: text('latest_version'),
  file_content: bytea('file_content'),
  file_name: text('file_name'),
  file_type: text('file_type'),
  metadata: jsonb('metadata').default({}),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  created_by: text('created_by'),
  updated_at: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  updated_by: text('updated_by'),
})

export const liteLLMPolicyTable = pgTable(
  'LiteLLM_PolicyTable',
  {
    policy_id: text('policy_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    policy_name: text('policy_name').notNull(),
    version_number: integer('version_number').notNull().default(1),
    version_status: text('version_status').notNull().default('production'),
    parent_version_id: text('parent_version_id'),
    is_latest: boolean('is_latest').notNull().default(true),
    published_at: timestamp('published_at', { precision: 3 }),
    production_at: timestamp('production_at', { precision: 3 }),
    inherit: text('inherit'),
    description: text('description'),
    guardrails_add: text('guardrails_add').array().default([]),
    guardrails_remove: text('guardrails_remove').array().default([]),
    condition: jsonb('condition').default({}),
    pipeline: jsonb('pipeline'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    index('LiteLLM_PolicyTable_policy_name_version_status_idx').on(
      table.policy_name,
      table.version_status,
    ),
    uniqueIndex('LiteLLM_PolicyTable_policy_name_version_number_key').on(
      table.policy_name,
      table.version_number,
    ),
  ],
)

export const liteLLMPolicyAttachmentTable = pgTable('LiteLLM_PolicyAttachmentTable', {
  attachment_id: text('attachment_id')
    .primaryKey()
    .$defaultFn(() => crypto.randomUUID()),
  policy_name: text('policy_name').notNull(),
  scope: text('scope'),
  teams: text('teams').array().default([]),
  keys: text('keys').array().default([]),
  models: text('models').array().default([]),
  tags: text('tags').array().default([]),
  priority: integer('priority'),
  is_default: boolean('is_default').notNull().default(false),
  created_at: timestamp('created_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  created_by: text('created_by'),
  updated_at: timestamp('updated_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow)
    .$onUpdate(utcNow),
  updated_by: text('updated_by'),
})

export const liteLLMToolTable = pgTable(
  'LiteLLM_ToolTable',
  {
    tool_id: text('tool_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    tool_name: text('tool_name').notNull(),
    origin: text('origin'),
    input_policy: text('input_policy').notNull().default('untrusted'),
    output_policy: text('output_policy').notNull().default('untrusted'),
    call_count: integer('call_count').notNull().default(0),
    assignments: jsonb('assignments').default({}),
    key_hash: text('key_hash'),
    team_id: text('team_id'),
    key_alias: text('key_alias'),
    user_agent: text('user_agent'),
    last_used_at: timestamp('last_used_at', { precision: 3 }),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    uniqueIndex('LiteLLM_ToolTable_tool_name_key').on(table.tool_name),
    index('LiteLLM_ToolTable_input_policy_idx').on(table.input_policy),
    index('LiteLLM_ToolTable_output_policy_idx').on(table.output_policy),
    index('LiteLLM_ToolTable_team_id_idx').on(table.team_id),
  ],
)

export const liteLLMAccessGroupTable = pgTable(
  'LiteLLM_AccessGroupTable',
  {
    access_group_id: text('access_group_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    access_group_name: text('access_group_name').notNull(),
    description: text('description'),
    access_model_names: text('access_model_names').array().default([]),
    access_mcp_server_ids: text('access_mcp_server_ids').array().default([]),
    mcp_tool_permissions: jsonb('mcp_tool_permissions'),
    access_agent_ids: text('access_agent_ids').array().default([]),
    access_skill_names: text('access_skill_names').array().default([]),
    assigned_team_ids: text('assigned_team_ids').array().default([]),
    assigned_key_ids: text('assigned_key_ids').array().default([]),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    uniqueIndex('LiteLLM_AccessGroupTable_access_group_name_key').on(table.access_group_name),
  ],
)

export const liteLLMClaudeCodePluginTable = pgTable(
  'LiteLLM_ClaudeCodePluginTable',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    name: text('name').notNull(),
    version: text('version'),
    description: text('description'),
    manifest_json: text('manifest_json'),
    files_json: text('files_json').default('{}'),
    enabled: boolean('enabled').notNull().default(true),
    created_at: timestamp('created_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 })
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    created_by: text('created_by'),
    anonymous_download_count: bigint('anonymous_download_count', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
  },
  (table) => [unique('LiteLLM_ClaudeCodePluginTable_name_key').on(table.name)],
)

export const liteLLMClaudeCodePluginVersion = pgTable(
  'LiteLLM_ClaudeCodePluginVersion',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    plugin_id: text('plugin_id').notNull(),
    version: text('version'),
    description: text('description'),
    manifest_json: text('manifest_json'),
    published_at: timestamp('published_at', { precision: 3 }),
    published_by: text('published_by'),
    archive_available: boolean('archive_available').notNull().default(false),
    content: bytea('content'),
  },
  (table) => [
    index('LiteLLM_ClaudeCodePluginVersion_plugin_id_published_at_idx').on(
      table.plugin_id,
      table.published_at,
    ),
    uniqueIndex('LiteLLM_ClaudeCodePluginVersion_plugin_id_version_key').on(
      table.plugin_id,
      table.version,
    ),
    foreignKey({
      name: 'LiteLLM_ClaudeCodePluginVersion_plugin_id_fkey',
      columns: [table.plugin_id],
      foreignColumns: [liteLLMClaudeCodePluginTable.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const liteLLMSkillSubmission = pgTable(
  'LiteLLM_SkillSubmission',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    plugin_name: text('plugin_name').notNull(),
    operation: text('operation').notNull(),
    status: text('status').notNull().default('pending_review'),
    proposal_json: text('proposal_json').notNull(),
    archive_content: bytea('archive_content'),
    submitted_by: text('submitted_by').notNull(),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    base_plugin_id: text('base_plugin_id'),
    base_updated_at: timestamp('base_updated_at', { precision: 3 }),
    reviewed_by: text('reviewed_by'),
    reviewed_at: timestamp('reviewed_at', { precision: 3 }),
    rejection_reason: text('rejection_reason'),
    scan_json: text('scan_json'),
  },
  (table) => [
    index('LiteLLM_SkillSubmission_submitted_by_created_at_id_idx').on(
      table.submitted_by,
      table.created_at,
      table.id,
    ),
    index('LiteLLM_SkillSubmission_submitted_by_status_created_at_id_idx').on(
      table.submitted_by,
      table.status,
      table.created_at,
      table.id,
    ),
    index('LiteLLM_SkillSubmission_status_created_at_id_idx').on(
      table.status,
      table.created_at,
      table.id,
    ),
    index('LiteLLM_SkillSubmission_created_at_id_idx').on(table.created_at, table.id),
  ],
)

export const liteLLMClaudeCodePluginDownload = pgTable(
  'LiteLLM_ClaudeCodePluginDownload',
  {
    id: bigserial('id', { mode: 'bigint' }).primaryKey(),
    plugin_id: text('plugin_id').notNull(),
    downloaded_at: timestamp('downloaded_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    version: text('version'),
    digest: text('digest').notNull(),
    authenticated: boolean('authenticated').notNull(),
    user_id: text('user_id'),
    user_email: text('user_email'),
    key_alias: text('key_alias'),
    team_id: text('team_id'),
    team_alias: text('team_alias'),
  },
  (table) => [
    index('LiteLLM_ClaudeCodePluginDownload_plugin_id_id_idx').on(table.plugin_id, table.id),
    foreignKey({
      name: 'LiteLLM_ClaudeCodePluginDownload_plugin_id_fkey',
      columns: [table.plugin_id],
      foreignColumns: [liteLLMClaudeCodePluginTable.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const liteLLMClaudeCodePluginUpload = pgTable(
  'LiteLLM_ClaudeCodePluginUpload',
  { plugin_name: text('plugin_name').primaryKey(), content: bytea('content').notNull() },
  (table) => [
    foreignKey({
      name: 'LiteLLM_ClaudeCodePluginUpload_plugin_name_fkey',
      columns: [table.plugin_name],
      foreignColumns: [liteLLMClaudeCodePluginTable.name],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const liteLLMMemoryTable = pgTable(
  'LiteLLM_MemoryTable',
  {
    memory_id: text('memory_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    key: text('key').notNull(),
    value: text('value').notNull(),
    metadata: jsonb('metadata'),
    user_id: text('user_id'),
    team_id: text('team_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by'),
  },
  (table) => [
    uniqueIndex('LiteLLM_MemoryTable_key_key').on(table.key),
    index('LiteLLM_MemoryTable_user_id_idx').on(table.user_id),
    index('LiteLLM_MemoryTable_team_id_idx').on(table.team_id),
  ],
)

export const liteLLMAdaptiveRouterState = pgTable(
  'LiteLLM_AdaptiveRouterState',
  {
    router_name: text('router_name').notNull(),
    request_type: text('request_type').notNull(),
    model_name: text('model_name').notNull(),
    alpha: doublePrecision('alpha').notNull(),
    beta: doublePrecision('beta').notNull(),
    total_samples: integer('total_samples').notNull().default(0),
    last_updated_at: timestamp('last_updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_AdaptiveRouterState_pkey',
      columns: [table.router_name, table.request_type, table.model_name],
    }),
  ],
)

export const liteLLMAdaptiveRouterSession = pgTable(
  'LiteLLM_AdaptiveRouterSession',
  {
    session_id: text('session_id').notNull(),
    router_name: text('router_name').notNull(),
    model_name: text('model_name').notNull(),
    classified_type: text('classified_type').notNull(),
    misalignment_count: integer('misalignment_count').notNull().default(0),
    stagnation_count: integer('stagnation_count').notNull().default(0),
    disengagement_count: integer('disengagement_count').notNull().default(0),
    satisfaction_count: integer('satisfaction_count').notNull().default(0),
    failure_count: integer('failure_count').notNull().default(0),
    loop_count: integer('loop_count').notNull().default(0),
    exhaustion_count: integer('exhaustion_count').notNull().default(0),
    last_user_content: text('last_user_content'),
    last_assistant_content: text('last_assistant_content'),
    tool_call_history: jsonb('tool_call_history').notNull().default([]),
    pending_tool_calls: jsonb('pending_tool_calls').notNull().default({}),
    turn_count: integer('turn_count').notNull().default(0),
    last_processed_turn: integer('last_processed_turn').notNull().default(-1),
    clean_credit_awarded: boolean('clean_credit_awarded').notNull().default(false),
    terminal_status: integer('terminal_status'),
    last_activity_at: timestamp('last_activity_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_AdaptiveRouterSession_pkey',
      columns: [table.session_id, table.router_name, table.model_name],
    }),
    index('idx_adaptive_router_session_activity').on(table.last_activity_at),
  ],
)

export const liteLLMAutoRouterBaselineComparison = pgTable(
  'LiteLLM_AutoRouterBaselineComparison',
  {
    scope: text('scope').primaryKey(),
    api_key: text('api_key').notNull(),
    session_id: text('session_id').notNull(),
    router_name: text('router_name').notNull(),
    initial_equivalent: boolean('initial_equivalent').notNull(),
    revision: bigint('revision', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    published_revision: bigint('published_revision', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    history: text('history'),
    attempted_at: timestamp('attempted_at', { precision: 3 }),
    retired: boolean('retired').notNull().default(false),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('idx_autorouter_baseline_scope').on(table.api_key, table.session_id, table.router_name),
    index('idx_autorouter_baseline_updated').on(table.updated_at),
  ],
)

export const liteLLMAutoRouterBaselineObservation = pgTable(
  'LiteLLM_AutoRouterBaselineObservation',
  {
    request_id: text('request_id').primaryKey(),
    scope: text('scope').notNull(),
    started_at: doublePrecision('started_at').notNull(),
    revision: bigint('revision', { mode: 'bigint' }).notNull(),
    data: text('data').notNull(),
    publication: text('publication'),
    conflicted: boolean('conflicted').notNull().default(false),
  },
  (table) => [
    index('idx_autorouter_baseline_event_order').on(
      table.scope,
      table.started_at,
      table.request_id,
    ),
    index('idx_autorouter_baseline_event_revision').on(
      table.scope,
      table.revision,
      table.started_at,
    ),
  ],
)

export const liteLLMAutoRouterSession = pgTable(
  'LiteLLM_AutoRouterSession',
  {
    api_key: text('api_key').notNull(),
    session_id: text('session_id').notNull(),
    router_name: text('router_name').notNull(),
    router_type: text('router_type').notNull(),
    first_turn_at: timestamp('first_turn_at', { precision: 3 }).notNull(),
    last_turn_at: timestamp('last_turn_at', { precision: 3 }).notNull(),
    last_model: text('last_model').notNull(),
    models: jsonb('models').notNull().default({}),
    turns: integer('turns').notNull().default(0),
    unordered_turns: integer('unordered_turns').notNull().default(0),
    covered_turns: integer('covered_turns').notNull().default(0),
    cache_hits: integer('cache_hits').notNull().default(0),
    same_model_turns: integer('same_model_turns').notNull().default(0),
    same_model_hits: integer('same_model_hits').notNull().default(0),
    first_visit_turns: integer('first_visit_turns').notNull().default(0),
    first_visit_hits: integer('first_visit_hits').notNull().default(0),
    return_turns: integer('return_turns').notNull().default(0),
    return_hits: integer('return_hits').notNull().default(0),
    return_expired_misses: integer('return_expired_misses').notNull().default(0),
    return_within_ttl_misses: integer('return_within_ttl_misses').notNull().default(0),
    ttl_5m_turns: integer('ttl_5m_turns').notNull().default(0),
    ttl_1h_turns: integer('ttl_1h_turns').notNull().default(0),
    total_tokens: bigint('total_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    spend: doublePrecision('spend').notNull().default(0),
    saved_spend: doublePrecision('saved_spend').notNull().default(0),
    savings_estimated_turns: integer('savings_estimated_turns').notNull().default(0),
    savings_estimated_actual_spend: doublePrecision('savings_estimated_actual_spend')
      .notNull()
      .default(0),
    savings_estimated_saved_spend: doublePrecision('savings_estimated_saved_spend')
      .notNull()
      .default(0),
    savings_estimated_baseline_models: jsonb('savings_estimated_baseline_models')
      .notNull()
      .default({}),
    classifier_cost: doublePrecision('classifier_cost').notNull().default(0),
    classifier_cost_recorded_turns: integer('classifier_cost_recorded_turns').notNull().default(0),
    tier_turns: jsonb('tier_turns').notNull().default({}),
    baseline_models: jsonb('baseline_models').notNull().default({}),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_AutoRouterSession_pkey',
      columns: [table.api_key, table.session_id, table.router_name],
    }),
    index('idx_autorouter_session_last_turn').on(table.last_turn_at),
  ],
)

export const liteLLMAutoRouterUserSession = pgTable(
  'LiteLLM_AutoRouterUserSession',
  {
    user_id: text('user_id').notNull(),
    api_key: text('api_key').notNull(),
    session_id: text('session_id').notNull(),
    router_name: text('router_name').notNull(),
    router_type: text('router_type').notNull(),
    first_turn_at: timestamp('first_turn_at', { precision: 3 }).notNull(),
    last_turn_at: timestamp('last_turn_at', { precision: 3 }).notNull(),
    last_model: text('last_model').notNull(),
    models: jsonb('models').notNull().default({}),
    turns: integer('turns').notNull().default(0),
    unordered_turns: integer('unordered_turns').notNull().default(0),
    covered_turns: integer('covered_turns').notNull().default(0),
    cache_hits: integer('cache_hits').notNull().default(0),
    same_model_turns: integer('same_model_turns').notNull().default(0),
    same_model_hits: integer('same_model_hits').notNull().default(0),
    first_visit_turns: integer('first_visit_turns').notNull().default(0),
    first_visit_hits: integer('first_visit_hits').notNull().default(0),
    return_turns: integer('return_turns').notNull().default(0),
    return_hits: integer('return_hits').notNull().default(0),
    return_expired_misses: integer('return_expired_misses').notNull().default(0),
    return_within_ttl_misses: integer('return_within_ttl_misses').notNull().default(0),
    ttl_5m_turns: integer('ttl_5m_turns').notNull().default(0),
    ttl_1h_turns: integer('ttl_1h_turns').notNull().default(0),
    total_tokens: bigint('total_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    spend: doublePrecision('spend').notNull().default(0),
    saved_spend: doublePrecision('saved_spend').notNull().default(0),
    savings_estimated_turns: integer('savings_estimated_turns').notNull().default(0),
    savings_estimated_actual_spend: doublePrecision('savings_estimated_actual_spend')
      .notNull()
      .default(0),
    savings_estimated_saved_spend: doublePrecision('savings_estimated_saved_spend')
      .notNull()
      .default(0),
    savings_estimated_baseline_models: jsonb('savings_estimated_baseline_models')
      .notNull()
      .default({}),
    classifier_cost: doublePrecision('classifier_cost').notNull().default(0),
    classifier_cost_recorded_turns: integer('classifier_cost_recorded_turns').notNull().default(0),
    tier_turns: jsonb('tier_turns').notNull().default({}),
    baseline_models: jsonb('baseline_models').notNull().default({}),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_AutoRouterUserSession_pkey',
      columns: [table.user_id, table.api_key, table.session_id, table.router_name],
    }),
    index('idx_autorouter_user_session_last_turn').on(table.last_turn_at),
    index('idx_autorouter_user_session_user_last_turn').on(table.user_id, table.last_turn_at),
  ],
)

export const liteLLMAutoRouterDailySpend = pgTable(
  'LiteLLM_AutoRouterDailySpend',
  {
    date: text('date').notNull(),
    api_key: text('api_key').notNull(),
    user_id: text('user_id').notNull(),
    router_name: text('router_name').notNull(),
    router_type: text('router_type').notNull(),
    turns: integer('turns').notNull().default(0),
    total_tokens: bigint('total_tokens', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    token_recorded_turns: integer('token_recorded_turns').notNull().default(0),
    spend: doublePrecision('spend').notNull().default(0),
    saved_spend: doublePrecision('saved_spend').notNull().default(0),
    savings_estimated_turns: integer('savings_estimated_turns').notNull().default(0),
    savings_estimated_actual_spend: doublePrecision('savings_estimated_actual_spend')
      .notNull()
      .default(0),
    savings_estimated_saved_spend: doublePrecision('savings_estimated_saved_spend')
      .notNull()
      .default(0),
    classifier_cost: doublePrecision('classifier_cost').notNull().default(0),
    classifier_cost_recorded_turns: integer('classifier_cost_recorded_turns').notNull().default(0),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_AutoRouterDailySpend_pkey',
      columns: [table.date, table.api_key, table.user_id, table.router_name, table.router_type],
    }),
  ],
)

export const liteLLMShadowEvalJob = pgTable(
  'LiteLLM_ShadowEvalJob',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    group_id: text('group_id').notNull(),
    target_type: text('target_type').notNull().default('key'),
    target_id: text('target_id').notNull(),
    router_name: text('router_name').notNull(),
    router_names: text('router_names').array().default([]),
    models: text('models').array().default([]),
    direction: text('direction').notNull().default('forward'),
    baseline_model: text('baseline_model'),
    judge_model: text('judge_model').notNull(),
    shadow_percentage: doublePrecision('shadow_percentage').notNull(),
    max_turns: integer('max_turns').notNull(),
    max_budget: doublePrecision('max_budget'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by'),
    ends_at: timestamp('ends_at', { precision: 3 }).notNull(),
    stopped_at: timestamp('stopped_at', { precision: 3 }),
    stopped_by: text('stopped_by'),
  },
  (table) => [
    index('LiteLLM_ShadowEvalJob_group_id_idx').on(table.group_id),
    index('LiteLLM_ShadowEvalJob_target_type_target_id_idx').on(table.target_type, table.target_id),
    index('LiteLLM_ShadowEvalJob_created_at_idx').on(table.created_at),
  ],
)

export const liteLLMShadowEvalAttempt = pgTable(
  'LiteLLM_ShadowEvalAttempt',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    job_id: text('job_id').notNull(),
    request_id: text('request_id').notNull(),
    outcome: text('outcome').notNull(),
    router_name: text('router_name'),
    tier: text('tier'),
    real_model: text('real_model'),
    shadow_model: text('shadow_model'),
    confidence: doublePrecision('confidence'),
    judge_cost: doublePrecision('judge_cost').notNull().default(0),
    shadow_cost: doublePrecision('shadow_cost').notNull().default(0),
    real_cost: doublePrecision('real_cost'),
    real_classifier_cost: doublePrecision('real_classifier_cost').notNull().default(0),
    shadow_classifier_cost: doublePrecision('shadow_classifier_cost').notNull().default(0),
    real_cache_hit: boolean('real_cache_hit').notNull().default(false),
    error: text('error'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [index('LiteLLM_ShadowEvalAttempt_job_id_idx').on(table.job_id)],
)

export const liteLLMShadowEvalFunnel = pgTable('LiteLLM_ShadowEvalFunnel', {
  job_id: text('job_id').primaryKey(),
  not_sampled: integer('not_sampled').notNull().default(0),
  unjudgeable: integer('unjudgeable').notNull().default(0),
  shed: integer('shed').notNull().default(0),
  withheld: integer('withheld').notNull().default(0),
})

export const liteLLMWorkflowRun = pgTable(
  'LiteLLM_WorkflowRun',
  {
    run_id: text('run_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    session_id: text('session_id')
      .notNull()
      .$defaultFn(() => crypto.randomUUID()),
    workflow_type: text('workflow_type').notNull(),
    status: text('status').notNull().default('pending'),
    created_by: text('created_by'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updated_at: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
    input: jsonb('input'),
    output: jsonb('output'),
    metadata: jsonb('metadata'),
  },
  (table) => [
    uniqueIndex('LiteLLM_WorkflowRun_session_id_key').on(table.session_id),
    index('LiteLLM_WorkflowRun_workflow_type_status_idx').on(table.workflow_type, table.status),
    index('LiteLLM_WorkflowRun_session_id_idx').on(table.session_id),
    index('LiteLLM_WorkflowRun_created_at_idx').on(table.created_at),
    index('LiteLLM_WorkflowRun_created_by_idx').on(table.created_by),
  ],
)

export const liteLLMWorkflowEvent = pgTable(
  'LiteLLM_WorkflowEvent',
  {
    event_id: text('event_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    run_id: text('run_id').notNull(),
    event_type: text('event_type').notNull(),
    step_name: text('step_name').notNull(),
    sequence_number: integer('sequence_number').notNull(),
    data: jsonb('data'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('LiteLLM_WorkflowEvent_run_id_idx').on(table.run_id),
    uniqueIndex('LiteLLM_WorkflowEvent_run_id_sequence_number_key').on(
      table.run_id,
      table.sequence_number,
    ),
    foreignKey({
      name: 'LiteLLM_WorkflowEvent_run_id_fkey',
      columns: [table.run_id],
      foreignColumns: [liteLLMWorkflowRun.run_id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const liteLLMWorkflowMessage = pgTable(
  'LiteLLM_WorkflowMessage',
  {
    message_id: text('message_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    run_id: text('run_id').notNull(),
    role: text('role').notNull(),
    content: text('content').notNull(),
    sequence_number: integer('sequence_number').notNull(),
    session_id: text('session_id'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('LiteLLM_WorkflowMessage_run_id_idx').on(table.run_id),
    uniqueIndex('LiteLLM_WorkflowMessage_run_id_sequence_number_key').on(
      table.run_id,
      table.sequence_number,
    ),
    foreignKey({
      name: 'LiteLLM_WorkflowMessage_run_id_fkey',
      columns: [table.run_id],
      foreignColumns: [liteLLMWorkflowRun.run_id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const liteLLMBackgroundInteractionSettlement = pgTable(
  'LiteLLM_BackgroundInteractionSettlement',
  {
    interaction_id: text('interaction_id').primaryKey(),
    custom_llm_provider: text('custom_llm_provider').notNull(),
    create_context: jsonb('create_context').notNull(),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    claimed_at: timestamp('claimed_at', { precision: 3 }),
    claimed_by: text('claimed_by'),
    settled_at: timestamp('settled_at', { precision: 3 }),
    outcome: text('outcome'),
  },
  (table) => [
    index('idx_background_interaction_settlement_claimed_at').on(table.claimed_at),
    index('idx_background_interaction_settlement_settled_at').on(table.settled_at),
  ],
)

export const liteLLMBackgroundResponseSettlement = pgTable('LiteLLM_BackgroundResponseSettlement', {
  response_id: text('response_id').primaryKey(),
  settled_at: timestamp('settled_at', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
})

export const liteLLMLens = pgTable(
  'LiteLLM_Lens',
  {
    id: text('id').primaryKey(),
    version: integer('version').notNull().default(0),
    data: jsonb('data').notNull(),
    due_at: timestamp('due_at', { precision: 3 }).default(
      sql`'1970-01-01 00:00:00'::timestamp without time zone`,
    ),
  },
  (table) => [index('LiteLLM_Lens_due_at_idx').on(table.due_at)],
)

export const liteLLMLensRun = pgTable(
  'LiteLLM_LensRun',
  {
    id: text('id').primaryKey(),
    lens_id: text('lens_id').notNull(),
    created_at: timestamp('created_at', { precision: 3 }).notNull(),
    data: jsonb('data').notNull(),
  },
  (table) => [index('LiteLLM_LensRun_lens_id_created_at_idx').on(table.lens_id, table.created_at)],
)

export const liteLLMLensReview = pgTable(
  'LiteLLM_LensReview',
  {
    lens_id: text('lens_id').notNull(),
    criteria_key: text('criteria_key').notNull(),
    execution_id: text('execution_id').notNull(),
    data: jsonb('data').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_LensReview_pkey',
      columns: [table.lens_id, table.criteria_key, table.execution_id],
    }),
    foreignKey({
      name: 'LiteLLM_LensReview_lens_id_fkey',
      columns: [table.lens_id],
      foreignColumns: [liteLLMLens.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const liteLLMLensWorker = pgTable(
  'LiteLLM_LensWorker',
  {
    id: text('id').primaryKey(),
    token_hash: text('token_hash').notNull(),
    data: jsonb('data').notNull(),
  },
  (table) => [uniqueIndex('LiteLLM_LensWorker_token_hash_key').on(table.token_hash)],
)

export const liteLLMLensIngestionKey = pgTable('LiteLLM_LensIngestionKey', {
  id: text('id').primaryKey(),
  data: jsonb('data').notNull(),
})

export const liteLLMLensDataset = pgTable(
  'LiteLLM_LensDataset',
  {
    id: text('id').notNull(),
    revision: integer('revision').notNull(),
    created_at: timestamp('created_at', { precision: 3 }).notNull(),
    data: jsonb('data').notNull(),
  },
  (table) => [
    primaryKey({ name: 'LiteLLM_LensDataset_pkey', columns: [table.id, table.revision] }),
  ],
)

export const liteLLMLensSignalConfig = pgTable('LiteLLM_LensSignalConfig', {
  id: text('id').primaryKey(),
  data: jsonb('data').notNull(),
})

export const liteLLMLensTraceSignal = pgTable(
  'LiteLLM_LensTraceSignal',
  {
    trace_id: text('trace_id').notNull(),
    trace_ref: text('trace_ref').notNull().default(''),
    config_key: text('config_key').notNull(),
    span_count: integer('span_count').notNull(),
    claimed_until: timestamp('claimed_until', { precision: 3 }),
    classified_at: timestamp('classified_at', { precision: 3 }),
    data: jsonb('data').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'LiteLLM_LensTraceSignal_pkey',
      columns: [table.trace_id, table.trace_ref],
    }),
  ],
)

export const liteLLMTelemetryReport = pgTable(
  'LiteLLM_TelemetryReport',
  {
    id: text('id').primaryKey(),
    window_start: doublePrecision('window_start').notNull(),
    window_end: doublePrecision('window_end').notNull(),
    report: jsonb('report').notNull(),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('LiteLLM_TelemetryReport_window_end_idx').on(table.window_end),
    index('LiteLLM_TelemetryReport_created_at_idx').on(table.created_at),
  ],
)

export const liteLLMSpendLogDelivery = pgTable(
  'LiteLLM_SpendLogDelivery',
  {
    delivery_id: text('delivery_id')
      .primaryKey()
      .default(sql`(gen_random_uuid())::text`),
    request_id: text('request_id').notNull(),
    state: accountingDeliveryStateEnum('state').notNull().default('pending'),
    accounting_payload: jsonb('accounting_payload').notNull(),
    is_batch: boolean('is_batch').notNull(),
    attempts: integer('attempts').notNull().default(0),
    next_attempt_at: timestamp('next_attempt_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    applied_at: timestamp('applied_at', { precision: 3 }),
    last_error: text('last_error'),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('LiteLLM_SpendLogDelivery_state_next_attempt_at_idx').on(
      table.state,
      table.next_attempt_at,
    ),
    index('LiteLLM_SpendLogDelivery_state_applied_at_idx').on(table.state, table.applied_at),
    index('LiteLLM_SpendLogDelivery_request_id_idx').on(table.request_id),
  ],
)

export const liteLLMSubscriptionAccountTable = pgTable(
  'LiteLLM_SubscriptionAccountTable',
  {
    subscription_account_id: text('subscription_account_id')
      .primaryKey()
      .$defaultFn(() => crypto.randomUUID()),
    custom_llm_provider: text('custom_llm_provider').notNull(),
    account_id: text('account_id').notNull(),
    label: text('label'),
    monthly_fee: doublePrecision('monthly_fee').notNull(),
    currency: text('currency').notNull(),
    billing_period_start: text('billing_period_start').notNull(),
    created_at: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    created_by: text('created_by').notNull(),
    updated_at: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updated_by: text('updated_by').notNull(),
  },
  (table) => [
    uniqueIndex('LiteLLM_SubscriptionAccountTable_provider_account_key').on(
      table.custom_llm_provider,
      table.account_id,
    ),
  ],
)

export const relations = defineRelations(
  {
    liteLLMBudgetTable,
    liteLLMCredentialsTable,
    liteLLMProxyModelTable,
    liteLLMAgentsTable,
    liteLLMAgentIdentity,
    liteLLMRetiredAgentIdentity,
    liteLLMRetiredAgent,
    liteLLMVerifiedSubject,
    liteLLMOrganizationTable,
    liteLLMModelTable,
    liteLLMTeamTable,
    liteLLMProjectTable,
    liteLLMDeletedTeamTable,
    liteLLMUserTable,
    liteLLMObjectPermissionTable,
    liteLLMMCPServerTable,
    liteLLMMCPToolsetTable,
    liteLLMMCPUserCredentials,
    liteLLMUserProviderCredentials,
    liteLLMMCPUserEnvVars,
    liteLLMMCPServerOAuthClient,
    liteLLMSSOIdentityAssertion,
    liteLLMVerificationToken,
    liteLLMJWTKeyMapping,
    liteLLMDeprecatedVerificationToken,
    liteLLMDeletedVerificationToken,
    liteLLMEndUserTable,
    liteLLMModelAccessGroupBudgetTable,
    liteLLMTagTable,
    liteLLMConfig,
    liteLLMSpendLogs,
    liteLLMBudgetWindowSpend,
    liteLLMErrorLogs,
    liteLLMUserNotifications,
    liteLLMTeamMembership,
    liteLLMOrganizationMembership,
    liteLLMInvitationLink,
    liteLLMAuditLog,
    liteLLMDailyUserSpend,
    liteLLMDailyGlobalSpend,
    liteLLMDailyOrganizationSpend,
    liteLLMDailyEndUserSpend,
    liteLLMDailyAgentSpend,
    liteLLMDailyTeamSpend,
    liteLLMDailyTagSpend,
    liteLLMProxyWorkerHeartbeat,
    liteLLMCronJob,
    liteLLMManagedFileTable,
    liteLLMManagedObjectTable,
    liteLLMManagedFileContentTable,
    liteLLMManagedVectorStoreTable,
    liteLLMManagedVectorStoresTable,
    liteLLMGuardrailsTable,
    liteLLMDailyGuardrailMetrics,
    liteLLMDailyGuardrailUsageUnits,
    liteLLMDailyPolicyMetrics,
    liteLLMSpendLogGuardrailIndex,
    liteLLMSpendLogToolIndex,
    liteLLMDailyToolSpend,
    liteLLMDailyModelUsage,
    liteLLMDailyGatewayRequests,
    liteLLMDailyGatewayFailedRequests,
    liteLLMDailyRequestErrors,
    liteLLMPromptTable,
    liteLLMHealthCheckTable,
    liteLLMSearchToolsTable,
    liteLLMSSOConfig,
    liteLLMManagedVectorStoreIndexTable,
    liteLLMCacheConfig,
    liteLLMUISettings,
    liteLLMConfigOverrides,
    liteLLMSkillsTable,
    liteLLMPolicyTable,
    liteLLMPolicyAttachmentTable,
    liteLLMToolTable,
    liteLLMAccessGroupTable,
    liteLLMClaudeCodePluginTable,
    liteLLMClaudeCodePluginVersion,
    liteLLMSkillSubmission,
    liteLLMClaudeCodePluginDownload,
    liteLLMClaudeCodePluginUpload,
    liteLLMMemoryTable,
    liteLLMAdaptiveRouterState,
    liteLLMAdaptiveRouterSession,
    liteLLMAutoRouterBaselineComparison,
    liteLLMAutoRouterBaselineObservation,
    liteLLMAutoRouterSession,
    liteLLMAutoRouterUserSession,
    liteLLMAutoRouterDailySpend,
    liteLLMShadowEvalJob,
    liteLLMShadowEvalAttempt,
    liteLLMShadowEvalFunnel,
    liteLLMWorkflowRun,
    liteLLMWorkflowEvent,
    liteLLMWorkflowMessage,
    liteLLMBackgroundInteractionSettlement,
    liteLLMBackgroundResponseSettlement,
    liteLLMLens,
    liteLLMLensRun,
    liteLLMLensReview,
    liteLLMLensWorker,
    liteLLMLensIngestionKey,
    liteLLMLensDataset,
    liteLLMLensSignalConfig,
    liteLLMLensTraceSignal,
    liteLLMTelemetryReport,
    liteLLMSpendLogDelivery,
    liteLLMSubscriptionAccountTable,
  },
  (r) => ({
    liteLLMBudgetTable: {
      organization: r.many.liteLLMOrganizationTable(),
      projects: r.many.liteLLMProjectTable(),
      keys: r.many.liteLLMVerificationToken(),
      end_users: r.many.liteLLMEndUserTable(),
      tags: r.many.liteLLMTagTable(),
      model_access_groups: r.many.liteLLMModelAccessGroupBudgetTable(),
      team_membership: r.many.liteLLMTeamMembership(),
      organization_membership: r.many.liteLLMOrganizationMembership(),
    },
    liteLLMAgentsTable: {
      object_permission: r.one.liteLLMObjectPermissionTable({
        from: r.liteLLMAgentsTable.object_permission_id,
        to: r.liteLLMObjectPermissionTable.object_permission_id,
      }),
      identity: r.one.liteLLMAgentIdentity({
        from: r.liteLLMAgentsTable.agent_id,
        to: r.liteLLMAgentIdentity.agent_id,
      }),
      retired_identities: r.many.liteLLMRetiredAgentIdentity(),
    },
    liteLLMAgentIdentity: {
      agent: r.one.liteLLMAgentsTable({
        from: r.liteLLMAgentIdentity.agent_id,
        to: r.liteLLMAgentsTable.agent_id,
        optional: false,
      }),
    },
    liteLLMRetiredAgentIdentity: {
      agent: r.one.liteLLMAgentsTable({
        from: r.liteLLMRetiredAgentIdentity.agent_id,
        to: r.liteLLMAgentsTable.agent_id,
      }),
    },
    liteLLMVerifiedSubject: {
      user: r.one.liteLLMUserTable({
        from: r.liteLLMVerifiedSubject.user_id,
        to: r.liteLLMUserTable.user_id,
      }),
    },
    liteLLMOrganizationTable: {
      litellm_budget_table: r.one.liteLLMBudgetTable({
        from: r.liteLLMOrganizationTable.budget_id,
        to: r.liteLLMBudgetTable.budget_id,
      }),
      teams: r.many.liteLLMTeamTable(),
      users: r.many.liteLLMUserTable(),
      keys: r.many.liteLLMVerificationToken(),
      members: r.many.liteLLMOrganizationMembership(),
      object_permission: r.one.liteLLMObjectPermissionTable({
        from: r.liteLLMOrganizationTable.object_permission_id,
        to: r.liteLLMObjectPermissionTable.object_permission_id,
      }),
    },
    liteLLMModelTable: {
      team: r.one.liteLLMTeamTable({
        from: r.liteLLMModelTable.id,
        to: r.liteLLMTeamTable.model_id,
      }),
    },
    liteLLMTeamTable: {
      litellm_organization_table: r.one.liteLLMOrganizationTable({
        from: r.liteLLMTeamTable.organization_id,
        to: r.liteLLMOrganizationTable.organization_id,
      }),
      litellm_model_table: r.one.liteLLMModelTable({
        from: r.liteLLMTeamTable.model_id,
        to: r.liteLLMModelTable.id,
      }),
      object_permission: r.one.liteLLMObjectPermissionTable({
        from: r.liteLLMTeamTable.object_permission_id,
        to: r.liteLLMObjectPermissionTable.object_permission_id,
      }),
      projects: r.many.liteLLMProjectTable(),
    },
    liteLLMProjectTable: {
      litellm_team_table: r.one.liteLLMTeamTable({
        from: r.liteLLMProjectTable.team_id,
        to: r.liteLLMTeamTable.team_id,
      }),
      litellm_budget_table: r.one.liteLLMBudgetTable({
        from: r.liteLLMProjectTable.budget_id,
        to: r.liteLLMBudgetTable.budget_id,
      }),
      keys: r.many.liteLLMVerificationToken(),
      object_permission: r.one.liteLLMObjectPermissionTable({
        from: r.liteLLMProjectTable.object_permission_id,
        to: r.liteLLMObjectPermissionTable.object_permission_id,
      }),
    },
    liteLLMUserTable: {
      verified_subjects: r.many.liteLLMVerifiedSubject(),
      litellm_organization_table: r.one.liteLLMOrganizationTable({
        from: r.liteLLMUserTable.organization_id,
        to: r.liteLLMOrganizationTable.organization_id,
      }),
      organization_memberships: r.many.liteLLMOrganizationMembership(),
      invitations_created: r.many.liteLLMInvitationLink({ alias: 'CreatedBy' }),
      invitations_updated: r.many.liteLLMInvitationLink({ alias: 'UpdatedBy' }),
      invitations_user: r.many.liteLLMInvitationLink({ alias: 'UserId' }),
      object_permission: r.one.liteLLMObjectPermissionTable({
        from: r.liteLLMUserTable.object_permission_id,
        to: r.liteLLMObjectPermissionTable.object_permission_id,
      }),
    },
    liteLLMObjectPermissionTable: {
      teams: r.many.liteLLMTeamTable(),
      projects: r.many.liteLLMProjectTable(),
      verification_tokens: r.many.liteLLMVerificationToken(),
      organizations: r.many.liteLLMOrganizationTable(),
      users: r.many.liteLLMUserTable(),
      end_users: r.many.liteLLMEndUserTable(),
      agents_table: r.many.liteLLMAgentsTable(),
    },
    liteLLMVerificationToken: {
      litellm_budget_table: r.one.liteLLMBudgetTable({
        from: r.liteLLMVerificationToken.budget_id,
        to: r.liteLLMBudgetTable.budget_id,
      }),
      litellm_organization_table: r.one.liteLLMOrganizationTable({
        from: r.liteLLMVerificationToken.organization_id,
        to: r.liteLLMOrganizationTable.organization_id,
      }),
      litellm_project_table: r.one.liteLLMProjectTable({
        from: r.liteLLMVerificationToken.project_id,
        to: r.liteLLMProjectTable.project_id,
      }),
      object_permission: r.one.liteLLMObjectPermissionTable({
        from: r.liteLLMVerificationToken.object_permission_id,
        to: r.liteLLMObjectPermissionTable.object_permission_id,
      }),
      jwt_key_mappings: r.many.liteLLMJWTKeyMapping(),
    },
    liteLLMJWTKeyMapping: {
      litellm_verification_token: r.one.liteLLMVerificationToken({
        from: r.liteLLMJWTKeyMapping.token,
        to: r.liteLLMVerificationToken.token,
        optional: false,
      }),
    },
    liteLLMEndUserTable: {
      litellm_budget_table: r.one.liteLLMBudgetTable({
        from: r.liteLLMEndUserTable.budget_id,
        to: r.liteLLMBudgetTable.budget_id,
      }),
      object_permission: r.one.liteLLMObjectPermissionTable({
        from: r.liteLLMEndUserTable.object_permission_id,
        to: r.liteLLMObjectPermissionTable.object_permission_id,
      }),
    },
    liteLLMModelAccessGroupBudgetTable: {
      litellm_budget_table: r.one.liteLLMBudgetTable({
        from: r.liteLLMModelAccessGroupBudgetTable.budget_id,
        to: r.liteLLMBudgetTable.budget_id,
      }),
    },
    liteLLMTagTable: {
      litellm_budget_table: r.one.liteLLMBudgetTable({
        from: r.liteLLMTagTable.budget_id,
        to: r.liteLLMBudgetTable.budget_id,
      }),
    },
    liteLLMTeamMembership: {
      litellm_budget_table: r.one.liteLLMBudgetTable({
        from: r.liteLLMTeamMembership.budget_id,
        to: r.liteLLMBudgetTable.budget_id,
      }),
    },
    liteLLMOrganizationMembership: {
      user: r.one.liteLLMUserTable({
        from: r.liteLLMOrganizationMembership.user_id,
        to: r.liteLLMUserTable.user_id,
        optional: false,
      }),
      organization: r.one.liteLLMOrganizationTable({
        from: r.liteLLMOrganizationMembership.organization_id,
        to: r.liteLLMOrganizationTable.organization_id,
        optional: false,
      }),
      litellm_budget_table: r.one.liteLLMBudgetTable({
        from: r.liteLLMOrganizationMembership.budget_id,
        to: r.liteLLMBudgetTable.budget_id,
      }),
    },
    liteLLMInvitationLink: {
      liteLLM_user_table_user: r.one.liteLLMUserTable({
        from: r.liteLLMInvitationLink.user_id,
        to: r.liteLLMUserTable.user_id,
        optional: false,
        alias: 'UserId',
      }),
      liteLLM_user_table_created: r.one.liteLLMUserTable({
        from: r.liteLLMInvitationLink.created_by,
        to: r.liteLLMUserTable.user_id,
        optional: false,
        alias: 'CreatedBy',
      }),
      liteLLM_user_table_updated: r.one.liteLLMUserTable({
        from: r.liteLLMInvitationLink.updated_by,
        to: r.liteLLMUserTable.user_id,
        optional: false,
        alias: 'UpdatedBy',
      }),
    },
    liteLLMClaudeCodePluginTable: {
      uploaded_archive: r.one.liteLLMClaudeCodePluginUpload({
        from: r.liteLLMClaudeCodePluginTable.name,
        to: r.liteLLMClaudeCodePluginUpload.plugin_name,
      }),
      versions: r.many.liteLLMClaudeCodePluginVersion(),
      downloads: r.many.liteLLMClaudeCodePluginDownload(),
    },
    liteLLMClaudeCodePluginVersion: {
      plugin: r.one.liteLLMClaudeCodePluginTable({
        from: r.liteLLMClaudeCodePluginVersion.plugin_id,
        to: r.liteLLMClaudeCodePluginTable.id,
        optional: false,
      }),
    },
    liteLLMClaudeCodePluginDownload: {
      plugin: r.one.liteLLMClaudeCodePluginTable({
        from: r.liteLLMClaudeCodePluginDownload.plugin_id,
        to: r.liteLLMClaudeCodePluginTable.id,
        optional: false,
      }),
    },
    liteLLMClaudeCodePluginUpload: {
      plugin: r.one.liteLLMClaudeCodePluginTable({
        from: r.liteLLMClaudeCodePluginUpload.plugin_name,
        to: r.liteLLMClaudeCodePluginTable.name,
        optional: false,
      }),
    },
    liteLLMWorkflowRun: {
      events: r.many.liteLLMWorkflowEvent(),
      messages: r.many.liteLLMWorkflowMessage(),
    },
    liteLLMWorkflowEvent: {
      run: r.one.liteLLMWorkflowRun({
        from: r.liteLLMWorkflowEvent.run_id,
        to: r.liteLLMWorkflowRun.run_id,
        optional: false,
      }),
    },
    liteLLMWorkflowMessage: {
      run: r.one.liteLLMWorkflowRun({
        from: r.liteLLMWorkflowMessage.run_id,
        to: r.liteLLMWorkflowRun.run_id,
        optional: false,
      }),
    },
    liteLLMLens: { reviews: r.many.liteLLMLensReview() },
    liteLLMLensReview: {
      lens: r.one.liteLLMLens({
        from: r.liteLLMLensReview.lens_id,
        to: r.liteLLMLens.id,
        optional: false,
      }),
    },
  }),
)
