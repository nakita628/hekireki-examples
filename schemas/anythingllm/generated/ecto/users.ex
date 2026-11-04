defmodule AnythingLLM.Users do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          username: String.t() | nil,
          password: String.t(),
          pfp_filename: String.t() | nil,
          role: String.t(),
          suspended: integer(),
          seen_recovery_codes: boolean() | nil,
          last_updated_at: DateTime.t(),
          daily_message_limit: integer() | nil,
          bio: String.t() | nil,
          web_push_subscription_config: String.t() | nil,
          inserted_at: DateTime.t(),
          workspace_chats: [AnythingLLM.WorkspaceChats.t()],
          workspace_users: [AnythingLLM.WorkspaceUsers.t()],
          embed_configs: [AnythingLLM.EmbedConfigs.t()],
          embed_chats: [AnythingLLM.EmbedChats.t()],
          threads: [AnythingLLM.WorkspaceThreads.t()],
          recovery_codes: [AnythingLLM.RecoveryCodes.t()],
          password_reset_tokens: [AnythingLLM.PasswordResetTokens.t()],
          workspace_agent_invocations: [AnythingLLM.WorkspaceAgentInvocations.t()],
          slash_command_presets: [AnythingLLM.SlashCommandPresets.t()],
          browser_extension_api_keys: [AnythingLLM.BrowserExtensionApiKeys.t()],
          temporary_auth_tokens: [AnythingLLM.TemporaryAuthTokens.t()],
          system_prompt_variables: [AnythingLLM.SystemPromptVariables.t()],
          prompt_history: [AnythingLLM.PromptHistory.t()],
          desktop_mobile_devices: [AnythingLLM.DesktopMobileDevices.t()],
          workspace_parsed_files: [AnythingLLM.WorkspaceParsedFiles.t()],
          memories: [AnythingLLM.Memories.t()]
        }

  schema "users" do
    field(:username, :string)
    field(:password, :string)
    field(:pfp_filename, :string, source: :pfpFilename)
    field(:role, :string, default: "default")
    field(:suspended, :integer, default: 0)
    field(:seen_recovery_codes, :boolean, default: false)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    field(:daily_message_limit, :integer, source: :dailyMessageLimit)
    field(:bio, :string, default: "")
    field(:web_push_subscription_config, :string)
    has_many(:workspace_chats, AnythingLLM.WorkspaceChats, foreign_key: :user_id)
    has_many(:workspace_users, AnythingLLM.WorkspaceUsers, foreign_key: :user_id)
    has_many(:embed_configs, AnythingLLM.EmbedConfigs, foreign_key: :users_id)
    has_many(:embed_chats, AnythingLLM.EmbedChats, foreign_key: :users_id)
    has_many(:threads, AnythingLLM.WorkspaceThreads, foreign_key: :user_id)
    has_many(:recovery_codes, AnythingLLM.RecoveryCodes, foreign_key: :user_id)
    has_many(:password_reset_tokens, AnythingLLM.PasswordResetTokens, foreign_key: :user_id)
    has_many(:workspace_agent_invocations, AnythingLLM.WorkspaceAgentInvocations, foreign_key: :user_id)
    has_many(:slash_command_presets, AnythingLLM.SlashCommandPresets, foreign_key: :user_id)
    has_many(:browser_extension_api_keys, AnythingLLM.BrowserExtensionApiKeys, foreign_key: :user_id)
    has_many(:temporary_auth_tokens, AnythingLLM.TemporaryAuthTokens, foreign_key: :user_id)
    has_many(:system_prompt_variables, AnythingLLM.SystemPromptVariables, foreign_key: :user_id)
    has_many(:prompt_history, AnythingLLM.PromptHistory, foreign_key: :modified_by)
    has_many(:desktop_mobile_devices, AnythingLLM.DesktopMobileDevices, foreign_key: :user_id)
    has_many(:workspace_parsed_files, AnythingLLM.WorkspaceParsedFiles, foreign_key: :user_id)
    has_many(:memories, AnythingLLM.Memories, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end